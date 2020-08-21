using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForBeginers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForBeginers.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task OnGet(int id)
        {
            Book = await _dbContext.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var tempBook = await _dbContext.Books.FindAsync(Book.Id);

            tempBook.Name = Book.Name;
            tempBook.Author = Book.Author;
            tempBook.ISBN = Book.ISBN;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
