using UseCases;
using Infrastructure;


string devicesPath = "Devices.json";
string conflictPath = "Conflicts.json";

if (args.Length > 0) devicesPath = args[0];
if (args.Length > 1) conflictPath = args[1];

JsonDeviceInfoRepository deviceInfoRepository = new(devicesPath);
DeviceInfoUseCases deviceInfoUseCases = new(deviceInfoRepository);
var conflicts = deviceInfoUseCases.FindConflicts();
JsonConflictRepository conflictRepository = new(conflictPath);
ConflictUseCases conflictUseCases = new(conflictRepository);
conflictUseCases.SaveConflicts(conflicts);