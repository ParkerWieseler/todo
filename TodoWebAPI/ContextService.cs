﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI
{
    public class ContextService
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _config;
        public ContextService(ToDoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public bool AccountExists(int accountId)
        {
            return _context.Accounts.Find(accountId) == null ? false : true;
        }

        public bool ListExists(int listId)
        {
            return _context.Lists.Find(listId) == null ? false : true;
        }

        public void RemoveList(Lists list)
        {
            var todos = _context.ToDos.Where(t => t.ListId == list.Id).ToList();

            foreach (var todo in todos)
            {
                _context.ToDos.Remove(todo);
            }

            _context.Lists.Remove(list);
            _context.SaveChanges();
        }
    }
}