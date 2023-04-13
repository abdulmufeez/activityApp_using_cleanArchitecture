using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase 
    {
        private IMediator _mediator;

        //??= checks if the left side of ??= is null then it use right side, else its left side 
        //and pass it to the writtenType just like IMediator
        protected IMediator Mediator => _mediator  ??= 
            HttpContext.RequestServices.GetService<IMediator>(); 

        //Handle Mediator response and pass Ok,Notfound or BadRequest response to api response
        protected IActionResult HandleResponse<T>(MediatorHandlerResult<T> response)
        {
            if(response is null)
                return NotFound();

            if (response.IsSuccess && response.Value is not null)
                return Ok(response.Value);
            
            if(response.IsSuccess && response.Value is null)
                return NotFound();

            return BadRequest(response.Error);
        }
    }
}