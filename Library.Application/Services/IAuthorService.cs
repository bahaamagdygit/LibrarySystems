using Library.Dtos.Author;
using Library.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public interface IAuthorService
    {
        Task<ResultDataList<AllAuthorDto>> GetAllAuthor();
    }
}
