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
    public virtual async Task Write(string buffer, CancellationToken cancellationToken) {
        // LogDebug(nameof(Write));
        var data = _port.Encoding.GetBytes(buffer + _port.NewLine);
        await _port.BaseStream.WriteAsync(data, 0, data.Length, cancellationToken);
    }


    /// <summary>
    /// Read line from <see cref="IDevicePort"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<string> Read(CancellationToken cancellationToken) {
        // LogDebug(nameof(Read));
        // SerialPort.ReadLine doesn't have an async version, 
        // but we can wrap it in Task.Run if we want to avoid blocking the calling thread,
        // though it still blocks a thread pool thread.
        // For now, keeping it as is or using Task.Run for better "apparent" asynchrony.
        return Task.Run(_port.ReadLine, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task<string> ReadAll(CancellationToken cancellationToken) {
        // LogDebug(nameof(ReadAll));
        return Task.Run(_port.ReadAll, cancellationToken);
    }
}