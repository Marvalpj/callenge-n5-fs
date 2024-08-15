using Application.Permissions.Create;
using Domain.DomainErrors;
using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using Domain.Services;

namespace Application.Permissions.UnitTests.Create
{
    public class CreatePermissionCommandHandlerUnitTest
    {
        private readonly Mock<IPermissionRepository> mockPermissionRepository;
        private readonly Mock<IPermissionTypeRepository> mockPermissionTypeRepository;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IKafkaProducer> mockKafkaProducer;
        private readonly CreatePermissionCommandHandler handler;

        public CreatePermissionCommandHandlerUnitTest()
        {
            mockPermissionRepository = new Mock<IPermissionRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockPermissionTypeRepository = new Mock<IPermissionTypeRepository>();
            mockKafkaProducer = new Mock<IKafkaProducer>();
            handler = new CreatePermissionCommandHandler(mockPermissionRepository.Object, mockPermissionTypeRepository.Object, mockUnitOfWork.Object, mockKafkaProducer.Object);
        }

        // cuando se intenta registrar un permiso y el tipo no existe
        [Fact]
        public async Task HandleCreatePermission_WhenPermissionTypeIdDoesNotExist_ShouldReturnValidationError()
        {
            // arrange
            CreatePermissionCommand createPermissionCommand = new CreatePermissionCommand("name", "lastname", 10, DateTime.Now);

            // act 
            var result = await handler.Handle(createPermissionCommand, default);

            //Assert 
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(Errors.Permission.PermissionTypeIdDoesNotExist.Code);
            result.FirstError.Description.Should().Be(Errors.Permission.PermissionTypeIdDoesNotExist.Description);
        }
    }
}