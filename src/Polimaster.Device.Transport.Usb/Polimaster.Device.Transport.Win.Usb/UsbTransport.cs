using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// 
/// </summary>
public class UsbTransport : ATransport<string> {
    
    /// <inheritdoc />
    protected override bool KeepOpen => true;

    /// <inheritdoc />
    public UsbTransport(IClient<string> client, ILoggerFactory? loggerFactory) : base(client, loggerFactory) {
    }
}