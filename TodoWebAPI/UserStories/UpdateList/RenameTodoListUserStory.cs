﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Domain.Repositories;
using TodoWebAPI.Models;

namespace TodoWebAPI.UserStories
{
    public class RenameTodoListUserStory : IRequestHandler<UpdateList, TodoList>
    {
        private readonly ITodoListRepository _repository;

        public RenameTodoListUserStory(ITodoListRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoList> Handle(UpdateList request, CancellationToken cancellationToken)
        {
            var todoList = await _repository.FindTodoListIdByIdAsync(request.ListId);

            todoList.ListTitle = request.ListTitle;

            todoList.UpdateListName();

            await _repository.SaveChangesAsync();

            return todoList;
        }
    }
}
