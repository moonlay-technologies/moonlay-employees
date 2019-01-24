using Moonlay.Employees.Application;
using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.ValueObjects;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Moonlay.Employees.Tests.Application
{
    public class NewEmployeeServiceTests : IDisposable
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IEmployeeRepository> mockEmployeeRepo;

        public NewEmployeeServiceTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockEmployeeRepo = this.mockRepository.Create<IEmployeeRepository>();
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        private INewEmployeeService CreateService()
        {
            return new NewEmployeeService(mockEmployeeRepo.Object);
        }

        [Fact]
        public async Task AddEmployeeAsync()
        {
            // Arrange
            var unitUnderTest = this.CreateService();

            CompanyId companyId = new CompanyId(Guid.NewGuid().ToString());
            PersonId personId = new PersonId(Guid.NewGuid().ToString());

            // Act
            Employee result = await unitUnderTest.AddEmployeeAsync(personId, companyId, regisDate: DateTime.Now, resignDate: null);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task CheckIn()
        {
            // Arrange
            var unitUnderTest = this.CreateService();

            var employeeId = Guid.NewGuid();

            // Act
            Employee result = await this.mockEmployeeRepo.Object.GetAsync(employeeId);

            //await result.CheckIn(checkInDate: DateTime.Now, locationsCheckIn: LocationsCheckInEnum.MoonlayHQ);

            // Assert
            Assert.True(false);
        }
    }
}