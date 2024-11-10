using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Contracts
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);

        Task<Client?> GetClientByIdAsync(Guid id);
        Task<Client?> GetClientByCpfAsync(CPF cpf);
    }
}