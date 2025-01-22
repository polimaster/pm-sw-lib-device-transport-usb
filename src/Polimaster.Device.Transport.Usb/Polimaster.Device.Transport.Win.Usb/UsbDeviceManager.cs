using System;
using Microsoft.Extensions.Logging;
using Polimaster.Device.Abstract;
using Polimaster.Device.Abstract.Device;
using Polimaster.Device.Abstract.Transport;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb;

/// <summary>
/// Usb device manager
/// </summary>
/// <param name="discovery"><see cref="IUsbDiscovery"/></param>
/// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
/// <typeparam name="TDevice">Subclass of <see cref="IDevice{TTransport,TStream}"/></typeparam>
/// <typeparam name="TTransport">Subclass of <see cref="ITransport{ISerialPortStream}"/></typeparam>
/// <typeparam name="TStream">Subclass of <see cref="ISerialPortStream"/></typeparam>
/// <typeparam name="TDiscovery">Subclass of <see cref="IUsbDiscovery"/></typeparam>
public abstract class UsbDeviceManager<TDevice, TTransport, TStream, TDiscovery>(TDiscovery discovery, ILoggerFactory? loggerFactory)
    : ADeviceManager<TDevice, TTransport, TStream, TDiscovery, UsbConnectionProperties>(discovery, loggerFactory)
    where TDevice : IDevice<TTransport, TStream>, IDisposable
    where TTransport : ITransport<TStream>
    where TStream : ISerialPortStream
    where TDiscovery : IUsbDiscovery;


/// <summary>
/// Usb device manager with default <see cref="SerialPortStream"/> implemntation
/// </summary>
/// <param name="discovery"><see cref="IUsbDiscovery"/></param>
/// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
/// <typeparam name="TDevice">Subclass of <see cref="IDevice{TTransport,TStream}"/></typeparam>
/// <typeparam name="TTransport">Subclass of <see cref="ITransport{ISerialPortStream}"/></typeparam>
/// <typeparam name="TDiscovery">Subclass of <see cref="IUsbDiscovery"/></typeparam>
public abstract class UsbDeviceManager<TDevice, TTransport, TDiscovery>(TDiscovery discovery, ILoggerFactory? loggerFactory)
    : UsbDeviceManager<TDevice, TTransport, ISerialPortStream, TDiscovery>(discovery, loggerFactory)
    where TDevice : IDevice<TTransport, ISerialPortStream>, IDisposable
    where TTransport : IUsbTransport
    where TDiscovery : IUsbDiscovery {

    /// <inheritdoc />
    protected override IClient<ISerialPortStream> CreateClient(UsbConnectionProperties connectionParams) => new SerialPortClient(connectionParams, LoggerFactory);
}

/// <summary>
/// Usb device manager with default <see cref="UsbTransport"/> implemntation
/// </summary>
/// <param name="discovery"><see cref="IUsbDiscovery"/></param>
/// <param name="loggerFactory"><see cref="ILoggerFactory"/></param>
/// <typeparam name="TDevice">Subclass of <see cref="IDevice{TTransport,TStream}"/></typeparam>
/// <typeparam name="TDiscovery">Subclass of <see cref="IUsbDiscovery"/></typeparam>
public abstract class UsbDeviceManager<TDevice, TDiscovery>(TDiscovery discovery, ILoggerFactory? loggerFactory)
    : UsbDeviceManager<TDevice, IUsbTransport, TDiscovery>(discovery, loggerFactory)
    where TDevice : IDevice<IUsbTransport, ISerialPortStream>, IDisposable
    where TDiscovery : IUsbDiscovery {

    /// <inheritdoc />
    protected override IUsbTransport CreateTransport(IClient<ISerialPortStream> client) => new UsbTransport(client, LoggerFactory);
}
