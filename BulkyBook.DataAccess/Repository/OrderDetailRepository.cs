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
    public class OrderDetailRepository: Rpository<OrderDetail>, IOrderDetailRepository
    {
        private readonly AppDbContext _db;
        public OrderDetailRepository(AppDbContext db) :base(db)
        {
            _db=db;
        }


        public void Update(OrderDetail orderDetail)
        {
            _db.OrderDetail.Update(orderDetail);
        }
    }
}
