using System;
using System.Diagnostics;
using System.Threading;

namespace BankAccountApp
{
    public class BankAccount
    {
        private decimal _balance;
        private readonly object _lock = new object();
        public BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }
        public void Deposit(decimal amount)
        {
            lock (_lock)
            {
                if (amount <= 0)
                {
                    return;
                }

                _balance += amount;
                Console.WriteLine($"Deposited: {amount:C}. New Balance: {_balance:C}");
            }
        }
        public void Withdraw(decimal amount)
        {
            lock (_lock)
            {
                if (amount <= 0)
                {
                    return;
                }

                if (_balance >= amount)
                {
                    _balance -= amount;
                    Console.WriteLine($"Withdrew: {amount:C}. Remaining Balance: {_balance:C}");
                }
                else
                {
                    Console.WriteLine($"Insufficient funds. Current Balance: {_balance:C}");
                }
            }
        }
        public decimal GetBalance()
        {
            lock (_lock)
            {
                return _balance;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Test your code here or use the tests file!");
            BankAccount account = new BankAccount(1000M);
            Thread t1 = new Thread(() => account.Deposit(500));
            Thread t2 = new Thread(() => account.Withdraw(300));
            Thread t3 = new Thread(() => account.Deposit(200));
            Thread t4 = new Thread(() => account.Withdraw(1200));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            Console.WriteLine($"Final Balance: {account.GetBalance():C}");
        }
    }
}


