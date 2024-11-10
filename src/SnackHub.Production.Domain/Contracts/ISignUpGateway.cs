using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Domain.Contracts;

public interface ISignUpFunctionGateway
{
    Task Execute(SignUpRequest request);
}