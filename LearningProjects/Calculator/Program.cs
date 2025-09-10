using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        int number1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int number2 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите желаемую операцию (*,/, +, -): ");
        string op = Console.ReadLine();
        switch (op)
        {
            case "*":
                Console.WriteLine($"Произведение чисел: {number1 * number2}");
                break;
            case "/":
                Console.WriteLine($"Частное чисел: {(double)number1 / number2}");
                break;
            case "+":
                Console.WriteLine($"Сумма чисел: {number1 + number2}");
                break;
            case "-":
                Console.WriteLine($"Разница чисел: {number1 - number2}");
                break;
            default:
                Console.WriteLine("Выбрана неверная операция");
                break;
        }
        
        
    }
}


