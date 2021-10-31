using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KazanBook.BAL.Schema;
using KazanBook.DAL.DataAccess;

namespace KazanBook.BAL.Logic
{
    public class AuthorLogic
    {
        public static async Task<object> GetAll()
        {
            Response<List<Author>> resp = Passer.Convert<Response<List<Author>>>(await AuthorEngine.GetAllAsync());
            return Passer.Pass(resp);
        }
        public static async Task<object> GetById(Guid id)
        {
            Response<Author> resp = (Response<Author>)await AuthorEngine.GetByIdAsync(id);
            return Passer.Pass(resp);
        }
        public static async Task<object> Create(object authorObject)
        {
            Response<Author> newAuthorUnsecure = (Response<Author>)authorObject;
            Author newAuthor = new Author()
            {
                Name = Passer.DBString(newAuthorUnsecure.data.Name),
                Slogan = Passer.DBString(newAuthorUnsecure.data.Slogan)
            };
            Response<Author> resp = (Response<Author>)await AuthorEngine.CreateAsync(newAuthor);
            return Passer.Pass(resp);
        }
        public static async Task<object> Update(object authorObject)
        {
            Response<Author> newAuthorUnsecure = (Response<Author>)authorObject;
            Author newAuthor = new Author()
            {
                Id = newAuthorUnsecure.data.Id,
                Name = Passer.DBString(newAuthorUnsecure.data.Name),
                Slogan = Passer.DBString(newAuthorUnsecure.data.Slogan)
            };
            Response<Author> resp = (Response<Author>)await AuthorEngine.CreateAsync(newAuthor);
            return Passer.Pass(resp);
        }
        public static async Task<object> Delete(Guid id)
        {
            Response<Author> resp = (Response<Author>)await AuthorEngine.DeleteAsync(id);
            return Passer.Pass(resp);
        }
    }
}
