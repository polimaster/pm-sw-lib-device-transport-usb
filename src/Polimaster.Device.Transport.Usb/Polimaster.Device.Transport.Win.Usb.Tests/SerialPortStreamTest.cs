using System;
using System.Threading.Tasks;
using Moq;
using Polimaster.Device.Transport.Win.Usb.Transport;

namespace Polimaster.Device.Transport.Win.Usb.Tests;

public class SerialPortStreamTest : Mocks {
    
    [Fact]
    public async Task ShouldRead() {
        var port = new Mock<IDevicePort>();
        var response = Guid.NewGuid().ToString();
        port.Setup(e => e.ReadLine()).Returns(response);
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);

        var result = await stream.Read(Token);
        
        port.Verify(e => e.ReadLine());
        Assert.Equal(response, result);
    }

    [Fact]
    public async Task ShouldReadAll() {
        var port = new Mock<IDevicePort>();
        var response = Guid.NewGuid().ToString();
        port.Setup(e => e.ReadAll()).Returns(response);
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);

        var result = await stream.ReadAll(Token);

        port.Verify(e => e.ReadAll());
        Assert.Equal(response, result);
    }


    [Fact]
    public async Task ShouldWrite() {
        var port = new Mock<IDevicePort>();
        var stream = new SerialPortStream(port.Object, LOGGER_FACTORY);
        
        var buff = Guid.NewGuid().ToString();
        await stream.Write(buff, Token);
        
        port.Verify(e => e.Write(buff));
    }
}