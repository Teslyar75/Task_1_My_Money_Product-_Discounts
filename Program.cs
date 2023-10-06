using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization; 

namespace Task_1_My_Money_Product__Discounts
{
    public class Money
    {
        private double totalCost;
        private double bonus;

        public Money(double totalCost)
        {
            this.totalCost = totalCost;
            bonus = 0;
        }

        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        public double Bonus
        {
            get { return bonus; }
        }

        public void PrintMoney()
        {
            Console.WriteLine($"Стоимость: {totalCost} долларов");
        }

        public void ReducePrice(double reductionAmount)
        {
            if (reductionAmount > 0 && reductionAmount <= totalCost)
            {
                totalCost -= reductionAmount;
            }
            else
            {
                Console.WriteLine("Некорректное значение для снижения цены.");
            }
        }

        public void AddBonus(double amount)
        {
            bonus += amount;
            if (bonus >= 1.0)
            {
                int bonusDollars = (int)bonus;
                bonus -= bonusDollars;
                totalCost -= bonusDollars;
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public Money Price { get; set; }

        public Product(string name, double totalCost)
        {
            Name = name;
            Price = new Money(totalCost);
        }
    }

    class Program
    {
        static void Main()
        {
            Money bonusMoney = new Money(0);

            while (true)
            {
                Console.Write("Введите название товара (или 'exit' для завершения): ");
                string productName = Console.ReadLine();

                if (productName.ToLower() == "exit")
                {
                    break;
                }

                Console.Write("Введите стоимость товара (в долларах): ");
                double totalCost;
                if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out totalCost))
                {
                    Console.Write("Введите количество купленных единиц товара: ");
                    int quantity = int.Parse(Console.ReadLine());

                    Product product = new Product(productName, totalCost);

                    Console.WriteLine("Информация о покупке:");
                    Console.WriteLine($"Название товара: {product.Name}");
                    Console.WriteLine($"Стоимость товара: {product.Price.TotalCost} долларов");
                    Console.WriteLine($"Количество купленных единиц: {quantity}");

                    double totalSpent = product.Price.TotalCost * quantity;

                    Console.WriteLine($"Общая стоимость: {totalSpent} долларов");

                    bonusMoney = new Money(0);
                    double remainingMoney = totalSpent - Math.Floor(totalSpent);

                    if (remainingMoney > 0)
                    {
                        bonusMoney.AddBonus(remainingMoney);
                        Console.WriteLine($"Вы получили {remainingMoney} бонусных долларов.");
                    }

                    Console.Write("Введите сумму для снижения цены: ");
                    double reductionAmount;
                    if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out reductionAmount))
                    {
                        product.Price.ReducePrice(reductionAmount);

                        Console.WriteLine("Информация о товаре после снижения цены:");
                        Console.WriteLine($"Название товара: {product.Name}");
                        Console.WriteLine($"Стоимость товара: {product.Price.TotalCost} долларов");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат суммы для снижения цены.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный формат стоимости товара. Пожалуйста, введите число.");
                }
            }

            Console.WriteLine("Завершение программы.");
        }
    }


}

