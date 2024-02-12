using Infrastructure.DTOs;

namespace Infrastructure
{
    public interface IDeviceInfoRepository
    {
        public List<DeviceInfo> GetAll();
    }
}
