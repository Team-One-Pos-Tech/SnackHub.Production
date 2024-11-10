namespace SnackHub.Domain.Entities
{
    public class Payment
    {
        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Status { get; private set; }

        public Payment(string id, decimal amount, string status)
        {
            Id = id;
            Amount = amount;
            Status = status;
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}
