using System;

namespace Polimaster.Device.Transport.Win.Usb.Transport;

/// <summary>
/// USB device connection properties
/// </summary>
public class UsbConnectionProperties : IFormattable {
    /// <summary>
    /// Device identifier. Usually COM port name.
    /// </summary>
    public readonly string Name;
    
    /// <summary>
    /// PlugAndPlay id
    /// </summary>
    public readonly string PlugAndPlayId;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="plugAndPlayId"></param>
    public UsbConnectionProperties(string name, string plugAndPlayId) {
        Name = name;
        PlugAndPlayId = plugAndPlayId;
    }

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)=> $"{Name}:{PlugAndPlayId}";

    /// <inheritdoc />
    public override string ToString() {
        return ToString(null, null);
    }
}