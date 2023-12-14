using System;
using Moq;

namespace Polimaster.Device.Transport.Win.Usb.Tests;

public class SerialPortStreamTest : Mocks {
    
    [Fact]
    public async void ShouldRead() {
        var port = new Mock<IDevicePort>();
        var response = Guid.NewGuid().ToString();
        port.Setup(e => e.ReadTo(It.IsAny<string>())).Returns(response);
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);

        var result = await stream.ReadAsync(Token);
        
        port.Verify(e => e.ReadTo(It.IsAny<string>()));
        Assert.Equal(response, result);
    }

    [Fact]
    public async void ShouldWrite() {
        var port = new Mock<IDevicePort>();
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);
        
        var buff = Guid.NewGuid().ToString();
        await stream.WriteAsync(buff, Token);
        
        port.Verify(e => e.WriteLine(buff));
    }
}