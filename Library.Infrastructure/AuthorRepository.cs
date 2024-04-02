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
    public class AuthorRepository : Repository<Author , int> , IAuthorRepository
    {
        public AuthorRepository(LibraryContext libraryContextt) : base(libraryContextt)
        {

        }
    }
}
