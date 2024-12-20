﻿using AutoMapper;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.ViewModels;

namespace Car_Rental_web_App.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationViewModel, Reservation>().ReverseMap();
        }
    }
}
