using Newtonsoft.Json;


namespace Infrastructure.DTOs
{
    public class DeviceInfo
    {
        [JsonRequired]
        public Device Device { get; set; }
        [JsonRequired]
        public Brigade Brigade { get; set; }
    }
}
