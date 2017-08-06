using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ICategoryRepository
    {
        int Add(string name);
        void Update(Category t);
        void Remove(int id);
        Category GetById(int id);
        Category GetByName(string name);
        List<Category> GetAll();
    }
}
