namespace Library.ConsoleApp.Menu
{
    public static class MenuRouter
    {
        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Ui.DrawHeader("Hikari Library");

                Ui.DrawPanel(new[]
                {
                    "Main Menu",
                    "",
                    "[1] Books",
                    "[2] Authors",
                    "[3] Reservations",
                    "[4] Users",
                    "",
                    "[0] Exit"
                }, width: 56);

                Ui.DrawHint("Choose an option…");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1: LibraryHandlers.ShowBooksMenu(); break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2: LibraryHandlers.ShowAuthorsMenu(); break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3: UsersHandler.ShowReservationsMenu(); break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4: UsersHandler.ShowUsersMenu(); break;

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.Escape: return;

                    default: Ui.Toast("Unknown option", ConsoleColor.Red, 700); break;
                }
            }
        }
    }
}
