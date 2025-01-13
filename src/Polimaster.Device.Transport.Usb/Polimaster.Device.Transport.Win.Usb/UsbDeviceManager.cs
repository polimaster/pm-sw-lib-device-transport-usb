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
/// <typeparam name="TDevice"><see cref="IDevice{T}"/></typeparam>
public abstract class UsbDeviceManager<TDevice>(ITransportDiscovery<UsbDevice> discovery, ILoggerFactory? loggerFactory)
    : ADeviceManager<TDevice, string, ITransportDiscovery<UsbDevice>, UsbDevice>(discovery, loggerFactory) where TDevice : IDevice<string> {

    /// <inheritdoc />
    protected override ITransport<string> CreateTransport(IClient<string> client) => new UsbTransport(client, LoggerFactory);

    /// <inheritdoc />
    protected override IClient<string> CreateClient(UsbDevice connectionParams) => new SerialPortClient(connectionParams, LoggerFactory);
}