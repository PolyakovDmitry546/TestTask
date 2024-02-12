using UseCases;
using Infrastructure;


JsonDeviceInfoRepository deviceInfoRepository = new("Devices.json");
DeviceInfoUseCases deviceInfoUseCases = new(deviceInfoRepository);
var conflicts = deviceInfoUseCases.FindConflicts();
JsonConflictRepository conflictRepository = new("Conflicts.json");
ConflictUseCases conflictUseCases = new(conflictRepository);
conflictUseCases.SaveConflicts(conflicts);
