using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddSchoolTeacherCommandHandler : IRequestHandler<AddSchoolTeacherCommand,Response<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;

        public AddSchoolTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<TeacherDto>> Handle(AddSchoolTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.AddSchoolTeacherAsync(_mapper.Map<Teacher>(request.Teacher));

            return new Response<TeacherDto>(_mapper.Map<TeacherDto>(result));
        }
    }
}