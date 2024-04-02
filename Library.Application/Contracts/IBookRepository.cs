﻿using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Contracts
{
    public interface  IBookRepository : IRepository<Book, int>
    {
    }
}