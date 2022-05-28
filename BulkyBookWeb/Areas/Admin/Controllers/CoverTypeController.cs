using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _context;

        public CoverTypeController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objCatList = _context.CoverTypeRepository.GetAll();
            return View(objCatList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _context.CoverTypeRepository.Add(obj);
                _context.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var obj = _context.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

            if(obj is null) 
                return NotFound();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _context.CoverTypeRepository.Update(obj);
                _context.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var obj = _context.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);

            if (obj is null)
                return NotFound();
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj == null) 
                return NotFound();
           
            _context.CoverTypeRepository.Remove(obj);
            _context.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
