using ForSomaBookStore.Data;
using ForSomaBookStore.Models;
using ForSomaBookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Services.Implementations;

public class TextbookService(ApplicationDbContext context) : ITextbookService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Textbook>> GetAllAsync()
    {
        return await _context.Textbooks.ToListAsync();
    }

    public async Task<Textbook> GetByIdAsync(int id)
    {
        var book = await _context.Textbooks.FindAsync(id);
        return book is null ? throw new KeyNotFoundException($"Textbook with id {id} not found.") : book;
    }

    public async Task CreateAsync(Textbook textbook)
    {
        _context.Textbooks.Add(textbook);
        await _context.SaveChangesAsync(); //ERROR 
    }

    public async Task UpdateAsync(Textbook textbook)
    {
        _context.Textbooks.Update(textbook);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Textbooks.FindAsync(id);

        if (book != null)
        {
            _context.Textbooks.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}