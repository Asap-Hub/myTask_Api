using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Domain.Model
{
    public partial class TblMyTodo
    {
        public int Id { get; set; }
        public string? TodoName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Note { get; set; }


    }
}
