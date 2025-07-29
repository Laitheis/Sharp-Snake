namespace Sharp_Snake
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new SnakeForm());
        }
    }
}