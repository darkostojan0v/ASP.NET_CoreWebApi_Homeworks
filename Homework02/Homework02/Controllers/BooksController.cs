using Homework02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> AllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Book> BookByIndexWithQuery(int? index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("Index is a required parameter!");
                }

                if (index < 0)
                {
                    return BadRequest("The index can not be negative!");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no resource on index {index}.");
                }

                return Ok(StaticDb.Books[index.Value]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpGet("multipleQueryParams")]
        public ActionResult<List<Book>> BookByAuthorAndTitle(string? author, string? title)
        {
            try
            {
                if(string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return BadRequest("You have to send at least filter parameter!");
                }

                if (string.IsNullOrEmpty(author))
                {
                    List<Book> filteredBooks = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return Ok(filteredBooks);
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return Ok(filteredBooks);
                }

                List<Book> bookDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()) && x.Title.ToLower().Contains(title.ToLower())).ToList();

                return Ok(bookDb);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("Book author must not be empty!");
                }

                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Book title must not be empty!");
                }

                StaticDb.Books.Add(book);

                return StatusCode(StatusCodes.Status201Created, "Book created!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }
    }
}
