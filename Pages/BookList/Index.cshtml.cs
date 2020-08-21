using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForBeginers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ForBeginers.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public IEnumerable<Book> Books { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            Books = await _dbContext.Books.ToListAsync();
        }


        public async Task<IActionResult> OnPostDelete(int id)
        {
            var bookToDelete = await _dbContext.Books.FindAsync(id);

            if (bookToDelete == null) return NotFound();

            _dbContext.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
