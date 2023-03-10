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
    public class GetMyTodoCommand:IRequest<getTodoDto>
    {
        public int ID { get; set; } 
    }

    public class GetMyTodoCommandHandler : IRequestHandler<GetMyTodoCommand, getTodoDto>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;

        public GetMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<getTodoDto> Handle(GetMyTodoCommand request, CancellationToken cancellationToken)
        {
            FormattableString getResponse = $"EXEC [dbo].[spcGetMyToDo] @Id = {request.ID}";
            var get = await _repository.Get(getResponse);
            var getData = _mapper.Map<getTodoDto>(get);
            return getData;
        }
    }
}
