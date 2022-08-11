using Homework02.Models;

namespace Homework02
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book() {Author = "J.K. Rowling", Title = "Harry Potter"},
            new Book() {Author = "J.R.R. Tolkien", Title = "The Lord of the Rings"},
            new Book() {Author = "Anne Frank", Title = "The Diary of Anne Frank"}
        };
    }
}
