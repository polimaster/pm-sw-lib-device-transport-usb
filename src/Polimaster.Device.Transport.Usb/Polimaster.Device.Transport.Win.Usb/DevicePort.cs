using System.Collections.Generic;
using System.IO.Ports;

namespace Polimaster.Device.Transport.Win.Usb;

/// <inheritdoc cref="IDevicePort" />
public class DevicePort : SerialPort, IDevicePort {
    /// <inheritdoc />
    public DevicePort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) :
        base(portName, baudRate, parity, dataBits, stopBits) {
    }

    /// <inheritdoc />
    public void Write(byte[] buffer) => Write(buffer, 0, buffer.Length);

    /// <inheritdoc />
    public byte[] Read() {
        var res = new List<byte>();
        while (BytesToRead > 0) {
            var buffer = new byte[BytesToRead];
            Read(buffer, 0, buffer.Length);
            res.AddRange(buffer);
        }
        return res.ToArray();
    }
}