using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DeviceGroup
    {
        private readonly Brigade brigade;
        private readonly List<Device> devices = new();
        private bool hasOnlineDevices;

        public DeviceGroup(Brigade brigade, Device device) {
            this.brigade = brigade;
            AddDevice(device);
        }
        public Brigade Brigade { 
            get { return brigade; }
        }

        public List<Device> Devices { 
            get { return devices; } 
        }

        public bool IsValid { 
            get { return devices.Count > 1; } 
        }

        public bool HasConflicts { 
            get { return IsValid && hasOnlineDevices; } 
        }

        public void AddDevice(Device device)
        {
            devices.Add(device);
            if (device.IsOnline)
            {
                hasOnlineDevices = true;
            }
        }
    }
}
