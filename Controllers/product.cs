using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList.Mvc;
using PagedList;

namespace MachineTest.Controllers
{
    public class product : Controller
    {
        private readonly Applicationcontext context;

        public product(Applicationcontext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pg=1)
        {
            
            var data = (from p in context.products
                       join c in context.categories
                       on p.productid equals c.categoryid
                       select new ViewModel
                       {
                           productid = p.productid,
                           productname = p.productname,
                           categoryid = p.categoryid,
                           categoryname=c.categoryname,
                       }).ToList();
            const int pageSize = 3;
            if (pg < 1)
                pg = 1;

            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg-1)*pageSize;
            var datas = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;


            //return View(data);
            return View(datas);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(ViewModel model)
        {
            if(ModelState.IsValid)
            {
                var db = new ViewModel()
                {
                    productname = model.productname,
                    categoryname = model.categoryname,
                };
              context.viewModel.Add(db);
                context.SaveChanges();
              return RedirectToAction("Index");
              
            }
            else
            {
                TempData["error"] = "Empty field can't submit";
                return View(model);
            }
            
        }

    }
}
