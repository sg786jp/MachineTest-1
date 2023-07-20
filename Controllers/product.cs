using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly Applicationcontext context;

        public ProductController(Applicationcontext context)
        {
            this.context = context;
        }
        public IActionResult addProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addProduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                await context.products.AddAsync(product);
                await context.SaveChangesAsync();
                TempData["succes"] = "product Has been created";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index(int pg = 1)
        {

            var data = (from p in context.products
                        join c in context.categories
                        on p.productid equals c.categoryid
                        select new ViewModel
                        {
                            productid = p.productid,
                            productname = p.productname,
                            categoryid = p.categoryid,
                            categoryname = c.categoryname,
                        }).ToList();
            const int pageSize = 3;
            if (pg < 1)
                pg = 1;

            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var datas = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            //return View(data);
            return View(datas);
        }
      
        

    }
}

        
