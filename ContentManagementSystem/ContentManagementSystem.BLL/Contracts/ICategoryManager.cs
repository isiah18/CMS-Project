using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.BLL.Contracts
{
    public interface ICategoryManager
    {
        Response<int> Add(string name);
        Response<Category> Update(Category t);
        Response<int> Remove(int id);
        Response<Category> GetById(int id);
        Response<Category> GetByName(string name);
        Response<List<Category>> GetAll();
    }
}
