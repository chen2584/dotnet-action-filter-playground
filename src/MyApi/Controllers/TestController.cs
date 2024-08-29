using Microsoft.AspNetCore.Mvc;
using MyApi.ActionFilters;
using MyApi.Models.Test;

namespace MyApi.Controllers;

[ServiceFilter(typeof(MyActionFilterAttribute), Order = 2)]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpPost()]
    public IActionResult PostData([FromBody] PostDataRequest request)
    {
        return Ok(new PostDataResponse()
        {
            Id = 1,
            Name = request.Name
        });
    }

    [HttpPost("post-with-list")]
    public IActionResult PostDataList([FromBody] PostDataRequest request)
    {
        var list = new List<PostDataResponse>()
        {
            new PostDataResponse()
            {
                Id = 1,
                Name = request.Name
            },
            new PostDataResponse()
            {
                Id = 2,
                Name = request.Name
            }
        };
        return Ok(list);
    }
}