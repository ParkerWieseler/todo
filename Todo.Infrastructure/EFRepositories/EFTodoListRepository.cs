﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using Todo.Infrastructure;
using Todo.Infrastructure.Guids;

namespace TodoWebAPI.Data
{
    public class EFTodoListRepository : ITodoListRepository
    {
        private readonly ISequentialIdGenerator _idGenerator;

        private TodoDatabaseContext _context { get; set; }
        public EFTodoListRepository(TodoDatabaseContext context, ISequentialIdGenerator idGenerator)
        {
            _context = context;
            _idGenerator = idGenerator;
        }
        public Task AddTodoListAsync(TodoList todoList, Guid accountId)
        {
            _context.TodoLists.Add(todoList);
            var accountLists = new RoleOwner()
            {
                Id = _idGenerator.NextId(),
                AccountId = accountId,
                ListId = todoList.Id,
            };

            accountLists.Owned();

            _context.AccountsLists.Add(accountLists);
            return Task.CompletedTask;
        }

        public Task<List<TodoList>> FindTodoListsByAccountIdAsync(Guid accountId, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<TodoList> FindTodoListIdByIdAsync(Guid listId)
        {
            return await _context.TodoLists.FindAsync(listId);
        }
        public async Task RemoveTodoListAsync(Guid listId)
        {
            var list = await _context.TodoLists.FindAsync(listId);

            _context.Remove(list);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Guid NextId()
        {
           return _idGenerator.NextId();
        }

        public Task AddInvitedRowToAccountListsAsync(Guid accountId, Guid listId)
        {
            var accountLists = new RoleInvited()
            {
                AccountId = accountId,
                ListId = listId,
                Id = _idGenerator.NextId()
            };

            accountLists.Invited();

            _context.AccountsLists.Add(accountLists);
            return Task.CompletedTask;
        }

        public void UpdateListAsync(TodoList list)
        {
            _context.TodoLists.Update(list);
        }

        public Task AddContributorRowToAccountsListsAsync(Guid accountId, Guid listId)
        {
             var accountsLists = new RoleContributor()
            {
                AccountId = accountId,
                ListId = listId,
            };
            accountsLists.Contributed();

            _context.AccountsLists.Add(accountsLists);
            return Task.CompletedTask;
        }

        public Task AddDeclinedRowToAccountsListsAsync(Guid accountId, Guid listId)
        {
            var accountsLists = new RoleDecline()
            {
                AccountId = accountId,
                ListId = listId,
            };
            accountsLists.Decline();

            _context.AccountsLists.Add(accountsLists);
            return Task.CompletedTask;
        }

        public Task AddLeftRowToAccountsListsAsync(Guid accountId, Guid listId)
        {
           var accountsLists = new RoleLeft()
            {
                AccountId = accountId,
                ListId = listId,
            };
            accountsLists.Left();

            _context.AccountsLists.Add(accountsLists);
            return Task.CompletedTask;
        }
    }
}