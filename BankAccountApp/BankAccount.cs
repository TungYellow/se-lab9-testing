namespace BankAccountApp
{
    public class BankAccount
    {
        public string CustomerName { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(string customerName, decimal initialBalance)
        {
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative");
            CustomerName = customerName;
            Balance = initialBalance;
        }

        public void Debit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");
            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Debit(amount);
        }
    }
}
