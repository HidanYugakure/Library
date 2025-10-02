namespace Library.ConsoleApp
{
    public static class Ui
    {
        public static void DrawHeader(string title)
        {
            var w = Console.WindowWidth;
            var line = new string('═', Math.Max(10, w - 2));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Center(line);
            Center($" {title} ");
            Center(line);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void DrawPanel(IEnumerable<string> lines, int width = 60)
        {
            var content = lines.ToList();
            var max = Math.Max(width, content.Any() ? content.Max(s => s?.Length ?? 0) + 4 : 40);
            var border = new string('─', max);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"┌{border}┐");
            foreach (var l in content)
            {
                var text = l ?? "";
                Console.WriteLine($"│ {text.PadRight(max - 2)} │");
            }
            Console.WriteLine($"└{border}┘");
            Console.ResetColor();
        }

        public static void DrawHint(string hint)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(hint);
            Console.ResetColor();
        }

        public static void PushPage(string title, IEnumerable<string> lines)
        {
            Console.Clear();
            DrawHeader(title);
            DrawPanel(lines, width: 72);
            Console.ReadKey(true);
        }

        public static void Toast(string message, ConsoleColor color = ConsoleColor.Green, int ms = 800)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            System.Threading.Thread.Sleep(ms);
        }

        private static void Center(string s)
        {
            var w = Console.WindowWidth;
            var pad = Math.Max(0, (w - s.Length) / 2);
            Console.WriteLine(new string(' ', pad) + s);

        }

        public static class PagedView
        {

            public static void ShowPaged<T>(string title, IList<T> items, Func<T, string> render, int pageSize = 8)
            {
                if (items == null) items = Array.Empty<T>();
                if (pageSize <= 1) pageSize = 8;

                int total = items.Count;
                int pages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
                int page = 1;

                while (true)
                {
                    Console.Clear();
                    Ui.DrawHeader(title);

                    var from = (page - 1) * pageSize;
                    var to = Math.Min(from + pageSize, total);

                    var lines = new List<string>();
                    if (total == 0)
                    {
                        lines.Add("Heç nə tapılmadı.");
                    }
                    else
                    {
                        for (int i = from; i < to; i++)
                            lines.Add(render(items[i]));
                    }

                    lines.Add("");
                    lines.Add($"Səhifə: {page}/{pages}  |  ←/→: keçid   Home/End: əvvəl/son   Rəqəm: səhifəyə get   Esc/0: geri");
                    Ui.DrawPanel(lines, width: 86);

                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.LeftArrow && page > 1) page--;
                    else if (key.Key == ConsoleKey.RightArrow && page < pages) page++;
                    else if (key.Key == ConsoleKey.Home) page = 1;
                    else if (key.Key == ConsoleKey.End) page = pages;
                    else if (key.Key == ConsoleKey.D0 || key.Key == ConsoleKey.NumPad0 || key.Key == ConsoleKey.Escape) return;
                    else if (IsDigit(key.Key))
                    {
                        int want = Digit(key.Key);
                        if (want >= 1 && want <= pages) page = want;
                    }
                }

                static bool IsDigit(ConsoleKey k) =>
                    k >= ConsoleKey.D0 && k <= ConsoleKey.D9 || k >= ConsoleKey.NumPad0 && k <= ConsoleKey.NumPad9;

                static int Digit(ConsoleKey k) =>
                    k switch
                    {
                        ConsoleKey.D0 => 0,
                        ConsoleKey.NumPad0 => 0,
                        ConsoleKey.D1 => 1,
                        ConsoleKey.NumPad1 => 1,
                        ConsoleKey.D2 => 2,
                        ConsoleKey.NumPad2 => 2,
                        ConsoleKey.D3 => 3,
                        ConsoleKey.NumPad3 => 3,
                        ConsoleKey.D4 => 4,
                        ConsoleKey.NumPad4 => 4,
                        ConsoleKey.D5 => 5,
                        ConsoleKey.NumPad5 => 5,
                        ConsoleKey.D6 => 6,
                        ConsoleKey.NumPad6 => 6,
                        ConsoleKey.D7 => 7,
                        ConsoleKey.NumPad7 => 7,
                        ConsoleKey.D8 => 8,
                        ConsoleKey.NumPad8 => 8,
                        ConsoleKey.D9 => 9,
                        ConsoleKey.NumPad9 => 9,
                        _ => -1
                    };
            }
        }

    }

}
