using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.DTO.Responses;

public interface IAccountService
{
    Task<bool> Register(RegistrationRequest request);
    Task<AuthenticationResponse> Login(LoginRequest request);
}