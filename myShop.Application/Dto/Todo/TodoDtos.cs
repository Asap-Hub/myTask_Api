using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Dto.Todo
{
    public class CreateTodoDto
    {  
        public string? Title { get; set; }

        public string? Note { get; set; }

        public DateTime? StartDate { get; set; } 

        public DateTime EndDate { get; set; }
    }


    public class UpdateTodoDto
    {
        public int TodoId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class getTodoDto
    {
        public int TodoId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
