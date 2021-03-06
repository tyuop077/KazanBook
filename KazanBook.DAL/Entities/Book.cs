using System;

namespace KazanBook.DAL.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? AuthorId { get; set; }
        public string[] Tags { get; set; }
    }
}
