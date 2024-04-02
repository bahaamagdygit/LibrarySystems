using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		[ForeignKey("Role")]
		public int Roleid { get; set; }
		public Role Role { get; set; }
	}
}
