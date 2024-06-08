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
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserRolesVM>().ReverseMap();
            CreateMap<ApplicationUser, UserVM>().ReverseMap();
			#endregion

			#region Item
			CreateMap<Item, ItemDTO>().ReverseMap();    
            #endregion
        }
    }
}
