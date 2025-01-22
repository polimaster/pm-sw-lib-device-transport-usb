using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Device.Commands;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Device.Commands;

/// <summary>
/// USB Device command for reading data
/// </summary>
/// <param name="transport"></param>
/// <param name="loggerFactory"></param>
/// <typeparam name="T"></typeparam>
public abstract class UsbReader<T>(IUsbTransport transport, ILoggerFactory? loggerFactory)
    : AReader<T, string, ISerialPortStream>(transport, loggerFactory) {

    /// <summary>
    /// Compile command to write to <see cref="ISerialPortStream"/>
    /// </summary>
    /// <returns></returns>
    protected abstract string Compile();

    /// <inheritdoc />
    protected override async Task<string> Execute(ISerialPortStream stream, CancellationToken cancellationToken) {
        await stream.Write(Compile(), cancellationToken);
        return await stream.Read(cancellationToken);
    }
}