using System;

namespace KazanBook.BAL.Schema
{
    internal class Book
    {
        internal Guid Id { get; set; }
        internal string Title { get; set; }
        internal Guid? AuthorId { get; set; }
        internal string[] Tags { get; set; }
    }
}
