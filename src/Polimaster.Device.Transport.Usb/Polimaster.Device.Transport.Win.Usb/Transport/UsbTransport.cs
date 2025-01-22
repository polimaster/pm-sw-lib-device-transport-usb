using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <summary>
/// Usb transport interface
/// </summary>
public interface IUsbTransport : ITransport<ISerialPortStream>;

/// <summary>
/// 
/// </summary>
public class UsbTransport(IClient<ISerialPortStream> client, ILoggerFactory? loggerFactory)
    : ATransport<ISerialPortStream>(client, loggerFactory), IUsbTransport {

    /// <inheritdoc />
    protected override bool KeepOpen => true;

}