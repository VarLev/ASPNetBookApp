using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = _context.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                CoverTypeList = _context.CoverTypeRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _context.ProductRepository.GetFirstOrDefault(p=>p.Id==id);
                return View(productVM);
            }
                
            
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (!string.IsNullOrEmpty(wwwRoot))
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRoot, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if ( !string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRoot, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var stream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + filename + extension;
                }

                if (obj.Product.Id == 0)
                {
                    _context.ProductRepository.Add(obj.Product);
                }
                else
                {
                    _context.ProductRepository.Update(obj.Product);
                }

                
                _context.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #region APICALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _context.ProductRepository.GetAll(includeProp:"Category");
            return Json(new {data = productList});
        }
        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _context.ProductRepository.GetFirstOrDefault(c => c.Id == id);
            if (obj is null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _context.ProductRepository.Remove(obj);
            _context.Save();
            return Json(new { success = true, message = "Deleted successful" });
        }
        #endregion
    }
}
