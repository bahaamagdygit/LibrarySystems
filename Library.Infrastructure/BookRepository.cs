using Library.Application.Contracts;
using Library.Context;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        public BookRepository(LibraryContext libraryContext) : base(libraryContext)
        {

        }
    }
}

