using ForSomaBookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForSomaBookStore.Controllers;

[Authorize]
public class TransactionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Transactions.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(x => x.Id == id);

        if (transaction == null)
            return NotFound();

        return View(transaction);
    }
}