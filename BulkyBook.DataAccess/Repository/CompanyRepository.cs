using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository: Rpository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _db;
        public CompanyRepository(AppDbContext db) :base(db)
        {
            _db=db;
        }

        public void Update(Company company)
        {
            _db.Companies.Update(company);
        }
    }
}
