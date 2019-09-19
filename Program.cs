using ConsoleApp.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<SampleDbContext>()
                    .UseSqlite(connection).Options;

                using (var context = new SampleDbContext(options))
                {
                    await context.Database.EnsureCreatedAsync();

                    context.Transactions.Add(
                        new Transaction
                        {
                            TransactionNumber = "A",
                            Movements = new List<Movement>
                            {
                                new Movement {MovementId = 1},
                                new Movement {MovementId = 2},
                            }
                        });

                    context.SaveChanges();

                    var transactionB = new Transaction { TransactionNumber = "B" };

                    context.Transactions.Add(transactionB);

                    context.SaveChanges();

                    var movement3 = new Movement { MovementId = 3 };

                    context.Movements.Add(movement3);

                    context.SaveChanges();

                    var movement4 = new Movement { MovementId = 4, TransactionNumber = "C" };

                    context.Movements.Add(movement4); // Cause exception

                    context.SaveChanges();

                    foreach (var transaction in context.Transactions)
                    {
                        Console.WriteLine($"Transaction {transaction.TransactionNumber} has {transaction.Movements?.Count ?? 0} movements");
                    }

                    foreach (var movement in context.Movements)
                    {
                        Console.WriteLine($"Movement {movement.MovementId} has the TransactionNumber {movement.TransactionNumber ?? "empty"}");

                    }

                }
            }

            Console.ReadKey();
        }
    }
}
