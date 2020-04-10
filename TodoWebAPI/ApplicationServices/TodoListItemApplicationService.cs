﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.DomainEvents;
using Todo.Domain.Repositories;

namespace Todo.WebAPI.ApplicationServices
{
    public class TodoListItemApplicationService
    {
        private readonly ITodoListRepository _listRepository;
        private readonly ITodoListItemRepository _listItemRepository;

        public TodoListItemApplicationService(ITodoListRepository listRepository, ITodoListItemRepository todoListItemRepository)
        {
            _listRepository = listRepository;
            _listItemRepository = todoListItemRepository;
        }

        public async Task<TodoListItem> CreateTodoListItemAsync(int listId, int accountId, string todoName, string notes, DateTime? dueDate)
        {
            var list = await _listRepository.FindTodoListIdByIdAsync(listId);

            if (list == null)
                return null;

            var todoItem = list.CreateListItem(todoName, notes, dueDate);

            await _listItemRepository.AddTodoListItemAsync(todoItem);

            await _listItemRepository.SaveChangesAsync();

            return todoItem;
        }

        public async Task UpdateTodoListItemAsync(int todoListItemId, string notes, string todoName, DateTime? dueDate)
        {
            var todoListItem = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            todoListItem.Notes = notes;
            todoListItem.Name = todoName;
            todoListItem.DueDate = dueDate;
        }

        public async Task DeleteTodoListItem(int todoListItemId)
        {
            await _listItemRepository.RemoveTodoListItemAsync(todoListItemId);
        }

        public async Task MarkTodoListItemAsCompletedAsync(int todoListItemId, bool state)
        {
            var item = await _listItemRepository.FindToDoListItemByIdAsync(todoListItemId);

            if(state == true)
            {
                item.SetCompleted();
            }
            else if(state == false)
            {
                item.SetNotCompleted();
            }

            await _listItemRepository.SaveChangesAsync();
        }
    }
}
