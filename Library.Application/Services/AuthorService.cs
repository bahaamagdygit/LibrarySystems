using AutoMapper;
using Library.Application.Contracts;
using Library.Dtos.Author;
using Library.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _AuthorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _AuthorRepository = authorRepository;

        }

        public async Task<ResultDataList<AllAuthorDto>> GetAllAuthor() 
        {
            var AlldAta = (await _AuthorRepository.GetAllAsync());
            var Prds = AlldAta.Select(p => new AllAuthorDto()
            {
                Id = p.Id,
                Name = p.Name,
               

            }).ToList();
            ResultDataList<AllAuthorDto> resultDataList = new ResultDataList<AllAuthorDto>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        
    }
}
