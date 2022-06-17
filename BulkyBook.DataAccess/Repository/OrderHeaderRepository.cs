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
    public class OrderHeaderRepository: Rpository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly AppDbContext _db;
        public OrderHeaderRepository(AppDbContext db) :base(db)
        {
            _db=db;
        }


        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeader.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatuc = paymentStatus;
                }
            }
        }
    }
}
