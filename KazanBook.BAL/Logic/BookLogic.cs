using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KazanBook.DAL;
using KazanBook.DAL.DataAccess;
using KazanBook.DAL.Entities;

namespace KazanBook.BAL.Logic
{
    public class BookLogic
    {
        public static async Task<List<Book>> GetAll()
        {
            Response<List<Book>> resp = await BookEngine.GetAllAsync();
            return Passer.Pass(resp);
        }
        public static async Task<Book> GetById(Guid id)
        {
            Response<Book> resp = await BookEngine.GetByIdAsync(id);
            return Passer.Pass(resp);
        }
        public static async Task<string> Create(Book book)
        {
            Book newBook = new Book()
            {
                Title = Passer.DBString(book.Title),
                AuthorId = book.AuthorId,
                Tags = Passer.DBArray(book.Tags)
            };
            Response<string> resp = await BookEngine.CreateAsync(newBook);
            return Passer.Pass(resp);
        }
        public static async Task<string> Update(Book book)
        {
            Book newBook = new Book()
            {
                Id = book.Id,
                Title = Passer.DBString(book.Title),
                AuthorId = book.AuthorId,
                Tags = Passer.DBArray(book.Tags)
            };
            Response<string> resp = await BookEngine.CreateAsync(newBook);
            return Passer.Pass(resp);
        }
        public static async Task<string> Delete(Guid id)
        {
            Response<string> resp = await BookEngine.DeleteAsync(id);
            return Passer.Pass(resp);
        }
    }
}
