using ForSomaBookStore.Models;

namespace ForSomaBookStore.Services.Interfaces
{
    public interface ITextbookService
    {
        Task<List<Textbook>> GetAllAsync();

        Task<Textbook> GetByIdAsync(int id);

        Task CreateAsync(Textbook textbook);

        Task UpdateAsync(Textbook textbook);

        Task DeleteAsync(int id);

        Task<List<Textbook>> GetByUserIdAsync(string userId);
    }
}
