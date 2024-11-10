using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetPaymentByIdAsync(string id);
        Task UpdateAsync(Payment payment);
    }
}
