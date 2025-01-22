using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// Searches for devices on USB interface
/// </summary>
public interface IUsbDiscovery : ITransportDiscovery<UsbDevice>;

/// <inheritdoc cref="Polimaster.Device.Transport.Win.Usb.IUsbDiscovery" />
public abstract class UsbDiscovery : ATransportDiscovery<UsbDevice>, IUsbDiscovery {
    /// <summary>
    /// See <see cref="ManagementEventWatcher"/>
    /// </summary>
    private readonly ManagementEventWatcher _managementEventWatcher;
    
    /// <summary>
    /// Device vendor identifier
    /// </summary>
    protected abstract string VID { get; }
    
    /// <summary>
    /// Device product identifier
    /// </summary>
    protected abstract string PID { get; }
    
    /// <inheritdoc />
    protected UsbDiscovery(ILoggerFactory? loggerFactory) : base(loggerFactory) {
        const string query =
            "SELECT * FROM __InstanceOperationEvent WITHIN 1 WHERE TargetInstance isa 'Win32_SerialPort'";
        _managementEventWatcher = new ManagementEventWatcher(new WqlEventQuery(query));
        _managementEventWatcher.EventArrived += OnMEWEvent;
    }

    /// <inheritdoc />
    protected override void Search() {
    }

    /// <inheritdoc />
    public override void Start(CancellationToken token) {
        base.Start(token);
        _managementEventWatcher.Start();
        
        Logger?.LogDebug("Search for already connected devices");
        using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SerialPort");
        var managementObjectCollection = searcher.Get();

        foreach (var deviceUsb in managementObjectCollection) {
            var pnpDeviceId = (string)deviceUsb.GetPropertyValue("PNPDeviceID");
            var deviceId = (string)deviceUsb.GetPropertyValue("DeviceID");
            if (pnpDeviceId.Contains($"{VID}&{PID}")) {
                OnFound(new UsbDevice(deviceId, pnpDeviceId));
            }
        }
    }

    /// <inheritdoc />
    public override void Stop() {
        base.Stop();
        _managementEventWatcher.Stop();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMEWEvent(object sender, EventArrivedEventArgs e) {
        var p = e.NewEvent.Properties["TargetInstance"];
        if (p.Value is not ManagementBaseObject mbo) return;

        var deviceId = mbo.Properties["DeviceID"];
        var pnpDeviceId = mbo.Properties["PNPDeviceID"];

        var usbDevice = new UsbDevice(deviceId.Value.ToString(), pnpDeviceId.Value.ToString());
        if (!usbDevice.PlugAndPlayId.Contains($"{VID}&{PID}")) return;

        switch (e.NewEvent.ClassPath.ClassName) {
            case "__InstanceCreationEvent":
                OnFound(usbDevice);
                break;
            case "__InstanceDeletionEvent":
                OnLost(usbDevice);
                break;
        }
    }
    
    /// <summary>
    /// When usb device found
    /// </summary>
    /// <param name="usbDevice"></param>
    private void OnFound(UsbDevice usbDevice) {
        Logger?.LogDebug("Found device {@Device}", usbDevice);
        Found?.Invoke(new List<UsbDevice> { usbDevice });
    }

    /// <summary>
    /// When usb device lost
    /// </summary>
    /// <param name="usbDevice"></param>
    private void OnLost(UsbDevice usbDevice) {
        Logger?.LogDebug("Lost device {@Device}", usbDevice);
        Lost?.Invoke(new List<UsbDevice> { usbDevice });
    }

    /// <inheritdoc />
    public override void Dispose() {
        base.Dispose();
        _managementEventWatcher.EventArrived -= OnMEWEvent;
        _managementEventWatcher.Stop();
        _managementEventWatcher.Dispose();
    }

    /// <inheritdoc />
    public override event Action<IEnumerable<UsbDevice>>? Found;

    /// <inheritdoc />
    public override event Action<IEnumerable<UsbDevice>>? Lost;
}