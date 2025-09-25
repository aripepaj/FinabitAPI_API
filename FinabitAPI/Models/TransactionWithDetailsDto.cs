using Finabit_API.Models;

namespace FinabitAPI.Models
{
    public sealed class TransactionWithDetailsDto
    {
        public Transactions Header { get; set; }
        public List<TransactionDetail> Details { get; set; } = new();
    }
}
