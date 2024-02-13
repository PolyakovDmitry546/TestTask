using Domain;
using Infrastructure;
using Infrastructure.DTOs;

namespace UseCases
{
    public class DeviceInfoUseCases
    {
        private IDeviceInfoRepository repository;
        public DeviceInfoUseCases(IDeviceInfoRepository repository)
        {
            this.repository = repository;
        }

        private static Dictionary<string, DeviceGroup> GetDeviceGroups(IEnumerable<DeviceInfo> devices)
        {
            Dictionary<string, DeviceGroup> deviceGroups = new();
            foreach (var deviceInfo in devices)
            {
                var device = new Domain.Device(
                    serialNumber: deviceInfo.Device.SerialNumber,
                    isOnline: deviceInfo.Device.IsOnline
                );
                if (deviceGroups.ContainsKey(deviceInfo.Brigade.Code))
                {
                    deviceGroups[deviceInfo.Brigade.Code].AddDevice(device);
                }
                else
                {
                    Domain.Brigade brigade = new(code: deviceInfo.Brigade.Code);
                    deviceGroups.Add(brigade.Code, new DeviceGroup(brigade: brigade, device: device));
                }
            }
            return deviceGroups;
        }

        private static List<Conflict> GetConflicts(Dictionary<string, DeviceGroup> deviceGroups)
        {
            List<Conflict> conflicts = new();
            foreach (var deviceGroup in deviceGroups.Values)
            {
                if (deviceGroup.HasConflicts)
                {
                    conflicts.Add(
                        new Conflict()
                        {
                            BrigadeCode = deviceGroup.Brigade.Code,
                            DevicesSerials = (from device in deviceGroup.Devices select device.SerialNumber).ToArray()
                        }
                    );
                }
            }
            return conflicts;
        }

        public List<Conflict> FindConflicts()
        {
            var devices = repository.GetAll();
            var deviceGroups = GetDeviceGroups(devices);
            var conflicts = GetConflicts(deviceGroups);
            return conflicts;
        }
    }
}
