using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Device.Commands;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Device.Commands;

/// <summary>
/// USB Device command for writing data
/// </summary>
/// <param name="transport"></param>
/// <param name="loggerFactory"></param>
/// <typeparam name="T"></typeparam>
public abstract class UsbWriter<T>(IUsbTransport transport, ILoggerFactory? loggerFactory)
    : AWriter<T, string, ISerialPortStream>(transport, loggerFactory) {

    /// <inheritdoc />
    protected override async Task Execute(ISerialPortStream stream, string compiled, CancellationToken cancellationToken) {
        await stream.Write(compiled, cancellationToken);
    }
}