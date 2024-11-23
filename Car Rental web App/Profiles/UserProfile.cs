using AutoMapper;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.ViewModels;

namespace Car_Rental_web_App.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, User>().ReverseMap();
            CreateMap<UpdateViewModel, User>().ReverseMap();

        }
    }
}
