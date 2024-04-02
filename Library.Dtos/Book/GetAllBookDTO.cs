using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos.Book
{
    public class GetAllBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Price { get; set; }
        public string AuthorName { get; set; }
    }
}
