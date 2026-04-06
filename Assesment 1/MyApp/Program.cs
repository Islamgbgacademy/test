using System;
using System.Collections.Generic;

namespace AssessmentModelAnswer
{
    interface ITask
    {
        string Title { get; }
        void Run();
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ITask> tasks = new List<ITask>
            {
                new FizzBuzzTask(),
                new StringInspectorTask(),
                new GradeCalculatorTask(),
                new NumberAnalyzerTask(),
                new ShoppingCartTask()
            };

            foreach (ITask task in tasks)
            {
                PrintSeparator(task.Title);
                task.Run();
                Console.WriteLine();
            }
        }

        static void PrintSeparator(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('═', 50));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('═', 50));
        }
    }

    class FizzBuzzTask : ITask
    {
        public string Title => "TASK 1 — FizzBuzz Extended (1 to 30)";

        public void Run()
        {
            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine($"{i,2} → {Classify(i)}");
            }
        }

        private string Classify(int n)
        {
            if (n % 7 == 0)             return "Lucky"; 
            if (n % 3 == 0 && n % 5 == 0) return "FizzBuzz";
            if (n % 3 == 0)             return "Fizz";
            if (n % 5 == 0)             return "Buzz";
            return n.ToString();
        }
    }

    class StringInspectorTask : ITask
    {
        public string Title => "TASK 2 — String Inspector";

        private readonly string _sentence = "  The Quick Brown Fox Jumps Over The Lazy Dog  ";

        public void Run()
        {
            string trimmed = _sentence.Trim();
            string lower   = trimmed.ToLower();

            Console.WriteLine($"1. Trimmed      : {trimmed}");
            Console.WriteLine($"2. Char Count   : {trimmed.Length}");
            Console.WriteLine($"3. Lowercase    : {lower}");
            Console.WriteLine($"4. Count of 'o' : {CountLetter(lower, 'o')}");
            Console.WriteLine($"5. Underscored  : {trimmed.Replace(" ", "_")}");
        }

        private int CountLetter(string text, char letter)
        {
            int count = 0;
            foreach (char c in text)
                if (c == char.ToLower(letter))
                    count++;
            return count;
        }
    }


    class GradeCalculatorTask : ITask
    {
        public string Title => "TASK 3 — Grade Calculator";

        public void Run()
        {
            int[] scores = ReadScores(5);
            PrintReport(scores);
        }

        private int[] ReadScores(int count)
        {
            int[] scores = new int[count];
            int i = 0;
            while (i < count)
            {
                Console.Write($"  Enter score {i + 1}: ");
                if (int.TryParse(Console.ReadLine(), out int score) && score >= 0 && score <= 100)
                    scores[i++] = score;
                else
                    Console.WriteLine("  ✗ Invalid — enter a value between 0 and 100.");
            }
            return scores;
        }

        private double CalcAverage(int[] scores)
        {
            int sum = 0;
            foreach (int s in scores) sum += s;
            return (double)sum / scores.Length;
        }

        private int CalcHighest(int[] scores)
        {
            int max = scores[0];
            foreach (int s in scores) if (s > max) max = s;
            return max;
        }

        private int CalcLowest(int[] scores)
        {
            int min = scores[0];
            foreach (int s in scores) if (s < min) min = s;
            return min;
        }

        private (string grade, string label) GetGrade(double avg)
        {
            if (avg >= 90) return ("A", "Excellent");
            if (avg >= 80) return ("B", "Good");
            if (avg >= 70) return ("C", "Above Average");
            if (avg >= 60) return ("D", "Pass");
            return ("F", "Fail");
        }

        private void PrintReport(int[] scores)
        {
            double avg = CalcAverage(scores);
            var (grade, label) = GetGrade(avg);

            Console.WriteLine($"\n  Scores   : {string.Join(", ", scores)}");
            Console.WriteLine($"  Average  : {avg:F2}");
            Console.WriteLine($"  Highest  : {CalcHighest(scores)}  │  Lowest: {CalcLowest(scores)}");
            Console.WriteLine($"  Grade    : {grade}  ({label})");
            Console.WriteLine($"  Status   : {(avg >= 60 ? "PASS" : "FAIL")}");
        }
    }

    class NumberAnalyzerTask : ITask
    {
        public string Title => "TASK 4 — Number Analyzer";

        public void Run()
        {
            Console.Write("  Enter an integer: ");
            if (!int.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine("  ✗ Invalid input.");
                return;
            }

            int abs = n < 0 ? -n : n;
            int isNegative = n < 0 ? 1 : 0;

            Console.WriteLine($"\n  Positive?       {(n > 0 ? "Yes" : n < 0 ? "No (Negative)" : "No (Zero)")}");
            Console.WriteLine($"  Even/Odd?       {(abs % 2 == 0 ? "Even" : "Odd")}");
            Console.WriteLine($"  Prime?          {(IsPrime(abs) ? "Yes" : "No")}");

            int sqrtFactor = PerfectSquareRoot(abs);
            Console.WriteLine($"  Perfect Square? {(sqrtFactor > 0 ? $"Yes  ({sqrtFactor} × {sqrtFactor})" : "No")}");
            Console.WriteLine($"  Digit Sum       {DigitSum(abs)}");
            Console.WriteLine($"  Reversed        {(isNegative == 1 ? "-" : "")}{Reverse(abs)}");
        }

        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        private int PerfectSquareRoot(int n)
        {
            for (int i = 0; i * i <= n; i++)
                if (i * i == n) return i;
            return 0;
        }

        private int DigitSum(int n)
        {
            int sum = 0;
            while (n > 0) { sum += n % 10; n /= 10; }
            return sum;
        }

        private int Reverse(int n)
        {
            int result = 0;
            while (n > 0) { result = result * 10 + n % 10; n /= 10; }
            return result;
        }
    }

    class Product
    {
        public int    Id    { get; }
        public string Name  { get; }
        public double Price { get; }

        public Product(int id, string name, double price)
        {
            Id = id; Name = name; Price = price;
        }
    }
    class CartItem
    {
        public Product Product  { get; }
        public int     Quantity { get; private set; }
        public double  Subtotal => Product.Price * Quantity;

        public CartItem(Product product, int quantity)
        {
            Product = product; Quantity = quantity;
        }

        public void AddQuantity(int qty) => Quantity += qty;
    }

    interface IDiscountService
    {
        double GetDiscountRate(double subtotal);
    }

    class TieredDiscountService : IDiscountService
    {
        public double GetDiscountRate(double subtotal)
        {
            if (subtotal >= 3000) return 0.15;
            if (subtotal >= 1000) return 0.10;
            if (subtotal >= 500)  return 0.05;
            return 0;
        }
    }

    class ReceiptPrinter
    {
        public void Print(List<CartItem> items, double subtotal, double discountRate)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 44));
            foreach (CartItem item in items)
                Console.WriteLine($"  {item.Product.Name,-16} x{item.Quantity,-4} {item.Subtotal,8:F2}");
            Console.WriteLine(new string('-', 44));

            double discountAmt = subtotal * discountRate;
            double total       = subtotal - discountAmt;

            Console.WriteLine($"  {"Subtotal",-20} {subtotal,14:F2}");
            if (discountRate > 0)
                Console.WriteLine($"  {"Discount (" + (int)(discountRate*100) + "%)",-20} {-discountAmt,14:F2}");
            else
                Console.WriteLine($"  {"Discount",-20} {"No discount",14}");
            Console.WriteLine($"  {"TOTAL",-20} {total,14:F2}");
            Console.WriteLine(new string('-', 44));
        }
    }

    class ShoppingCart
    {
        private readonly List<Product>        _catalog;
        private readonly List<CartItem>       _items   = new();
        private readonly IDiscountService     _discount;
        private readonly ReceiptPrinter       _printer = new();

        public ShoppingCart(List<Product> catalog, IDiscountService discount)
        {
            _catalog  = catalog;
            _discount = discount;
        }

        public void Run()
        {
            PrintCatalog();

            while (true)
            {
                Product p = AskProduct();
                if (p == null) break;

                int qty = AskQuantity();
                AddToCart(p, qty);
                Console.WriteLine($" {p.Name} x {qty} added to cart.");
            }

            if (_items.Count == 0)
            {
                Console.WriteLine("  Cart is empty — nothing to checkout.");
                return;
            }

            double subtotal = CalcSubtotal();
            double rate     = _discount.GetDiscountRate(subtotal);
            _printer.Print(_items, subtotal, rate);
        }

        private void PrintCatalog()
        {
            Console.WriteLine("\n  ID  Product         Price");
            Console.WriteLine("  " + new string('-', 30));
            foreach (Product p in _catalog)
                Console.WriteLine($"  {p.Id}   {p.Name,-16} {p.Price,7:F2}");
            Console.WriteLine("  (Enter 0 to checkout)\n");
        }

        private Product AskProduct()
        {
            while (true)
            {
                Console.Write("  Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                { Console.WriteLine("  ✗ Enter a number."); continue; }
                if (id == 0) return null;

                Product found = _catalog.Find(p => p.Id == id);
                if (found != null) return found;
                Console.WriteLine("  ✗ Invalid ID — choose between 1 and 5.");
            }
        }

        private int AskQuantity()
        {
            while (true)
            {
                Console.Write("  Quantity   : ");
                if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                    return qty;
                Console.WriteLine("  ✗ Quantity must be greater than 0.");
            }
        }

        private void AddToCart(Product product, int qty)
        {
            CartItem existing = _items.Find(i => i.Product.Id == product.Id);
            if (existing != null)
                existing.AddQuantity(qty);
            else
                _items.Add(new CartItem(product, qty));
        }

        private double CalcSubtotal()
        {
            double total = 0;
            foreach (CartItem item in _items) total += item.Subtotal;
            return total;
        }
    }

    class ShoppingCartTask : ITask
    {
        public string Title => "TASK 5 — Mini Shopping Cart";

        public void Run()
        {
            var catalog = new List<Product>
            {
                new Product(1, "Keyboard", 350.00),
                new Product(2, "Mouse",    180.00),
                new Product(3, "Monitor",  2200.00),
                new Product(4, "USB Hub",  120.00),
                new Product(5, "Webcam",   650.00),
            };

            var cart = new ShoppingCart(catalog, new TieredDiscountService());
            cart.Run();
        }
    }
}