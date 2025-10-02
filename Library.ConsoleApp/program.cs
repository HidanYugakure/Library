using Library.ConsoleApp.Menu;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Online Library Application başladı!");
            MenuRouter.MainMenu();
        }
    }
}
