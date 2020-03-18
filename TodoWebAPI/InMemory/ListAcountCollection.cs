﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebAPI.Data;
using TodoWebAPI.Interfaces;
using TodoWebAPI.Models;

namespace TodoWebAPI.InMemory
{
    public class ListAcountCollection : IAccountCollection
    {
        private List<CreateAccountModel> _accounts = new List<CreateAccountModel>();

        public Task CreateAccount(CreateAccountModel account)
        {
            _accounts.Add(account);
            return Task.FromResult(account);
        }

        public void DeleteAccounts(int accountId)
        {
            var value = _accounts.Find(x => x.Id == accountId);

            _accounts.Remove(value);
        }
        public Task GetAccount(int accountId)
        {
            var value = _accounts.Find(x => x.Id == accountId);

            return Task.FromResult(value);
        }
    }
}