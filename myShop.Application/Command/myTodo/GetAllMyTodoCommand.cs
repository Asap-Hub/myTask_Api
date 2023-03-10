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
    public class GetAllMyTodoCommand: IRequest<List<getTodoDto>>
    {
    }

    public class GetAllMyTodoCommandHandler : IRequestHandler<GetAllMyTodoCommand, List<getTodoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;

        public GetAllMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
               _repository = repository;
        }
        public async Task<List<getTodoDto>> Handle(GetAllMyTodoCommand request, CancellationToken cancellationToken)
        {
            FormattableString getResponse = $"EXEC [dbo].[spcGetAllMyToDo]";
            var get = await _repository.GetAll(getResponse);
            var getData = _mapper.Map<List<getTodoDto>>(get);
            return getData;
        }
    }
}
