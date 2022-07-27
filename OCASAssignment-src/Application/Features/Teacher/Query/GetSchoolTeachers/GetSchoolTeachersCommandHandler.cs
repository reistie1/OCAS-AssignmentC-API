using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolTeachersCommandHandler : IRequestHandler<GetSchoolTeachersCommand,Response<IReadOnlyList<TeacherDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;

        public GetSchoolTeachersCommandHandler(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<IReadOnlyList<TeacherDto>>> Handle(GetSchoolTeachersCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.GetTeacherCoursesAsync(t => t.SchoolId == request.SchoolId);

            return new Response<IReadOnlyList<TeacherDto>>(_mapper.Map<IReadOnlyList<TeacherDto>>(result));
        }
    }
}