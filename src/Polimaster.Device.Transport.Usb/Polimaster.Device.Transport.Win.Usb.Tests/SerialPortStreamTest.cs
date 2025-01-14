using System;
using System.Threading.Tasks;
using Moq;

namespace Polimaster.Device.Transport.Win.Usb.Tests;

public class SerialPortStreamTest : Mocks {
    
    [Fact]
    public async Task ShouldRead() {
        var port = new Mock<IDevicePort>();
        var response = Guid.NewGuid().ToByteArray();
        port.Setup(e => e.Read()).Returns(response);
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);

        var result = await stream.ReadAsync<object>(Token);
        
        port.Verify(e => e.Read());
        Assert.Equal(response, result);
    }

    [Fact]
    public async Task ShouldWrite() {
        var port = new Mock<IDevicePort>();
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);
        
        var buff = Guid.NewGuid().ToByteArray();
        await stream.WriteAsync<object>(buff, Token);
        
        port.Verify(e => e.Write(buff));
    }
}