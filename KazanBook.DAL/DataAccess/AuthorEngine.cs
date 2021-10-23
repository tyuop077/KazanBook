using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using KazanBook.DAL.Entities;

namespace KazanBook.DAL.Data
{
    class AuthorEngine
    {
        public async Task<Response<List<Author>>> GetAll()
        {
            List<Author> list = new List<Author>();
            using (SqlDataReader reader = await db.SqlQueryReader("SELECT id,name,slogan FROM Authors")) {
                while (await reader.ReadAsync())
                {
                    Author book = new Author
                    {
                        Id = Guid.Parse(reader.GetValue(0).ToString()),
                        Name = reader.GetValue(1).ToString(),
                        Slogan = reader.GetValue(2).ToString()
                    };
                    list.Add(book);
                }
            }
            return Response<List<Author>>.Success(list);
        }
        public async Task<Response<Author>> GetById(string id) // trust BAL
        {
            using (SqlDataReader reader = await db.SqlQueryReader($"SELECT id,name,slogan FROM Authors WHERE id = '{id}'"))
            {
                if (!reader.HasRows) return Response<Author>.NotFound();
                await reader.ReadAsync();
                Author author = new Author
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetValue(1).ToString(),
                    Slogan = reader.GetValue(2).ToString()
                };
                return Response<Author>.Success(author);
            }
        }
        public async Task<Response<string>> Create(object authorObject)
        {
            Author author = (Author)authorObject;
            string query = "INSERT INTO Authors(id,name,slogan)\n" +
                $"values(NEWID(), '{author.Name}', '{author.Slogan}')";
            await db.SqlQueryRead(query);
            return Response<string>.Success();
        }
        public async Task<Response<string>> Update(object authorObject)
        {
            Author author = (Author)authorObject;
            List<string> statement = new List<string> { };
            if (!(author.Name is null)) statement.Add($"name = '{author.Name}'");
            if (!(author.Slogan is null)) statement.Add($"slogan = '{author.Slogan}'");
            using (SqlDataReader reader = await db.SqlQueryReader($"UPDATE Authors SET {String.Join(", ", statement)} WHERE id = '{author.Id}'"))
            {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
        public async Task<Response<string>> Delete(string id) // Guid
        {
            using (SqlDataReader reader = await db.SqlQueryReader($"DELETE FROM Authors WHERE id = '{id}'"))
            {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
    }
}
