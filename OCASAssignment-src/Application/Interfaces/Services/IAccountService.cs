using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.DTO.Responses;
using OCASAPI.Application.Wrappers;

public interface IAccountService
{
    Task<Response<bool>> Register(RegistrationRequest request);
    Task<AuthenticationResponse> Login(LoginRequest request);
}