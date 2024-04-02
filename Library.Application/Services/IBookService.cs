using Library.Application.Contracts;
using Library.Dtos.Book;
using Library.Dtos.ViewResult;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services
{
    public interface IBookService
    {

        Task<ResultView<CreateOrUpdateBookDTO>> Create(CreateOrUpdateBookDTO book);
        Task<ResultView<CreateOrUpdateBookDTO>> Update(CreateOrUpdateBookDTO book);
        Task<ResultView<CreateOrUpdateBookDTO>> HardDelete(CreateOrUpdateBookDTO book);
        Task<ResultView<CreateOrUpdateBookDTO>> SoftDelete(CreateOrUpdateBookDTO book);
        Task<ResultDataList<GetAllBookDTO>> GetAllPagination(int items, int pagenumber); 
        Task<ResultDataList<GetAllBookDTO>> GetProductListByIdList(List<int> Ids);
        Task<CreateOrUpdateBookDTO> GetOne(int ID);
        




    }
}
