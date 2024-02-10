using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Device
    {
        private readonly string serialNumber;
        private readonly bool isOnline;

        public Device(string serialNumber, bool isOnline) { 
            this.serialNumber = serialNumber;
            this.isOnline = isOnline;
        }

        public string SerialNumber {
            get { return serialNumber; }
        }
        public bool IsOnline { 
            get { return isOnline; } 
        }
    }
}
