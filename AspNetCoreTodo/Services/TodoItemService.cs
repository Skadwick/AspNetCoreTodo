using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
namespace AspNetCoreTodo.Services
{

	/*
	 * This is the service class which does most of the interactions with the Database. 
	 * This work could be done in the controller, but it is good to separate functionalities.
	 */
	public class TodoItemService : ITodoItemService
	{
		private readonly ApplicationDbContext _context;

		public TodoItemService(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<TodoItem[]> GetIncompleteItemsAsync()
		{
			return await _context.Items
			.Where(x => x.IsDone == false)
			.ToArrayAsync();
		}

		public async Task<bool> AddItemAsync(TodoItem newItem)
		{
			newItem.Id = Guid.NewGuid();
			newItem.IsDone = false;
			newItem.DueAt = DateTimeOffset.Now.AddDays(3);
			_context.Items.Add(newItem);
			var saveResult = await _context.SaveChangesAsync();
			return saveResult == 1;
		}

		//Logic to apply checked todo item (item done) to the DB.
		public async Task<bool> MarkDoneAsync(Guid id)
		{
			var item = await _context.Items
			.Where(x => x.Id == id)
			.SingleOrDefaultAsync();

			if (item == null) return false;
			item.IsDone = true;
			var saveResult = await _context.SaveChangesAsync(); //the change only applies localy until SaveChangesAsync is called
			return saveResult == 1; // One entity should have been updated
		}



	}
}