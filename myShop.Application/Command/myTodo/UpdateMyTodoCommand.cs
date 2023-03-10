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
    public class UpdateMyTodoCommand:IRequest<int>
    {
        public UpdateTodoDto updateDto { get; set; }
    }

    public class UpdateMyTodoCommandHandler : IRequestHandler<UpdateMyTodoCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;

        public UpdateMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> Handle(UpdateMyTodoCommand request, CancellationToken cancellationToken)
        {
            var dto = request.updateDto;
            var data =  _mapper.Map<TblMyTodo>(dto);
            FormattableString sql = $"EXEC  [dbo].[spcUpdateMyToDo] @TodoName = {data.TodoName}, @StartTime = {data.StartTime}, @EndTime = {data.EndTime}, @Note = {data.Note}, @Id = {data.Id}";

            var updateData = await _repository.Update(sql);
            return updateData;


        }
    }
}
