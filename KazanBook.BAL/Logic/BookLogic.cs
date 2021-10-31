using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KazanBook.BAL.Schema;
using KazanBook.DAL.DataAccess;

namespace KazanBook.BAL.Logic
{
    public class BookLogic
    {
        public static async Task<object> GetAll()
        {
            Response<List<Book>> resp = Passer.Convert<Response<List<Book>>>(await BookEngine.GetAllAsync());
            return Passer.Pass(resp);
        }
        public static async Task<object> GetById(Guid id)
        {
            Response<Book> resp = (Response<Book>)await BookEngine.GetByIdAsync(id);
            return Passer.Pass(resp);
        }
        public static async Task<object> Create(object bookObject)
        {
            Response<Book> newBookUnsecure = (Response<Book>)bookObject;
            Book newBook = new Book()
            {
                Title = Passer.DBString(newBookUnsecure.data.Title),
                AuthorId = newBookUnsecure.data.AuthorId,
                Tags = Passer.DBArray(newBookUnsecure.data.Tags)
            };
            Response<Book> resp = (Response<Book>)await BookEngine.CreateAsync(newBook);
            return Passer.Pass(resp);
        }
        public static async Task<object> Update(object bookObject)
        {
            Response<Book> newBookUnsecure = (Response<Book>)bookObject;
            Book newBook = new Book()
            {
                Id = newBookUnsecure.data.Id,
                Title = Passer.DBString(newBookUnsecure.data.Title),
                AuthorId = newBookUnsecure.data.AuthorId,
                Tags = Passer.DBArray(newBookUnsecure.data.Tags)
            };
            Response<Book> resp = (Response<Book>)await BookEngine.CreateAsync(newBook);
            return Passer.Pass(resp);
        }
        public static async Task<object> Delete(Guid id)
        {
            Response<Book> resp = (Response<Book>)await BookEngine.DeleteAsync(id);
            return Passer.Pass(resp);
        }
    }
}
