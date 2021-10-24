using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using KazanBook.DAL.Entities;

namespace KazanBook.DAL.DataAccess
{
    public class BookEngine
    {
        public static async Task<object> GetAllAsync()
        {
            List<Book> list = new List<Book>();
            using (SqlDataReader reader = await db.SqlQueryReader("SELECT id,title,authorid,tags FROM Books"))
            {
                while (await reader.ReadAsync())
                {
                    Guid? authorId = null;
                    try
                    {
                        authorId = reader.GetGuid(2);
                    }
                    catch (Exception) { }
                    Book book = new Book
                    {
                        Id = reader.GetGuid(0),
                        Title = reader.GetValue(1).ToString(),
                        AuthorId = authorId,
                        Tags = reader.GetValue(3).ToString().Split(";")
                    };
                    list.Add(book);
                }
            }
            return Response<List<Book>>.Success(list);
        }
        public static async Task<object> GetByIdAsync(string id) // assume it's getting Guid (we trust BAL, so we don't check if it's really Guid) and quote "'" checks
        {
            using (SqlDataReader reader = await db.SqlQueryReader($"SELECT id,title,authorid,tags FROM Books WHERE id = '{id}'"))
            {
                //Book book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (!reader.HasRows) return Response<Book>.NotFound();
                await reader.ReadAsync();
                Guid? authorId = null;
                try
                {
                    authorId = reader.GetGuid(2);
                }
                catch (Exception) { }
                Book book = new Book
                {
                    Id = reader.GetGuid(0),
                    Title = reader.GetValue(1).ToString(),
                    AuthorId = authorId,
                    Tags = reader.GetValue(3).ToString().Split(";")
                };
                return Response<Book>.Success(book);
            }
        }
        public static async Task<object> CreateAsync(object bookObject)
        {
            Book book = (Book)bookObject;
            string query = "INSERT INTO Books(id,title,authorid,tags)\n" +
                $"values(NEWID(), '{book.Title}', {book.AuthorId}, '{String.Join(";", book.Tags)}';";
            await db.SqlQueryRead(query);
            return Response<string>.Success();
        }
        public static async Task<object> UpdateAsync(object bookObject)
        {
            Book book = (Book)bookObject;
            List<string> statement = new List<string> { };
            if (!(book.Title is null)) statement.Add($"title = '{book.Title}'");
            if (!(book.AuthorId is null)) statement.Add($"authorid = '{book.AuthorId}'");
            if (!(book.Tags is null)) statement.Add($"tags = '{String.Join(";", book.Tags)}'");
            using (SqlDataReader reader = await db.SqlQueryReader($"UPDATE Books SET {String.Join(", ",statement)} WHERE id = '{book.Id}'")) {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
        public static async Task<object> DeleteAsync(string id) // Guid
        {
            using (SqlDataReader reader = await db.SqlQueryReader($"DELETE FROM Books WHERE id = '{id}'")) {
                if (!reader.HasRows) return Response<string>.NotFound();
            }
            return Response<string>.Success();
        }
    }
}
