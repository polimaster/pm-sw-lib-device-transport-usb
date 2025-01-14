using System.IO.Ports;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// <see cref="SerialPort"/> interface adapter
/// </summary>
public interface IDevicePort {
    /// <summary>
    /// See <see cref="SerialPort.Write(byte[],int,int)"/>
    /// </summary>
    void Write(byte[] buffer);

    /// <summary>
    /// See <see cref="SerialPort.Read(byte[],int,int)"/>
    /// </summary>
    byte[] Read();
}