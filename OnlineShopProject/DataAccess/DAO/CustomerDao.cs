using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class CustomerDao
    {
        OSFPDbContext db = null;
        public CustomerDao()
        {
            db = new OSFPDbContext();
        }
        public long Insert(Customer entity)
        {
            db.Customers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Customer entity)
        {
            try
            {
                var item = db.Customers.Find(entity.ID);
                item.FullName = entity.FullName;
                item.Address = entity.Address;
                item.Email = entity.Email;
                item.ModifiedBy = entity.ModifiedBy;
                item.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cus = db.Customers.Find(id);
                db.Customers.Remove(cus);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Customer ViewDetail(int id)
        {
            return db.Customers.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.Customers.SingleOrDefault(m => m.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Locked == true)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }

        public bool Unlocked(long id)
        {
            var cus = db.Customers.Find(id);
            cus.Locked = !cus.Locked;
            db.SaveChanges();
            return cus.Locked;
        }

        public bool checkUsername(string username)
        {
            return db.Customers.Count(x => x.UserName == username) > 0;
        }

    }
}