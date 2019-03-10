using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi.DAL
{
    public   class LogHelper
    {

        public void Log(String s) {
            using (MyDbContext db = new MyDbContext()) {
                db.Event.Add(new Models.Event() {
                    EventDes=s,
                    EventDateTime=DateTime.Now
                });
                db.SaveChanges();
            }
            
        }

 
    }
}
