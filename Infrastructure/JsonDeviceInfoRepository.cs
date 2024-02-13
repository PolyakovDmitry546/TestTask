using Newtonsoft.Json;

using Infrastructure.DTOs;


namespace Infrastructure
{
    public class JsonDeviceInfoRepository : IDeviceInfoRepository
    {
        private readonly string path;
        public JsonDeviceInfoRepository(string path)
        {
            this.path = path;
            if (!File.Exists(path)) 
            {
                throw new FileNotFoundException($"File {path} not found.");
            }
        }

        public List<DeviceInfo> GetAll()
        {
            var jsonString = File.ReadAllText(path);
            List<DeviceInfo> devices = new();
            try
            {
                var desierializedDevices = JsonConvert.DeserializeObject<List<DeviceInfo>>(jsonString);
                if (desierializedDevices != null) devices = desierializedDevices;
            }
            catch (Exception e)
            {
                throw new JsonException(message: e.Message);
            }
            return devices;
        }
    }
}
