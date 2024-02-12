using System.Text.Json;

using Infrastructure.DTOs;

namespace Infrastructure
{
    public class JsonDeviceInfoRepository : IDeviceInfoRepository
    {
        List<DeviceInfo> devices;
        public JsonDeviceInfoRepository(string path)
        {
            var jsonString = File.ReadAllText(path);
            devices = JsonSerializer.Deserialize<List<DeviceInfo>>(
                jsonString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true}
            );
        }
        public List<DeviceInfo> GetAll()
        {
            return devices;
        }
    }
}
