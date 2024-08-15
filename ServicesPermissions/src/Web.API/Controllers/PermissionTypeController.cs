using Application.PermissionTypes.Create;
using Application.PermissionTypes.GetAll;
using Application.PermissionTypes.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionTypeController : ApiController
    {
        private readonly ISender mediator;

        public PermissionTypeController(ISender mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllPermissionTypeQuery());

            return result.Match(
                permmisionTypes => Ok(permmisionTypes),
                errors => Problem(errors)
            );
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await mediator.Send(new GetByIdPermissionTypeQuery(id));

            return result.Match(
                permmisionType => Ok(permmisionType),
                errors => Problem(errors)
            );
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionTypeCommand command)
        {
            var result = await mediator.Send(command);


            return result.Match(
                permmisionType => Ok(),
                errors => Problem(errors)
            );
        }



    }
}
