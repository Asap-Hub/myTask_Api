using AutoMapper;
using MediatR;
using myShop.Application.Dto.Todo;
using myShop.Application.Interface;
using myShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Command.myTodo
{
    public class CreateMyTodoCommand: IRequest<TblMyTodo>
    { 
        public CreateTodoDto createDto { get; set; }
    }

    public class CreateMyTodoCommandHandler : IRequestHandler<CreateMyTodoCommand, TblMyTodo>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;

        public CreateMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<TblMyTodo> Handle(CreateMyTodoCommand request, CancellationToken cancellationToken)
        {
           var dto = request.createDto;
            //var entity = new TblMyTodo();

            var data = _mapper.Map<TblMyTodo>(dto);

            FormattableString sql = $"EXEC [dbo].[spcCreateMyToDo] @TodoName = {data.TodoName}, @StartDate = {data.StartTime}, @EndDate = {data.EndTime}, @Note = {data.Note}";
            var createResponse = await _repository.Add(sql);
            return createResponse;
        }
    }
}
