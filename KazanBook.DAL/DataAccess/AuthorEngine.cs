using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using KazanBook.DAL.Entities;

namespace KazanBook.DAL.DataAccess
{
    public class AuthorEngine
    {
        public static async Task<Response<List<Author>>> GetAllAsync()
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
        public static async Task<Response<Author>> GetByIdAsync(Guid id)
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
        public static async Task<Response<string>> CreateAsync(Author author)
        {
            string query = "INSERT INTO Authors(id,name,slogan)\n" +
                $"values(NEWID(), {author.Name}, {author.Slogan})";
            await db.SqlQueryRead(query);
            return Response<string>.Success();
        }
        public static async Task<Response<string>> UpdateAsync(Author author)
        {
            List<string> statement = new List<string> { };
            if (!(author.Name is null)) statement.Add($"name = {author.Name}");
            if (!(author.Slogan is null)) statement.Add($"slogan = {author.Slogan}");
            using (SqlDataReader reader = await db.SqlQueryReader($"UPDATE Authors SET {String.Join(", ", statement)} WHERE id = '{author.Id}'"))
            {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
        public static async Task<Response<string>> DeleteAsync(Guid id)
        {
            using (SqlDataReader reader = await db.SqlQueryReader($"DELETE FROM Authors WHERE id = '{id}'"))
            {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
    }
}
