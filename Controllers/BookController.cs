using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForBeginers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForBeginers.Controllers
{
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll ()
        {
            return Json(new { data = await _dbContext.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _dbContext.Books.FindAsync(id);

            if(bookToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _dbContext.Books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();

            return Json(new { success = true, message="Delete successful" });
        }
    }
}
