using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseAPIController : ControllerBase
    {
        public ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}