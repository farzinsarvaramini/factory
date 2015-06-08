using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clientFactory.Models;

namespace clientFactory
{
   public class DbCenter
    {
        protected clientContainer clientDb;
        public DbCenter()
        {
            clientDb = new clientContainer();

        }
    }
}