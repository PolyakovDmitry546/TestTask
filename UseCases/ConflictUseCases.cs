using Infrastructure;
using Infrastructure.DTOs;


namespace UseCases
{
    public class ConflictUseCases
    {
        private readonly IConflictRepository repository;
        public ConflictUseCases(IConflictRepository repository) 
        {
            this.repository = repository;
        }

        public void SaveConflicts(List<Conflict> conflicts) 
        {
            foreach (var conflict in conflicts)
            {
                repository.Insert(conflict);
            }
            repository.Save();
        }
    }
}
