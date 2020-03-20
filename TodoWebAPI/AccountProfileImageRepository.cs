﻿using System;
using TodoWebAPI.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TodoWebAPI
{
    public class AccountProfileImageRepository
    {
        private readonly string _connectionString;

        public AccountProfileImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public byte[] ConvertStringToByteArray(string image)
        {
            byte[] imageBytes = Convert.FromBase64String(image);
            return imageBytes;
        }

        public async Task StoreImageProfileAsync(AccountModel account)
        {
            var id = account.Id;
            var s = account.Picture;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"Update Accounts SET Picture = @pic WHERE ID = @id";

                    var pic = ConvertStringToByteArray(s);

                    command.Parameters.AddWithValue(@"pic", pic);
                    command.Parameters.AddWithValue(@"id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
