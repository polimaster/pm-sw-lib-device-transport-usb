using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <inheritdoc />
public class SerialPortClient : AClient<ISerialPortStream, UsbConnectionProperties> {
    /// <summary>
    /// See <see cref="DevicePort"/>
    /// </summary>
    private DevicePort? _wrapped;
    
    /// <summary>
    /// See <see cref="SerialPort.BaudRate"/>
    /// </summary>
    protected virtual int BaudRate => 9600;
    /// <summary>
    /// See <see cref="SerialPort.ReadTimeout"/>, <see cref="SerialPort.WriteTimeout"/>
    /// </summary>
    protected virtual int Timeout => 2000;
    /// <summary>
    /// See <see cref="SerialPort.ReadBufferSize"/>
    /// </summary>
    protected virtual int BufferSize => 4096;
    
    /// <summary>
    /// See <see cref="SerialPort.DataBits"/>
    /// </summary>
    protected virtual int DataBits => 8;
    
    /// <summary>
    /// See <see cref="SerialPort.NewLine"/>
    /// </summary>
    protected virtual string Newline => "\r\n";
    
    /// <summary>
    /// See <see cref="SerialPort.Encoding"/>
    /// </summary>
    protected virtual Encoding Encoding => Encoding.UTF8;

    /// <inheritdoc />
    public SerialPortClient(UsbConnectionProperties @params, ILoggerFactory? loggerFactory) : base(@params, loggerFactory) {
    }

    /// <inheritdoc />
    public override bool Connected => _wrapped is { IsOpen: true };

    /// <inheritdoc />
    public override void Close() {
        try {
            _wrapped?.Close();
            _wrapped?.Dispose();
        } finally {
            _wrapped = null;
        }
    }

    /// <inheritdoc />
    public override void Reset() {
        Close();
        _wrapped = new DevicePort(Params.Name, BaudRate, Parity.None, DataBits, StopBits.One);
        _wrapped.ReadBufferSize = BufferSize;
        _wrapped.ReadTimeout = Timeout;
        _wrapped.WriteTimeout = Timeout;
        _wrapped.Encoding = Encoding;
        _wrapped.NewLine = Newline;
    }

    /// <inheritdoc />
    public override ISerialPortStream GetStream() {
        if (_wrapped is not { IsOpen: true }) throw new Exception($"{_wrapped?.GetType().Name} is closed or null");
        return new SerialPortStream(_wrapped, LoggerFactory);
    }

    /// <inheritdoc />
    public override Task Open(CancellationToken token) {
        if (_wrapped is { IsOpen: true }) return Task.CompletedTask;

        Reset();
        _wrapped?.Open();
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override void Dispose() => Close();
}