using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new ServiceCollection()
                    .AddHttpClient()
                    .BuildServiceProvider()
                    .GetService<IHttpClientFactory>()
                    .CreateClient();

            Console.WriteLine("USANDO NEWTONSOFT:");
            DateTime comecoNewton = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Iniciou pesquisa");
                var response = await httpClient.GetAsync(@"https://localhost:44302/data/newton");

                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Terminou pesquisa");
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Iniciou conversão");

                var data = await response.Content.ReadAsAsync<IEnumerable<TransactionHistory>>();
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Terminou conversão");

                Console.WriteLine($"--- {i} ---");
            }

            DateTime fimNewton = DateTime.Now;
            Console.WriteLine("USANDO UTF8JSON:");

            DateTime comecoUtf8 = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Iniciou pesquisa");
                var response = await httpClient.GetAsync(@"https://localhost:44394/data/utf8");

                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Terminou pesquisa");
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Iniciou conversão");

                var resposta = await response.Content.ReadAsStreamAsync();
                var data = Utf8Json.JsonSerializer.Deserialize<IEnumerable<TransactionHistory>>(resposta);
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - Terminou conversão");

                Console.WriteLine($"--- {i} ---");
            }

            DateTime fimUtf8 = DateTime.Now;

            Console.WriteLine($"Newton: {(fimNewton - comecoNewton).TotalSeconds}");
            Console.WriteLine($"Utf8: {(fimUtf8 - comecoUtf8).TotalSeconds}");
        }
    }
}
