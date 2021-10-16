using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using KazanBook.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KazanBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            SqlDataReader reader = await db.SqlQueryReader("SELECT id,name,slogan FROM Authors");
            //List<Book> books = reader.GetEnumerator().Select(r => new Book {}).ToList();
            List<Author> list = new List<Author>();
            while (await reader.ReadAsync()) // построчно считываем данные
            {
                Author book = new Author
                {
                    Id = Guid.Parse(reader.GetValue(0).ToString()),
                    Name = reader.GetValue(1).ToString(),
                    Slogan = reader.GetValue(2).ToString()
                };
                list.Add(book);
            }
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(Guid id)
        {
            SqlDataReader reader = await db.SqlQueryReader($"SELECT id,name,slogan FROM Authors WHERE id = '{id.ToString().Replace("''","'")}'");
            //Book book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (!reader.HasRows) { reader.Close(); return NotFound(); }
            await reader.ReadAsync();
            Author author = new Author
            {
                Id = Guid.Parse(reader.GetValue(0).ToString()),
                Name = reader.GetValue(1).ToString(),
                Slogan = reader.GetValue(2).ToString()
            };
            reader.Close();
            return new ObjectResult(author);
        }
        [HttpPut]
        public async Task<ActionResult> Create(string name, string slogan = "")
        {
            string query = "INSERT INTO Authors(id,name,slogan)\n" +
                $"values(NEWID(), '{name.Replace("'", "''")}', '{slogan.Replace("'", "''")}')";
            await db.SqlQueryRead(query);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(string id, string name = null, string slogan = null)
        {
            List<string> statement = new List<string> { };
            if (!(name is null))
            {
                statement.Add($"name = '{name.Replace("'", "''")}'");
            }
            if (!(slogan is null))
            {
                statement.Add($"slogan = '{slogan.Replace("'", "''")}'");
            }
            SqlDataReader reader = await db.SqlQueryReader($"UPDATE Authors SET {statement} WHERE id = '{id.Replace("'", "''")}'");
            if (!reader.HasRows) { reader.Close(); return NotFound(); }
            reader.Close();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            SqlDataReader reader = await db.SqlQueryReader($"DELETE FROM Authors WHERE id = '{id.Replace("'", "''")}'");
            if (!reader.HasRows) { reader.Close(); return NotFound(); }
            reader.Close();
            return Ok();
        }
    }
}
