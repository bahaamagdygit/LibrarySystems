using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Context
{
    //لو هغير في ال user بتاعي 
    public class NewUser  :IdentityUser
    {
        public bool? IsDeleted { get; set; }

    }
    public class LibraryContext : IdentityDbContext<NewUser>
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
		public DbSet<Role> Role { get; set; }
		public DbSet<User> User { get; set; }
        public LibraryContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}
