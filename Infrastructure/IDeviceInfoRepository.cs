using Infrastructure.DTOs;

namespace Infrastructure
{
    public interface IDeviceInfoRepository
    {
        public IEnumerable<DeviceInfo> GetAll();
    }
}
