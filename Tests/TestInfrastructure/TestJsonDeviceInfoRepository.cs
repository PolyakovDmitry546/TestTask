using Infrastructure;
using Infrastructure.DTOs;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;


namespace TestInfrastructure
{
    public class TestJsonDeviceInfoRepository
    {
        IEqualityComparer<List<DeviceInfo>> equalityComparer;
        public TestJsonDeviceInfoRepository() 
        {
            equalityComparer = new ListDeviceInfoComparer();
        }

        [Fact]
        public void TestDeserializeJsonFile()
        {
            string directory = ".\\..\\..\\..\\TestJsonFiles";
            string fileName = "testJson1.json";
            string filePath =  Path.Combine(directory, fileName);
            var repository = new JsonDeviceInfoRepository(filePath);
            List<DeviceInfo> expectedDevices = new() 
            {
                new DeviceInfo 
                {            
                    Device = new Device { SerialNumber = "913580515e13", IsOnline = false },
                    Brigade = new Brigade { Code = "2027154745" }
                },
                new DeviceInfo
                {
                    Device = new Device { SerialNumber = "624535123e13", IsOnline = true },
                    Brigade = new Brigade { Code = "2027154745" }
                }
            };

            var devices = repository.GetAll();

            Assert.Equal(expectedDevices, devices, comparer: equalityComparer);
        }

        public class ListDeviceInfoComparer : IEqualityComparer<List<DeviceInfo>>
        {
            public bool Equals(List<DeviceInfo>? x, List<DeviceInfo>? y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                if (x.Count != y.Count) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    var xDeviceInfo = x[i];
                    var yDeviceInfo = y[i];
                    if (xDeviceInfo.Brigade.Code != yDeviceInfo.Brigade.Code) return false;
                    if (xDeviceInfo.Device.SerialNumber != yDeviceInfo.Device.SerialNumber) return false;
                    if (xDeviceInfo.Device.IsOnline != yDeviceInfo.Device.IsOnline) return false;
                }
                return true;
            }

            public int GetHashCode([DisallowNull] List<DeviceInfo> obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
