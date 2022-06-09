using EFModel.Vehicles;
using Xunit;
using xu = Xunit;

namespace EFModelTests
{
    public class VehiclesTest
    {
        [Fact]
        public void Test1()
        {
            var v = new Car() { Accessories = Accessorie.GPS | Accessorie.Radio };
            Assert.False(v.Accessories.HasFlag(Accessorie.AirConditioned));
            Assert.True(v.Accessories.HasFlag(Accessorie.GPS));
            Assert.True(v.Accessories.HasFlag(Accessorie.Radio));
            
        }
    }
}