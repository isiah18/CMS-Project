using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface IStaticPageRepository
    {
        int Add(StaticPage sp);
        void Update(StaticPage sp);
        void Remove(int id);
        StaticPage GetById(int id);
        StaticPage GetByTitle(string name);
        List<StaticPage> GetAll();
    }
}
