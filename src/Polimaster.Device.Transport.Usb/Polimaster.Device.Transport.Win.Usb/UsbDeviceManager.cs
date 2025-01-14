using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract;
using Polimaster.Device.Abstract.Device;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// Usb device manager
/// </summary>
/// <param name="discovery"><see cref="ITransportDiscovery{TConnectionParams}"/></param>
/// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
/// <typeparam name="TDevice"><see cref="IDevice"/></typeparam>
public abstract class UsbDeviceManager<TDevice>(ITransportDiscovery<UsbDevice> discovery, ILoggerFactory? loggerFactory)
    : ADeviceManager<TDevice, ITransportDiscovery<UsbDevice>, UsbDevice>(discovery, loggerFactory) where TDevice : IDevice {

    /// <inheritdoc />
    protected override ITransport CreateTransport(IClient client) => new UsbTransport(client, LoggerFactory);

    /// <inheritdoc />
    protected override IClient CreateClient(UsbDevice connectionParams) => new SerialPortClient(connectionParams, LoggerFactory);
}