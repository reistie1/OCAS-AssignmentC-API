using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;
using OCASAPI.Application.DTO.Requests;

namespace OCASAPI_Tests.Commands
{
    public class UpdateSchoolInformationCommandTests
    {
        private readonly Mock<ISchoolRepository> _mockSchoolRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SchoolInfoRequest _mappedSchoolDto;
        private readonly School _mappedSchool;

        public UpdateSchoolInformationCommandTests()
        {
            Guid Id = Guid.NewGuid();

            _mappedSchoolDto = new SchoolInfoRequest(){Id = Id, Address = new AddressDto {
                Id = Guid.NewGuid(),
                Address1 = "124 Test Rd W",
                Address2 = null,
                City = "Kitchener",
                Province = "Ontario",
                PostalCode = "N2M 8X8"
            }, Name = "St.Joseph's School"};

            _mappedSchool = new School(){Id = Id, Address = new Address {
                Id = Guid.NewGuid(),
                Address1 = "124 Test Rd W",
                Address2 = null,
                City = "Kitchener",
                Province = "Ontario",
                PostalCode = "N2M 8X8"
            }, Name = "St.Joseph's School"};

            _mockSchoolRepo = new Mock<ISchoolRepository>();
            _mockSchoolRepo.Setup(s => s.UpdateSchoolInformationAsync(_mappedSchool)).ReturnsAsync(_mappedSchool);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<SchoolInfoRequest>(_mappedSchool)).Returns(_mappedSchoolDto);
            _mockMapper.Setup(m => m.Map<School>(_mappedSchoolDto)).Returns(_mappedSchool);
        }

        [Fact]
        public async Task UpdateSchoolInformationCommand_ReturnsUpdatedSchoolEntity()
        {
            var command = new UpdateSchoolInformationCommand(_mappedSchoolDto);
            var handler = new UpdateSchoolInformationCommandHandler(_mockMapper.Object, _mockSchoolRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<SchoolInfoRequest>>(result);
            Assert.NotNull(result.Data);
            Assert.Same(_mappedSchoolDto, result.Data);
        }

        [Fact]
        public async Task UpdateSchoolInformationCommand_ValidatesEntityPropertiesCorrectly()
        {
            var command = new UpdateSchoolInformationCommand(new SchoolInfoRequest());
            var validator = new UpdateSchoolInformationCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(1, result.Errors.Count);
        }
    }
}