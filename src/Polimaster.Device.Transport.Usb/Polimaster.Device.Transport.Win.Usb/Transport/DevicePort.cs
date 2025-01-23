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
        var data = string.Empty;
        while (BytesToRead > 0) data += ReadExisting();
        return data;
    }
}