using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// Usb transport inteface
/// </summary>
public interface IUsbTransport : ITransport;

/// <summary>
/// 
/// </summary>
public class UsbTransport : ATransport, IUsbTransport {
    
    /// <inheritdoc />
    protected override bool KeepOpen => true;

    /// <inheritdoc />
    public UsbTransport(IClient client, ILoggerFactory? loggerFactory) : base(client, loggerFactory) {
    }
}