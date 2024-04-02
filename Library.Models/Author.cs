using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Author : BaseEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public Author()
        {
            Books = new List<Book>();
        }
    }
   
}
