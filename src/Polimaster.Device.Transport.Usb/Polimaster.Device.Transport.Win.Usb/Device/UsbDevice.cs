using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract.Device;
using Polimaster.Device.Abstract.Device.Settings;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Device;

/// <summary>
/// UsbDevice interface
/// </summary>
public interface IUsbDevice : IDevice<IUsbTransport, ISerialPortStream>;

/// <summary>
/// Default ub device implementation
/// </summary>
public abstract class UsbDevice(IUsbTransport transport, ISettingDescriptors settingDescriptors, ILoggerFactory? loggerFactory = null)
    : ADevice<IUsbTransport, ISerialPortStream>(transport, settingDescriptors, loggerFactory), IUsbDevice;