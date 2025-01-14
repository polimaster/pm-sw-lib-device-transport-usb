using System.IO.Ports;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// <see cref="SerialPort"/> interface adapter
/// </summary>
public interface IDevicePort {
    /// <summary>
    /// See <see cref="SerialPort.Write"/>
    /// </summary>
    void Write(byte[] buffer);

    /// <summary>
    /// See <see cref="SerialPort.ReadTo"/>
    /// </summary>
    byte[] Read();

    /// <summary>
    /// See <see cref="SerialPort.NewLine"/>
    /// </summary>
    string NewLine { get; set; }
}