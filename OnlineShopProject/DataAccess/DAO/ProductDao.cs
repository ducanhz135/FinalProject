using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class ProductDao
    {
        OSFPDbContext db = null;
        public ProductDao()
        {
            db = new OSFPDbContext();

        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Product entity)
        {
            try
            {
                var item = db.Products.Find(entity.ID);
                item.Name = entity.Name;
                item.Code = entity.Code;
                item.MetaTitle = entity.MetaTitle;
                item.Description = entity.Description;
                item.Image = entity.Image;
                item.Price = entity.Price;
                item.PromotionPrice = entity.PromotionPrice;
                item.IncludedVAT = entity.IncludedVAT;
                item.Quantity = entity.Quantity;
                item.CategoryID = entity.CategoryID;
                item.Warranty = entity.Warranty;
                item.DisplayOrder = entity.DisplayOrder;
                item.Status = entity.Status;
                item.Language = entity.Language;
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
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> ListAll()
        {
            return db.Products.Where(m => m.Status == true).ToList();
        }
    }
}