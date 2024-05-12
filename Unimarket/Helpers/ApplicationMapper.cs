using AutoMapper;
using Unimarket.Core.Entities;
using Unimarket.Core.Models;

namespace Unimarket.API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            #region User
            CreateMap<ApplicationUser, UserSignUp>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            #endregion
        }
    }
}
