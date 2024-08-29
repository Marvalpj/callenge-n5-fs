using Application.Permissions.Update;
using Domain.DomainErrors;
using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using Domain.Services;
using MediatR;

namespace Application.Permissions.UnitTests.Update
{
    public class UpdatePermissionCommandHandlerUnitTest
    {
        private readonly Mock<IPermissionRepository> mockPermissionRepository;
        private readonly Mock<IPermissionTypeRepository> mockPermissionTypeRepository;
        private readonly Mock<IKafkaProducer> mockKafkaProducer;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly UpdatePermissionCommandHandler handler;

        public UpdatePermissionCommandHandlerUnitTest()
        {
            // Mock de los repositorios
            // mock permission repo
            Permission permissionMock = new Permission(1, "OldName", "OldLastName", 1, DateTime.UtcNow);
            mockPermissionRepository = new Mock<IPermissionRepository>();
            mockPermissionRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(permissionMock);
            // mock permission type repo 
            PermissionType permissionTypeMock = new PermissionType(1, "leer");
            mockPermissionTypeRepository = new Mock<IPermissionTypeRepository>();
            mockPermissionTypeRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(permissionTypeMock);
            
            mockKafkaProducer = new Mock<IKafkaProducer>();
            mockKafkaProducer.Setup(kafka => kafka.ProduceMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            mockUnitOfWork = new Mock<IUnitOfWork>();

            handler = new UpdatePermissionCommandHandler(mockPermissionRepository.Object, mockPermissionTypeRepository.Object, mockUnitOfWork.Object, mockKafkaProducer.Object);
        }


        [Fact]
        public async Task HandleUpdatePermission_WhenPermissionUpdate_ShouldUpdatePermissionAndReturnUnit()
        {
            // Arrange
            var updatePermissionCommand = new UpdatePermissionCommand(1, "John", "Doe", null, DateTime.UtcNow);

            // Act
            var result = await handler.Handle(updatePermissionCommand, default);
            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(Unit.Value);

            mockPermissionRepository.Verify(repo => repo.UpdateAsync(1, "John", "Doe", null, It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task HandleUpdatePermission_WhenPermissionIdDoesNotExist_ShouldReturnValidationError()
        {
            // arrange
            UpdatePermissionCommand updatePermissionCommand = new UpdatePermissionCommand(0, "name", "lastname", null, null);

            // act 
            var result = await handler.Handle(updatePermissionCommand, default);

            // assert 
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(Errors.Permission.PermissionIdDoesNotExist.Code);
            result.FirstError.Description.Should().Be(Errors.Permission.PermissionIdDoesNotExist.Description);
        }

        [Fact]
        public async Task HandleUpdatePermission_WhenPermissionTypIdDoesNotExist_ShouldReturnValidationError()
        {
            // arrange
            UpdatePermissionCommand updatePermissionCommand = new UpdatePermissionCommand(1, "name", "lastname", 0, null);

            // act 
            var result = await handler.Handle(updatePermissionCommand, default);

            // assert 
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(Errors.Permission.PermissionTypeIdDoesNotExist.Code);
            result.FirstError.Description.Should().Be(Errors.Permission.PermissionTypeIdDoesNotExist.Description);
        }


    }
}
