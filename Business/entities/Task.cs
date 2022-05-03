using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.entities
{
    public class Task
    {
        public int idTask { get; set; }
        public string text { get; set; }
        public DateTime dateOfCreate { get; set; }
        public int mark { get; set; }
        public int idCategory { get; set; }
        public int status { get; set; }
        public DateTime deadLine { get; set; }
    }
}
