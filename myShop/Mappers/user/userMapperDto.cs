using AutoMapper;
using myShop.Application.Dto.User;
using myShop.Domain.Model;

namespace myShop.Api.Mappers.user
{
    public class userMapperDto: Profile
    {
        public userMapperDto()
        {
            CreateMap<CreateUserDto, TblAccount>().ReverseMap();
        }
    }
}
