using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// 
/// </summary>
public class UsbTransport : ATransport {
    
    /// <inheritdoc />
    protected override bool KeepOpen => true;

    /// <inheritdoc />
    public UsbTransport(IClient client, ILoggerFactory? loggerFactory) : base(client, loggerFactory) {
    }
}