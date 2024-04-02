using AutoMapper;
using Library.Dtos.Author;
using Library.Dtos.Book;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Mapper
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrUpdateBookDTO, Book>().ReverseMap();
            CreateMap<GetAllBookDTO, Book>().ReverseMap();
            CreateMap<AllAuthorDto, Author>().ReverseMap();
        }
    }
}
