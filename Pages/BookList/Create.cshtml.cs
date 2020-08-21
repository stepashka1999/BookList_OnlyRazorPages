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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        [BindProperty]
        public Book Book { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            
            await _dbContext.Books.AddAsync(Book);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
