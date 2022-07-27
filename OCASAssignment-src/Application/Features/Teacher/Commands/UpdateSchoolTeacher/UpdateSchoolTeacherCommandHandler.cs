using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolTeacherCommandHandler : IRequestHandler<UpdateSchoolTeacherCommand,Response<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;

        public UpdateSchoolTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<TeacherDto>> Handle(UpdateSchoolTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.UpdateTeacherAsync(_mapper.Map<Teacher>(request.Teacher));

            return new Response<TeacherDto>(_mapper.Map<TeacherDto>(result));
        }
    }
}