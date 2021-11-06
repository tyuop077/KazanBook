using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using KazanBook.BAL.Logic;
using KazanBook.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KazanBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            return await BookLogic.GetAll();
            //return JsonSerializer.Deserialize<List<Book>>(JsonSerializer.Serialize(await BookLogic.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(Guid id)
        {
            return await BookLogic.GetById(id);
        }
        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> Create(string title, Guid? authorid = null, string[] tags = null)
        {
            Book book = new Book()
            {
                Title = title,
                AuthorId = authorid,
                Tags = tags
            };
            await BookLogic.Create(book);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, string title = null, Guid? authorid = null, string[] tags = null)
        {
            Book book = new Book()
            {
                Id = id,
                Title = title,
                AuthorId = authorid,
                Tags = tags
            };
            await BookLogic.Create(book);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await BookLogic.Delete(id);
            return Ok();
        }
    }
}
