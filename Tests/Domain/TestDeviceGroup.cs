using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace TestDomain
{
    
    public class TestDeviceGroup
    {
        private readonly Device _device;
        private readonly DeviceGroup _group;
        private readonly Brigade _brigade;

        public TestDeviceGroup() {
            _brigade = new("111");
            _device = new("101001", false);
        }

        [Fact]
        public void TestCreateDeviceGroup()
        {

            DeviceGroup deviceGroup = new(_brigade, _device);

            Assert.Equal(_brigade, deviceGroup.Brigade);
            Assert.Contains(_device, deviceGroup.Devices);
        }

        [Fact]
        public void TestDeviceGroupAddDevice()
        {
            Device secondDevice = new("222", false);
            DeviceGroup deviceGroup = new(_brigade, _device);

            deviceGroup.AddDevice(secondDevice);

            Assert.Contains(secondDevice, deviceGroup.Devices);
        }

        [Fact]
        public void TestDeviceGroupWithOneDeviceIsNotInvalid()
        {
            DeviceGroup deviceGroup = new(_brigade, _device);

            Assert.False(deviceGroup.IsValid);
        }

        [Fact]
        public void TestDeviceGroupWithTwoOrMoreDevicesIsValid()
        {
            Device secondDevice = new("222", false);
            Device thirdDevice = new("333", false);
            DeviceGroup deviceGroup = new(_brigade, _device);

            deviceGroup.AddDevice(secondDevice);

            Assert.True(deviceGroup.IsValid);

            deviceGroup.AddDevice(thirdDevice);

            Assert.True(deviceGroup.IsValid);
        }

        [Fact]
        public void TestDeviceGroupAddConflictingDevice()
        {
            Device conflictDevice = new("222", true);
            DeviceGroup deviceGroup = new(_brigade, _device);

            deviceGroup.AddDevice(conflictDevice);

            Assert.True(deviceGroup.HasConflicts);
        }

        [Fact]
        public void TestDeviceGroupHasConflictsIfFirstAddedDeviceIsOnlineButSecondIsNot()
        {
            Device onlineDevice = new("111", true);
            Device secondDevice = new("222", false);
            DeviceGroup deviceGroup = new(_brigade, onlineDevice);

            deviceGroup.AddDevice(secondDevice);

            Assert.True(deviceGroup.HasConflicts);
        }

        [Fact]
        public void TestDeviceGroupWithOneDeviceHasNoConflicts()
        {
            Device onlineDevice = new("111", true);
            Device offlineDevice = new("222", false);
            DeviceGroup oneOnlinedeviceGroup = new(_brigade, onlineDevice);
            DeviceGroup oneOfflinedeviceGroup = new(_brigade, offlineDevice);

            Assert.False(oneOnlinedeviceGroup.HasConflicts);
            Assert.False(oneOfflinedeviceGroup.HasConflicts);
        }
    }
}
