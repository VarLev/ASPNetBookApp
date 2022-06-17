using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
            CoverTypeRepository = new CoverTypeRepository(_db);
            ProductRepository = new ProductRepository(_db);
            CompanyRepository = new CompanyRepository(_db);
            ShoppingCartRepository = new ShoppingCartRepository(_db);
            ApplicationUserRepository = new ApplicationUserRepository(_db);
            OrderDetailRepository = new OrderDetailRepository(_db);
            OrderHeaderRepository = new OrderHeaderRepository(_db);
        }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICoverTypeRepository CoverTypeRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public ICompanyRepository CompanyRepository { get; }
        public IShoppingCartRepository ShoppingCartRepository { get; }
        public IApplicationUserRepository ApplicationUserRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IOrderHeaderRepository OrderHeaderRepository { get; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
