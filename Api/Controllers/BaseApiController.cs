using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase 
    {
        private IMediator _mediator;

        //??= checks if the left side ??= is null then it use right side, else its left side 
        //and pass it to the writtenType just like IMediator
        protected IMediator Mediator => _mediator  ??= 
            HttpContext.RequestServices.GetService<IMediator>(); 
    }
}