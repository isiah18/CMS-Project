using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentManagementSystem.Model;

namespace ContentManagementSystem.Data.Contracts
{
    public interface ITagRepository
    {
        int Add(string name);
        void Update(Tag t);
        void Remove(int id);
        Tag GetById(int id);
        Tag GetByName(string name);
        List<Tag> GetAll();
    }
}
