using Infrastructure.DTOs;

namespace Infrastructure
{
    public class FakeDeviceInfoRepository : IDeviceInfoRepository
    {
        private List<DeviceInfo> db = new();
        public FakeDeviceInfoRepository(List<DeviceInfo>? fakeData = null)
        {
            if (fakeData != null) { db = fakeData; }
        }
        public List<DeviceInfo> GetAll()
        {
            return db;
        }
    }
}
