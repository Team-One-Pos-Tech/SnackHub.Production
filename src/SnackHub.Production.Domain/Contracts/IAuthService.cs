using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Domain.Contracts;

public interface IAuthService
{
    public Task<AuthResponseType> Execute(SignInRequest request);
}