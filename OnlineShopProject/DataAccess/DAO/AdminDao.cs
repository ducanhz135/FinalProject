using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class AdminDao
    {
        OSFPDbContext db = null;
        public AdminDao()
        {
            db = new OSFPDbContext();
        }
        public long Insert(Admin entity)
        {
            db.Admins.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Admin entity)
        {
            try
            {
                var item = db.Admins.Find(entity.ID);
                item.FullName = entity.FullName;
                item.DoB = entity.DoB;
                item.Email = entity.Email;
                item.Phone = entity.Phone;
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
                var cus = db.Admins.Find(id);
                db.Admins.Remove(cus);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Admin ViewDetail(int id)
        {
            return db.Admins.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.Admins.SingleOrDefault(m => m.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Password == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public bool checkUsername(string username)
        {
            return db.Admins.Count(x => x.UserName == username) > 0;
        }

        public Admin getByUserName(string userName)
        {
            return db.Admins.SingleOrDefault(m => m.UserName == userName);
        }
    }
}