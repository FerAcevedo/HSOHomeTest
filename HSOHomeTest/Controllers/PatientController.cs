using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HSOHomeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        // POST api/<PatientController>
        [HttpPost]
        public async Task<ActionResult> Post(string input)
        {
            return await Task.FromResult(Content(ProcessRequest(input)));
        }

        public static string ProcessRequest(string value)
        {
            StringBuilder newString = new();
            bool opening = true;
            for (int i = 0; i < value.Length; i++)
            {
                char v = value[i];

                if (v == '"')
                {
                    if (opening)
                    {
                        newString.Append('[');
                        opening = false;
                    }
                    else
                    {
                        newString.Append(']');
                        opening = true;
                    }
                }
                else if (v == ',')
                {
                    if (i == 0 || i == value.Length - 1)
                        newString.Append("");

                    else if (value[i - 1] == '"' && value[i + 1] == '"')
                        newString.Append(' ');
                    else if (char.IsDigit(value[i + 1]) && value[i - 1] == '"')
                    {
                        newString.Append(" [");
                        opening = false;
                    }
                    else if (char.IsDigit(value[i - 1]) && value[i + 1] == '"' && !opening)
                    {
                        newString.Append("] ");
                        opening = true;
                    }
                    else
                        newString.Append(v);
                }

                else
                    newString.Append(v);
            }

            if (!opening)
                newString.Append("]");
            return newString.ToString();
        }
    }
}
