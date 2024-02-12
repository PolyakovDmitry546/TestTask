using UseCases;
using Infrastructure;
using Infrastructure.DTOs;
using Xunit.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace TestUseCases
{
    public class TestDeviceInfoUseCases
    {
        private readonly IEqualityComparer<List<Conflict>> listConflictsComparer;

        public TestDeviceInfoUseCases()
        {
            listConflictsComparer = new ListConflictsComparer();
        }
        private static IDeviceInfoRepository CreateFakeRepository(object[] deviceInfoList)
        {
            List<DeviceInfo> devices = new();
            foreach (object[] deviceInfo in deviceInfoList) 
            {
                devices.Add(
                    new DeviceInfo
                    {
                        Device = new Device { SerialNumber = (string)deviceInfo[0], IsOnline = (bool)deviceInfo[1] },
                        Brigade = new Brigade { Code = (string)deviceInfo[2] }
                    }
                );
            }
            return new FakeDeviceInfoRepository(devices);
        }

        private static List<Conflict> CreateFakeConflicts(object[] conflicts) 
        {
            List<Conflict> expectedConflicts = new();
            foreach (object[] conflict in conflicts)
            {
                expectedConflicts.Add(
                    new Conflict
                    {
                        BrigadeCode = (string)conflict[0],
                        DevicesSerials = (string[])conflict[1]
                    }
                );
            }
            return expectedConflicts;
        }

        [Theory]
        [MemberData(nameof(DeviceInfoWithoutConflict))]
        public void TestFindWithoutConflicts(object[] deviceInfoList, object[] conflictList)
        {
            DeviceInfoUseCases useCases = new(CreateFakeRepository(deviceInfoList));
            var expectedConflicts = CreateFakeConflicts(conflictList);

            var conflicts = useCases.FindConflicts();

            Assert.Equal(expectedConflicts, conflicts, comparer: listConflictsComparer);
        }

        [Theory]
        [MemberData(nameof(DeviceInfoWithOneConflict))]
        public void TestFindOneConflict(object[] deviceInfoList, object[] conflictList)
        {
            DeviceInfoUseCases useCases = new(CreateFakeRepository(deviceInfoList));
            var expectedConflicts = CreateFakeConflicts(conflictList);

            var conflicts = useCases.FindConflicts();
            
            Assert.Equal(expectedConflicts, conflicts, comparer: listConflictsComparer);
        }

        [Theory]
        [MemberData(nameof(DeviceInfoMoreThenOneConflict))]
        public void TestFindMoreThenOneConflict(object[] deviceInfoList, object[] conflictList)
        {
            DeviceInfoUseCases useCases = new(CreateFakeRepository(deviceInfoList));
            var expectedConflicts = CreateFakeConflicts(conflictList);

            var conflicts = useCases.FindConflicts();

            Assert.Equal(expectedConflicts, conflicts, comparer: listConflictsComparer);
        }

        public class ListConflictsComparer : IEqualityComparer<List<Conflict>>
        {
            public bool Equals(List<Conflict>? x, List<Conflict>? y)
            {
                if (x.Count != y.Count) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    var xConflict = x[i];
                    var yConflict = y[i];
                    if (xConflict.BrigadeCode != yConflict.BrigadeCode) return false;
                    if (xConflict.DevicesSerials.Length != yConflict.DevicesSerials.Length) return false;
                    for (int j = 0; j < xConflict.DevicesSerials.Length; j++)
                    {
                        if (xConflict.DevicesSerials[j]  != yConflict.DevicesSerials[j]) return false;
                    }
                }
                return true;
            }

            public int GetHashCode([DisallowNull] List<Conflict> obj)
            {
                throw new NotImplementedException();
            }
        }

        public static IEnumerable<object[]> DeviceInfoWithoutConflict()
        {
            //В группах нет приборов на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", false, "b100"},
                    new object[] {"d112", false, "b100"},
                    new object[] {"d111", false, "b101"}
                },
                Array.Empty<object>()
            };
            //Прибор не состоящий в группе находится на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", false, "b100"},
                    new object[] {"d112", false, "b100"},
                    new object[] {"d111", true, "b101"}
                },
                Array.Empty<object>()
            };
            //Два прибора не состоящие в группах находятся на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", false, "b100"},
                    new object[] {"d112", true, "b101"},
                    new object[] {"d113", true, "b102"}
                },
                Array.Empty<object>()
            };
        }

        public static IEnumerable<object[]> DeviceInfoWithOneConflict()
        {
            //Первый прибор в группе находится на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", true, "b100"},
                    new object[] {"d112", false, "b100"},
                    new object[] {"d111", false, "b101"}
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                }
            };
            //Второй прибор в группе находится на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", false, "b100"},
                    new object[] {"d112", true, "b100"},
                    new object[] {"d111", false, "b101"}
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                }
            };
            //Два прибора в группе находятся на связи
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", true, "b100"},
                    new object[] {"d112", true, "b100"},
                    new object[] {"d111", false, "b101"}
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                }
            };
            //Три прибора находятся на связи, один не состоит в группе
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", true, "b100"},
                    new object[] {"d112", true, "b100"},
                    new object[] {"d111", true, "b101"}
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                }
            };
        }
        public static IEnumerable<object[]> DeviceInfoMoreThenOneConflict()
        {
            //Две конфликтных группы приборов
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", true, "b100"},
                    new object[] {"d112", true, "b100"},
                    new object[] {"d113", false, "b101"},
                    new object[] {"d114", true, "b101"},
                    new object[] {"d115", false, "b102"},
                    new object[] {"d116", true, "b103"}
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                    new object[] { "b101", new string[] { "d113", "d114" } }
                }
            };
            //Три конфликтных группы приборов
            yield return new object[] {
                new object[]
                {
                    new object[] {"d111", true, "b100"},
                    new object[] {"d112", true, "b100"},
                    new object[] {"d113", false, "b101"},
                    new object[] {"d114", true, "b101"},
                    new object[] {"d115", false, "b102"},
                    new object[] {"d116", true, "b103"},
                    new object[] {"d117", false, "b104"},
                    new object[] {"d118", true, "b104"},
                    new object[] {"d119", false, "b104"},
                },
                new object[]
                {
                    new object[] { "b100", new string[] { "d111", "d112" } },
                    new object[] { "b101", new string[] { "d113", "d114" } },
                    new object[] { "b104", new string[] { "d117", "d118", "d119" } }
                }
            };
        }
    }
}
