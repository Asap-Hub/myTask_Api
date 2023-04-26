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
    public class CreateMyTodoCommand: IRequest<int>
    { 
        public CreateTodoDto createDto { get; set; }
    }

    public class CreateMyTodoCommandHandler : IRequestHandler<CreateMyTodoCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;

        public CreateMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> Handle(CreateMyTodoCommand request, CancellationToken cancellationToken)
        {
           var dto = request.createDto;
            var entity = new TblMyTodo();
            //var entity = new TblMyTodo();

            var data = _mapper.Map(dto, entity);

            FormattableString sql = $"EXEC [dbo].[spcCreateMyToDo] @Title = {data.Title}, @Note = {data.Note}, @Status = {data.Status}, @startDate = {data.StartDate}, @endDate = {data.EndDate}";
            var createResponse = await _repository.Add(sql);
            return createResponse;
        }
    }
}
