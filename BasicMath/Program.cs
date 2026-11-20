using BasicMath;

var bm = new BasicMaths();
Console.WriteLine("=== BasicMath demo (SE Lab 9 - Exercise 1) ===");
Console.WriteLine($"Add(5, 3)       = {bm.Add(5, 3)}");
Console.WriteLine($"Subtract(5, 3)  = {bm.Subtract(5, 3)}");
Console.WriteLine($"Multiply(5, 3)  = {bm.Multiply(5, 3)}");
Console.WriteLine($"Divide(5, 2)    = {bm.Divide(5, 2)}");
try
{
    bm.Divide(5, 0);
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Divide(5, 0)    -> {ex.GetType().Name}: {ex.Message}");
}
