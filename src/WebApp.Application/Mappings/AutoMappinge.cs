using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApp.Application.Models.User;
using WebApp.Domain.Entities;

namespace WebApp.Application.Mappings
{
    public class AutoMappinge : Profile
    {
        public AutoMappinge()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, CreateUserDTO>();
        }
    }
}
