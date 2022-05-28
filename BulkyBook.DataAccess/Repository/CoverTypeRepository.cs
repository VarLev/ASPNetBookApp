using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository:Rpository<CoverType>, ICoverTypeRepository
    {
        private readonly AppDbContext _db;
        public CoverTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType cover)
        {
            _db.CoverType.Update(cover);
        }
    }
}
