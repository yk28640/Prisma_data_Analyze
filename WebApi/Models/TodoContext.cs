using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Wheather> wheatherFort { get; set; }
        //public DbSet<Wheather> wheatherFortdfg { get; set; }
        //  public DbSet<string> Sti { set; get; } 

    }
}
