using AutoMapper;
using myShop.Application.Dto.Todo;
using myShop.Domain.Model;

namespace myShop.Api.Mappers.Todo
{
    public class TodoMapper: Profile
    {
        public TodoMapper()
        {
            CreateMap<CreateTodoDto, TblMyTodo>().ReverseMap();
            CreateMap<UpdateTodoDto, TblMyTodo>().ReverseMap();
            CreateMap<getTodoDto, TblMyTodo>().ReverseMap();
        }
    }
}
