using Infrastructure.DTOs;


namespace Infrastructure
{
    public interface IConflictRepository
    {
        void Insert(Conflict conflict);

        void Save();
    }
}
