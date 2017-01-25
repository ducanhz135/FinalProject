using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class OrderDao
    {
        OSFPDbContext db = null;
        public OrderDao()
        {
            db = new OSFPDbContext();
        }

        public long insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}