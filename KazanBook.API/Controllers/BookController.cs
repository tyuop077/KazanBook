using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KazanBook.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KazanBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        /*public void BooksContext(BooksContext context)
        {
            db = context;
            if (!db.Books.Any())
            {
                db.Books.Add(new Book { Title = "Hello World" });
                db.Books.Add(new Book { Title = "test" });
                db.SaveChanges();
            }
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            //
            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(Guid id)
        {
            //
            return new ObjectResult(book);
        }
        [HttpPut]
        public async Task<ActionResult> Create(string title, Guid? authorid = null, string[] tags = null)
        {
            string tagsStr = tags is null ? "" : String.Join(";", tags);
            string authorIdStr = authorid is null ? "NULL" : $"'{authorid.ToString().Replace("'", "''")}'";
            //string query = "INSERT INTO Books(id,title,authorid,tags)\n" +
            //    $"values(NEWID(), '{title.Replace("'","''")}', {authorIdStr}, '{tagsStr.Replace("'", "''")}';";
            //await db.SqlQueryRead(query);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(string id, string title = null, Guid? authorid = null, string[] tags = null)
        {
            List<string> statement = new List<string> { };
            if (!(title is null))
            {
                statement.Add($"title = '{title.Replace("'", "''")}'");
            }
            // Обновляем authorId в любом случае, так как неизвестно начальное значение (сбрасываем ли бы его на null или устанавливаем новое)
            string authorIdStr = authorid is null ? "NULL" : $"'{authorid.ToString().Replace("'", "''")}'";
            statement.Add($"authorid = {authorIdStr}");
            if (tags.Length > 0)
            {
                statement.Add($"tags = '{String.Join(';', tags).Replace("'", "''")}'");
            }
            SqlDataReader reader = await db.SqlQueryReader($"UPDATE Books SET {statement} WHERE id = '{id.Replace("'", "''")}'");
            if (!reader.HasRows) { reader.Close(); return NotFound(); }
            reader.Close();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            SqlDataReader reader = await db.SqlQueryReader($"DELETE FROM Books WHERE id = '{id.Replace("'", "''")}'");
            if (!reader.HasRows) { reader.Close(); return NotFound(); }
            return Ok();
        }
    }
}
