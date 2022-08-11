using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> AllUsers()
        {
            return Ok(StaticDb.UserNames);
        }

        [HttpGet("{index}")]
        public ActionResult<string> GetByIndex(int index)
        {
            try
            {
                if(index < 0)
                {
                    return BadRequest("The index can not be negative value!");
                }

                if(index >= StaticDb.UserNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no resource on index {index}");
                }

                return Ok(StaticDb.UserNames[index]);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult AddUser()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string newUser = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(newUser))
                    {
                        return BadRequest("The body of the request can not be empty");
                    }

                    StaticDb.UserNames.Add(newUser);
                    return StatusCode(StatusCodes.Status201Created, "The new user was added");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }
    }
}
