using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KazanBook.DAL;
using KazanBook.DAL.DataAccess;
using KazanBook.DAL.Entities;

namespace KazanBook.BAL.Logic
{
    public class AuthorLogic
    {
        public static async Task<List<Author>> GetAll()
        {
            Response<List<Author>> resp = await AuthorEngine.GetAllAsync();
            return Passer.Pass(resp);
        }
        public static async Task<Author> GetById(Guid id)
        {
            Response<Author> resp = await AuthorEngine.GetByIdAsync(id);
            return Passer.Pass(resp);
        }
        public static async Task<string> Create(Author author)
        {
            Author newAuthor = new Author()
            {
                Name = Passer.DBString(author.Name),
                Slogan = Passer.DBString(author.Slogan)
            };
            Response<string> resp = await AuthorEngine.CreateAsync(newAuthor);
            return Passer.Pass(resp);
        }
        public static async Task<string> Update(Author author)
        {
            Author newAuthor = new Author()
            {
                Id = author.Id,
                Name = Passer.DBString(author.Name),
                Slogan = Passer.DBString(author.Slogan)
            };
            Response<string> resp = await AuthorEngine.UpdateAsync(newAuthor);
            return Passer.Pass(resp);
        }
        public static async Task<string> Delete(Guid id)
        {
            Response<string> resp = await AuthorEngine.DeleteAsync(id);
            return Passer.Pass(resp);
        }
    }
}
