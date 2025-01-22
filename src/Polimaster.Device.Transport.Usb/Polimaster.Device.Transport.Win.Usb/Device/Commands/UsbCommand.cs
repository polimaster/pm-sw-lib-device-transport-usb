using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Device.Commands;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Device.Commands;

/// <summary>
/// USB Device command
/// </summary>
/// <param name="transport"></param>
/// <param name="loggerFactory"></param>
public abstract class UsbCommand(IUsbTransport transport, ILoggerFactory? loggerFactory)
    : ACommand<ISerialPortStream>(transport, loggerFactory) {

    /// <summary>
    /// Compile command to write to <see cref="ISerialPortStream"/>
    /// </summary>
    /// <returns></returns>
    protected abstract string Compile();

    /// <inheritdoc />
    protected override async Task Execute(ISerialPortStream stream, CancellationToken token) {
        await stream.Write(Compile(), token);
    }
}