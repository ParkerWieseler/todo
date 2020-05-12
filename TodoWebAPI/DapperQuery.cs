﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain;
using Todo.Domain.Repositories;
using System.Threading;
using System.Data.SqlClient;
using TodoWebAPI.Presentation;
using Dapper;
using Dapper.Transaction;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure;
using Todo.WebAPI.ApplicationServices;
using TodoWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TodoWebAPI.TypeHandlers;

namespace TodoWebAPI
{
    public class DapperQuery
    {
        private readonly string _connectionString;

        public DapperQuery(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings")["Development"];
        }
        public async Task<AccountPresentation> GetAccountAsync(Guid accountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<AccountPresentation>("SELECT * From Accounts Where ID = @accountId", new { accountId = accountId });

                return result.FirstOrDefault();
            }
        }

        public async Task<List<AccountContributorsPresentation>> GetContributorsAsync(Guid accountId)
        {
            List<AccountContributorsPresentation> contributors = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using(var transaction = await connection.BeginTransactionAsync())
                {
                    var contributorEmails = await transaction.QueryFirstOrDefaultAsync<List<string>>(@"
                        SELECT Contributors From Accounts WHERE ID = @accountId",
                        new { accountId });

                    if(contributorEmails == null)
                        return null;

                    var contributorsQueryResult = await transaction.QueryAsync<AccountContributorsPresentation>(@"
                        SELECT FullName, PictureUrl, Email From Accounts WHERE Email IN @contributorEmails",
                        new { contributorEmails = contributorEmails.ToArray() });

                    contributors = contributorsQueryResult.ToList();

                    transaction.Commit();
                }

                return contributors;
            }
        }

        public async Task<List<TodoListItemModel>> GetAllTodoItemAsync(Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListItemModel>("SELECT * From TodoListItems WHERE ListID = @listId", new { listId = listId });
                
                return result.ToList();
            }
        }

        public async Task<TodoListModel> GetListAsync(Guid accountId, Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListModel>("SELECT * FROM TodoLists as t INNER JOIN AccountLists as a ON t.ID = a.ListID WHERE a.AccountID = @accountId AND a.ListID = @listId", new { accountId = accountId, listId = listId });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<TodoListModel>> GetListsAsync(Guid accountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListModel>("SELECT * FROM TodoLists as t INNER JOIN AccountLists as a ON t.ID = a.ListID WHERE a.AccountID = @accountId", new { accountId = accountId });

                return result.ToList();
            }
        }

        public async Task<TodoListLayoutPresentation> GetTodoListLayoutAsync(Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<TodoListLayoutPresentation>("SELECT * FROM TodoListLayouts WHERE ListId = @listId", new { listId = listId });
                return result.FirstOrDefault();
            }
        }


        public async Task<List<SubItemModel>> GetSubItems(Guid listItemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<SubItemModel>("SELECT * FROM SubItems WHERE ListItemID = @listItemId", new { listItemId = listItemId });
                
                return result.ToList();
            }
        }

        public async Task<List<TodoListItem>> GetDueDatesFromListItemsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<TodoListItem>("SELECT * FROM TodoListItems WHERE DueDate IS NOT NULL");
                return result.ToList();
            }
        }

        public async Task<List<string>> GetEmailsFromAccountsByListIdAsync(Guid listId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<string>("SELECT Email FROM Accounts as a INNER JOIN AccountLists as l ON a.ID = l.AccountID and l.ListID = @listId", new { listId = listId });
                return result.ToList();
            }
        }

        public async Task<Guid> GetAccountIdByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.QueryAsync<Guid>("SELECT ID FROM Accounts WHERE Email = @email", new { email = email});
                return result.FirstOrDefault();
            }
        }
    }
}
