using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseAPIController : ControllerBase
    {
        public IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}