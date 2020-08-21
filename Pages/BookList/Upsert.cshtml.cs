using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForBeginers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForBeginers.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Book Book { get; set; }
        public UpsertModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id != null)
                Book = await _dbContext.Books.FindAsync(id);
            else
                Book = new Book();

            if (Book == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if (Book.Id == 0) await _dbContext.Books.AddAsync(Book);
            else _dbContext.Books.Update(Book);

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
