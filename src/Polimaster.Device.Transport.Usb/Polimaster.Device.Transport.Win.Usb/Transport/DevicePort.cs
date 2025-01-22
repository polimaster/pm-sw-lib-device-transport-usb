using System.Collections.Generic;
using System.IO.Ports;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <inheritdoc cref="IDevicePort" />
public class DevicePort : SerialPort, IDevicePort {

    /// <inheritdoc />
    public DevicePort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) :
        base(portName, baudRate, parity, dataBits, stopBits) {
        // RtsEnable = true;
    }

    /// <inheritdoc />
    public string ReadAll() {
        var res = new List<byte>();
        while (BytesToRead > 0) {
            var buffer = new byte[BytesToRead];
            Read(buffer, 0, buffer.Length);
            res.AddRange(buffer);
        }
        return System.Text.Encoding.UTF8.GetString(res.ToArray());
    }
}