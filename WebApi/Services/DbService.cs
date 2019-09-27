using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services
{
    public class DbService:IdatabaseService

    {
        private readonly TodoContext _context;

        public DbService(TodoContext ctx)
        {
            _context = ctx;
        }
        public void add(string name)
        {
            _context.TodoItems.Add(new TodoItem { Name = name });
        }
    }
}
