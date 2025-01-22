using System.IO.Ports;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <summary>
/// <see cref="SerialPort"/> interface adapter
/// </summary>
public interface IDevicePort {
    /// <summary>
    /// See <see cref="SerialPort.Write(byte[],int,int)"/>
    /// </summary>
    void Write(string buffer);

    /// <summary>
    /// See <see cref="SerialPort.ReadLine"/>
    /// </summary>
    string ReadLine();

    /// <summary>
    /// Read all data from <see cref="SerialPort"/>
    /// </summary>
    /// <returns></returns>
    string ReadAll();
}