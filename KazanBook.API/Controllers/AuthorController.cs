using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KazanBook.BAL.Logic;
using KazanBook.DAL.Entities;
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
            return await AuthorLogic.GetAll();
            //return JsonSerializer.Deserialize<List<Author>>(JsonSerializer.Serialize(await AuthorLogic.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(Guid id)
        {
            return (Author)await AuthorLogic.GetById(id);
        }
        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> Create(string name, string slogan = "")
        {
            Author author = new Author()
            {
                Name = name,
                Slogan = slogan
            };
            await AuthorLogic.Create(author);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, string name = null, string slogan = null)
        {
            Author author = new Author()
            {
                Id = id,
                Name = name,
                Slogan = slogan
            };
            await AuthorLogic.Create(author);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await AuthorLogic.Delete(id);
            return Ok();
        }
    }
}
