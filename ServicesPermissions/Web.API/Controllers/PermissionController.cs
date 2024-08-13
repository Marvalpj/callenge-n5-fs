using Application.Permissions.Create;
using Application.Permissions.GetAll;
using Application.Permissions.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ApiController
    {
        private readonly ISender mediator;

        public PermissionController(ISender mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetAllPermissionQuery());

            return result.Match(
                p => Ok(p),
                errors => Problem(errors)
            );
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await mediator.Send(new GetByIdPermissionQuery(id));

            return result.Match(
                p => Ok(p),
                errors => Problem(errors)
            );
        }
        
        [HttpPost()]
        public async Task<IActionResult> Create(CreatePermissionCommand createPermission)
        {
            var result = await mediator.Send(createPermission);

            return result.Match(
                p => Ok(),
                errors => Problem(errors)
            );
        }



    }
}
