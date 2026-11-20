using BankAccountApp;

Console.WriteLine("=== BankAccount demo (SE Lab 9 - Exercise 3) ===");
var account = new BankAccount("Trung Pham", 1000m);
Console.WriteLine($"{account.CustomerName}: opening balance = {account.Balance}");
account.Debit(200m);
Console.WriteLine($"Debit(200)    -> balance = {account.Balance}");
account.Credit(150m);
Console.WriteLine($"Credit(150)   -> balance = {account.Balance}");
account.Withdraw(50m);
Console.WriteLine($"Withdraw(50)  -> balance = {account.Balance}");
try
{
    account.Withdraw(5000m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Withdraw(5000)-> {ex.Message}");
}
