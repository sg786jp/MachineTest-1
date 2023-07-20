using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Applicationcontext context;

        public CategoryController(Applicationcontext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()

        {
            var data = await context.categories.ToListAsync();

            return View(data);
        }

        public IActionResult addCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addCategory(category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
               await context.categories.AddAsync(category);
               await context.SaveChangesAsync();
               TempData["succes"] = "Category Has been created";
            }
            return RedirectToAction("Index");
        }
    }
}
