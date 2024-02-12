using System.Text.Json;

using Infrastructure.DTOs;


namespace Infrastructure
{
    public class JsonConflictRepository : IConflictRepository
    {
        private readonly List<Conflict> conflicts;
        private readonly string path;
        public JsonConflictRepository(string path) 
        {
            this.path = path;
            conflicts = new List<Conflict>();
        }

        public void Insert(Conflict conflict)
        {
            conflicts.Add(conflict);
        }

        public void Save()
        {
            var jsonString = JsonSerializer.Serialize(
                conflicts,
                options: new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );
            File.WriteAllText(path, jsonString);
        }
    }
}
