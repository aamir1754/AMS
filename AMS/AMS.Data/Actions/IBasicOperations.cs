using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Data.Actions
{
    public interface IBasicOperations<Entity> where Entity : new()
    {
        int Add(Entity t);
        Entity Get(int id);
        IEnumerable<Entity> GetAll();
        void Delete(int id);
        void Update(Entity t);
    }
}