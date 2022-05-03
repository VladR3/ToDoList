using Business.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.providers
{
        public interface ICategoryProvider
        {
            void Create(Category category);
            void Delete(int id);
            public Category Get(int id);
            public List<Category> GetCategories();
            void Update(Category category);
        }
}
