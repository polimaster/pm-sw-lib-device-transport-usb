using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Device.Commands;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <summary>
/// Serial port stream for reading/ writing data to device.
/// </summary>
public interface ISerialPortStream : IDeviceStream<string> {
    /// <summary>
    /// Read all data from serial port
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<string> ReadAll(CancellationToken cancellationToken);
}


/// <inheritdoc cref="IDeviceStream{T}" />
public class SerialPortStream : ALogged, ISerialPortStream {
    /// <summary>
    /// See <see cref="IDevicePort"/>
    /// </summary>
    private readonly IDevicePort _port;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="port"></param>
    /// <param name="loggerFactory"></param>
    public SerialPortStream(IDevicePort port, ILoggerFactory? loggerFactory = null) : base(loggerFactory) {
        _port = port;
    }

    /// <inheritdoc />
    public virtual Task Write(string buffer, CancellationToken cancellationToken) {
        LogDebug(nameof(Write));
        _port.Write(buffer);
        return Task.CompletedTask;
    }


    /// <summary>
    /// Read line from <see cref="IDevicePort"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<string> Read(CancellationToken cancellationToken) {
        LogDebug(nameof(Read));
        var res = _port.ReadLine();
        return Task.FromResult(res);
    }

    /// <inheritdoc />
    public virtual Task<string> ReadAll(CancellationToken cancellationToken) {
        LogDebug(nameof(ReadAll));
        var res = _port.ReadAll();
        return Task.FromResult(res);
    }
}