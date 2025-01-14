using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace Polimaster.Device.Transport.Win.Usb;

/// <inheritdoc cref="IDevicePort" />
public class DevicePort : SerialPort, IDevicePort {

    private ManualResetEvent? resetEvent;
    private List<byte> readBuffer = [];
    private SerialError? readError;

    /// <inheritdoc />
    public DevicePort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits) :
        base(portName, baudRate, parity, dataBits, stopBits) {
    }

    /// <inheritdoc />
    public void Write(byte[] buffer) => Write(buffer, 0, buffer.Length);

    /// <inheritdoc />
    public byte[] Read() {
        DataReceived += OnDataReceived;
        ErrorReceived += OnErrorReceived;
        readBuffer = [];
        readError = null;
        resetEvent = new ManualResetEvent(false);
        resetEvent.WaitOne();
        resetEvent.Reset();

        // var res = new List<byte>();
        // while (BytesToRead > 0) {
        //     var buffer = new byte[BytesToRead];
        //     Read(buffer, 0, buffer.Length);
        //     res.AddRange(buffer);
        // }

        DataReceived -= OnDataReceived;
        ErrorReceived -= OnErrorReceived;
        if (readError != null) throw new Exception($"Data read error: {readError}");
        return readBuffer.ToArray();
    }

    private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e) {
        readError = e.EventType;
        resetEvent?.Set();
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e) {
        if (e.EventType == SerialData.Eof) resetEvent?.Set();

        var buffer = new byte[BytesToRead];
        Read(buffer, 0, BytesToRead);
        readBuffer.AddRange(buffer);
    }
}