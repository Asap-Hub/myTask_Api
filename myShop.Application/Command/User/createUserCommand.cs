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
    public class createUserCommand: IRequest<int>
    {
        public CreateUserDto createUserDto { get; set; }
    }


    public class createUserCommandHandler : IRequestHandler<createUserCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TblAccount> _repository;

        public createUserCommandHandler( IMapper mapper, IGenericRepository<TblAccount> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> Handle(createUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.createUserDto;
            var entity = new TblAccount();
            var data = _mapper.Map(dto, entity);
            FormattableString sql = $"EXEC  [dbo].[spCreateAccount] @firstName = {data.FirstName}, @secondName = {data.SecondName}, @userName = {data.UserName}, @gender = {data.Gender}, @countryName = {data.CountryName}, @firstPassword = {data.FirstPassword}, @confirmPassword = {data.ConfirmPassword}";

            var result = await _repository.Add(sql);
              return result;

        }
    }
}
