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
            var desierializedDevices = JsonSerializer.Deserialize<List<DeviceInfo>>(
                jsonString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true}
            );
            if ( desierializedDevices == null ) devices = new List<DeviceInfo>();
            else devices = desierializedDevices;
        }
        public List<DeviceInfo> GetAll()
        {
            return devices;
        }
    }
}
