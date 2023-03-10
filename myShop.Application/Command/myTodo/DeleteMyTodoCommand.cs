using AutoMapper;
using MediatR;
using myShop.Application.Interface;
using myShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Command.myTodo
{
    public class DeleteMyTodoCommand: IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteMyTodoCommandHandler : IRequestHandler<DeleteMyTodoCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblMyTodo> _repository;
        public DeleteMyTodoCommandHandler(IMapper mapper, IGenericRepository<TblMyTodo> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> Handle(DeleteMyTodoCommand request, CancellationToken cancellationToken)
        {
            FormattableString sql = $"EXEC [dbo].[spcDeleteMyToDo] @Id = {request.Id}";
            var deleteData = await _repository.Delete(sql);
            return deleteData;
        }
    }
}
