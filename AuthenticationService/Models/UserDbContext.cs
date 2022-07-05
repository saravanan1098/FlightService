using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class UserDbontext:DbContext
    {
        public UserDbontext(DbContextOptions<UserDbontext> options ):base(options)
        {

        }
        public  DbSet<User> Users { get; set; }
    }
}
