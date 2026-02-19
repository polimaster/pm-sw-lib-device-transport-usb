using System.IO;
using System.IO.Ports;
using System.Text;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <summary>
/// <see cref="SerialPort"/> interface adapter
/// </summary>
public interface IDevicePort {
    /// <summary>
    /// See <see cref="SerialPort.Write(byte[],int,int)"/>
    /// </summary>
    void WriteLine(string buffer);

    /// <summary>
    /// See <see cref="SerialPort.ReadLine"/>
    /// </summary>
    string ReadLine();

    /// <summary>
    /// Read all data from <see cref="SerialPort"/>
    /// </summary>
    /// <returns></returns>
    string ReadAll();

    /// <summary>
    /// See <see cref="SerialPort.BaseStream"/>
    /// </summary>
    Stream BaseStream { get; }

    /// <summary>
    /// See <see cref="SerialPort.Encoding"/>
    /// </summary>
    Encoding Encoding { get; }

    /// <summary>
    /// See <see cref="SerialPort.NewLine"/>
    /// </summary>
    string NewLine { get; }
}