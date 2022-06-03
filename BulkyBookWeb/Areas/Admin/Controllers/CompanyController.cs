using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _context;

        public CompanyController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Upsert(int? id)
        {
            Company company = new();
            
            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {
                company = _context.CompanyRepository.GetFirstOrDefault(p=>p.Id==id);
                return View(company);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _context.CompanyRepository.Add(obj);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _context.CompanyRepository.Update(obj);
                    TempData["success"] = "Product updated successfully";
                }

                
                _context.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _context.CompanyRepository.GetAll();
            return Json(new {data = companyList });
        }
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _context.CompanyRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj is null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            
            _context.CompanyRepository.Remove(obj);
            _context.Save();
            return Json(new { success = true, message = "Deleted successful" });
        }
        #endregion
    }
}
