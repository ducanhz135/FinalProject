using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class CategoryDao
    {
        OSFPDbContext db = null;
        public CategoryDao()
        {
            db = new OSFPDbContext();
        }
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Category entity)
        {
            try
            {
                var item = db.Categories.Find(entity.ID);
                item.Name = entity.Name;
                item.MetaTitle = entity.MetaTitle;
                item.ParentID = entity.ParentID;
                item.DisplayOrder = entity.DisplayOrder;
                item.Status = entity.Status;
                item.ShowOnHome = entity.ShowOnHome;
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
                var cate = db.Categories.Find(id);
                db.Categories.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }

        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(m => m.Name.Contains(searchString));
            }

            return model.OrderByDescending(m => m.CreatedDate).ToPagedList(page, pageSize);
        }

        public Category FindCategory(int ID)
        {
            return db.Categories.Find(ID);
        }
    }
}