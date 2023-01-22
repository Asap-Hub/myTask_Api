using AutoMapper;
using MediatR;
using myShop.Application.Dto.User;
using myShop.Application.Interface;
using myShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Application.Command.User
{
    public class GetloginCommand: IRequest<TblAccount>
    {
       public string? userName { set; get; } = null; 
        public string? firstPassword { set; get; }  = null;
        public string? confirmPassword { set; get; } = null;
    }

    public class GetloginCommandHandler : IRequestHandler<GetloginCommand, TblAccount>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblAccount> _repository;

        public GetloginCommandHandler(IMapper mapper, IGenericRepository<TblAccount> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<TblAccount> Handle(GetloginCommand request, CancellationToken cancellationToken)
        { 
            //var entity = new TblAccount();
            //var data = _mapper.Map(dto, entity);
            FormattableString sql = $"EXEC  [dbo].[spLogUserIN] @userName = {request.userName}, @firstPassword = {request.firstPassword},@confirmPassword = {request.confirmPassword}";

            var login = await _repository.Get(sql);
            if (login != null) { 
            _mapper.Map<TblAccount>(login);
            }
            return login;
        }
    }
}
