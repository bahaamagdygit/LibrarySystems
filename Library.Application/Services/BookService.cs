using AutoMapper;
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
    public class BookService : IBookService
    {
        private IBookRepository _BookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository BookRepository , IMapper mapper)
        {
            _BookRepository = BookRepository;
            _mapper = mapper;
        }


        public async Task<ResultView<CreateOrUpdateBookDTO>> Create(CreateOrUpdateBookDTO book)
        {
            var Query = (await _BookRepository.GetAllAsync());
            var OldBook = Query.Where(p => p.Title == book.Title).FirstOrDefault();
            if (OldBook != null)
            {
                return new ResultView<CreateOrUpdateBookDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var book1 = _mapper.Map<Book>(book);
                var NewBook = await _BookRepository.CreateAsync(book1);
                await _BookRepository.SaveChangesAsync();
                var BookDto = _mapper.Map<CreateOrUpdateBookDTO>(NewBook);
                return new ResultView<CreateOrUpdateBookDTO> { Entity = BookDto, IsSuccess = true, Message = "Updated Successfully" };
            }

        }


        public async Task<ResultView<CreateOrUpdateBookDTO>> Update( CreateOrUpdateBookDTO book)
        {
            var oldBook = (await _BookRepository.GetAllAsync()).Where(p => p.Title == book.Title && p.Id != book.Id).FirstOrDefault();
            if (oldBook != null)
            {
                return new ResultView<CreateOrUpdateBookDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var prd = _mapper.Map<Book>(book);
                var NewBook = await _BookRepository.UpdateAsync(prd);
                await _BookRepository.SaveChangesAsync();
                var newookDTO = _mapper.Map<CreateOrUpdateBookDTO>(NewBook);
                return new ResultView<CreateOrUpdateBookDTO> { Entity = newookDTO, IsSuccess = true, Message = "Updated Successfully" };

            }


        }


        public async Task<ResultView<CreateOrUpdateBookDTO>> HardDelete(CreateOrUpdateBookDTO book)
        {
            try
            {
                var book1 = _mapper.Map<Book>(book);
                var OldBook = _BookRepository.DeleteAsync(book1);
                await _BookRepository.SaveChangesAsync();
                var BookDto = _mapper.Map<CreateOrUpdateBookDTO>(OldBook);
                return new ResultView<CreateOrUpdateBookDTO> { Entity = BookDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateBookDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }




        public async Task<ResultView<CreateOrUpdateBookDTO>> SoftDelete(CreateOrUpdateBookDTO book)
        {
            try
            {
                var book1 = _mapper.Map<Book>(book);
                var OldBook = (await _BookRepository.GetAllAsync()).FirstOrDefault(p => p.Id == book.Id);
                OldBook.IsDeleted = true;
                await _BookRepository.SaveChangesAsync();
                var BookDto = _mapper.Map<CreateOrUpdateBookDTO>(OldBook);
                return new ResultView<CreateOrUpdateBookDTO> { Entity = BookDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateBookDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }




        public async Task<ResultDataList<GetAllBookDTO>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var Alldata = (await _BookRepository.GetAllAsync());
            var books = Alldata.Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllBookDTO()
                                              {
                                                  Id = p.Id,
                                                  Title = p.Title,
                                                  Price = p.Price,
                                                  ISBN = p.ISBN,
                                                  
                                                  AuthorName = p.Author.Name
                                              }).ToList();
            ResultDataList<GetAllBookDTO> resultDataList = new ResultDataList<GetAllBookDTO>();
            resultDataList.Entities = books;
            resultDataList.Count = Alldata.Count();
            return resultDataList;
        }






        public async Task<CreateOrUpdateBookDTO> GetOne(int ID)
        {
            var book1 = await _BookRepository.GetByIdAsync(ID);
            var REturnBook = _mapper.Map<CreateOrUpdateBookDTO>(book1);
            return REturnBook;
        }



        public async Task<ResultDataList<GetAllBookDTO>> GetProductListByIdList(List<int> Ids)
        {

            var AlldAta = (await _BookRepository.GetAllAsync());
            var Prds = AlldAta.Where(x => Ids.Contains(x.Id)).Select(p => new GetAllBookDTO()
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                ISBN= p.ISBN,
                AuthorName = p.Author.Name
            }).ToList();
            ResultDataList<GetAllBookDTO> resultDataList = new ResultDataList<GetAllBookDTO>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
    }
}
