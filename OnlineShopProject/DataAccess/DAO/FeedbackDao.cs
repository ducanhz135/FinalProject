using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DAO
{
    public class FeedbackDao
    {

        OSFPDbContext db = null;
        public FeedbackDao()
        {
            db = new OSFPDbContext();
        }

        public int InsertFeedback(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}