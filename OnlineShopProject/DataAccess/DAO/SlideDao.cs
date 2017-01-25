using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class SlideDao
    {
        OSFPDbContext db = null;
        public SlideDao()
        {
            db = new OSFPDbContext();
        }
    }
}