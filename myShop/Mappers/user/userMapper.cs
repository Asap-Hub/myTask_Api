using AutoMapper;
using myShop.Application.Dto.User;
using myShop.Domain.Model;

namespace myShop.Api.Mappers.user
{
    public class userMapper: Profile
    {
        public userMapper()
        {
            CreateMap<CreateUserDto, TblAccount>().ReverseMap();
            CreateMap<loginDto, TblAccount>().ReverseMap();
        }                                                       
    }
}
