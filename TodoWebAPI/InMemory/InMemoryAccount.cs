﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.InMemory
{
    public class InMemoryAccount : IAccountRepository
    {
        public InMemoryAccount()
        {
            _accounts = new List<AccountModel>();
            _accounts.Add(new AccountModel() { Id = 1, FullName = "Parker", UserName = "parker", Picture = "", Password = "1234" });
            _list = new List<ListModel>();
            _list.Add(new ListModel() { Id = 1, AccountId = 1, ListTitle = "New List" });
            _todo = new List<ToDos>();
            _todo.Add(new ToDos() { Id = 1, Completed = false, ListId = 1, Notes = "", ToDoName = "yes" });
        }
        private List<AccountModel> _accounts;
        private List<ListModel> _list;
        private List<ToDos> _todo;
        public Task<AccountModel> CreateAccountAsync(AccountModel account)
        {
            account.Id = 1;
            _accounts.Add(account);
            return Task.FromResult(account);
        }

        public void DeleteAccountsAsync(int accountId)
        {
            var getAccount = _accounts.Find(x => x.Id == accountId);
            var getList = _list.Find(x => x.AccountId == accountId);

            if (getList == null)
            {
                _accounts.Remove(getAccount);
            }

            DeleteToDo(getList);
            _list.Remove(getList);
            _accounts.Remove(getAccount);
        }
        public Task<AccountModel> GetAccountAsync(int accountId)
        {
            var account = _accounts.Find(x => x.Id == accountId);

            return Task.FromResult(account);
        }

        public void DeleteToDo(ListModel list)
        {
            var todos = _todo.Find(x => x.ListId == list.Id);

            _todo.Remove(todos);
            _list.Remove(list);
        }
    }
}