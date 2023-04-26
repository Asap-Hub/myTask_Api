using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Domain.Model
{
    public partial class TblMyTodo
    {
        public int TodoId { get; set; }

        public string? Title { get; set; }
        public string? Status { get; set; }

        public string? Note { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
