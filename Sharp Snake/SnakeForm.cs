
namespace Sharp_Snake
{
    public partial class SnakeForm : Form
    {
        private void SnakeForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра началась!");
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {

        }
 
        private const int CellSize = 20;
        private const int WidthInCells = 30;
        private const int HeightInCells = 20;

        private System.Windows.Forms.Timer gameTimer;
        private Snake snake;
        private Point food;
        private int score;
        private bool isGameRunning;
        private Random random;
        private Direction currentDirection;
        private Direction nextDirection;

        public SnakeForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.ClientSize = new Size(WidthInCells * CellSize, HeightInCells * CellSize);
            this.Text = "Змейка";
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            random = new Random();
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 150;
            gameTimer.Tick += GameLoop;

            btnStart.Location = new Point(WidthInCells * CellSize / 2 - 80, HeightInCells * CellSize / 2 - 30);
            btnRestart.Location = new Point(WidthInCells * CellSize / 2 - 80, HeightInCells * CellSize / 2 + 30);

            btnStart.Click += (s, e) => StartGame();
            btnRestart.Click += (s, e) => RestartGame();

            btnRestart.Enabled = false;
        }

        private void StartGame()
        {
            if (isGameRunning) return;

            snake = new Snake(new Point(WidthInCells / 2, HeightInCells / 2));
            currentDirection = Direction.Right;
            nextDirection = Direction.Right;
            score = 0;
            GenerateFood();

            isGameRunning = true;
            btnStart.Enabled = false;
            btnRestart.Enabled = false;

            gameTimer.Start();
            this.Focus();
        }

        private void RestartGame()
        {
            gameTimer.Stop();
            isGameRunning = false;
            btnStart.Enabled = true;
            btnRestart.Enabled = false;
            Invalidate();
        }

        private void GenerateFood()
        {
            do
            {
                food = new Point(
                    random.Next(0, WidthInCells),
                    random.Next(0, HeightInCells));
            }
            while (snake.Body.Contains(food));
        }

        private void GameLoop(object sender, EventArgs e)
        {
            currentDirection = nextDirection;
            snake.Move(currentDirection);

            // Проверка столкновений
            if (snake.Head.X < 0 || snake.Head.X >= WidthInCells ||
                snake.Head.Y < 0 || snake.Head.Y >= HeightInCells ||
                snake.Body.Skip(1).Any(segment => segment == snake.Head))
            {
                GameOver();
                return;
            }

            // Проверка съедания еды
            if (snake.Head == food)
            {
                snake.Grow();
                score++;
                GenerateFood();

                // Ускорение игры
                if (gameTimer.Interval > 50 && score % 5 == 0)
                {
                    gameTimer.Interval -= 5;
                }
            }

            Invalidate();
        }

        private void GameOver()
        {
            gameTimer.Stop();
            isGameRunning = false;
            btnRestart.Enabled = true;
            MessageBox.Show($"Игра окончена! Ваш счет: {score}", "Змейка");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!isGameRunning) return;

            // Рисуем сетку
            for (int x = 0; x < WidthInCells; x++)
            {
                for (int y = 0; y < HeightInCells; y++)
                {
                    e.Graphics.DrawRectangle(
                        Pens.LightGray,
                        x * CellSize,
                        y * CellSize,
                        CellSize,
                        CellSize);
                }
            }

            // Рисуем змейку
            foreach (var segment in snake.Body)
            {
                e.Graphics.FillRectangle(
                    Brushes.Green,
                    segment.X * CellSize,
                    segment.Y * CellSize,
                    CellSize,
                    CellSize);
            }

            // Рисуем голову змейки другим цветом
            e.Graphics.FillRectangle(
                Brushes.DarkGreen,
                snake.Head.X * CellSize,
                snake.Head.Y * CellSize,
                CellSize,
                CellSize);

            // Рисуем еду
            e.Graphics.FillEllipse(
                Brushes.Red,
                food.X * CellSize,
                food.Y * CellSize,
                CellSize,
                CellSize);

            // Рисуем счет
            e.Graphics.DrawString(
                $"Счет: {score}",
                new Font("Arial", 12),
                Brushes.Black,
                10, 10);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!isGameRunning) return;

            switch (e.KeyCode)
            {
                case Keys.Up when currentDirection != Direction.Down:
                    nextDirection = Direction.Up;
                    break;
                case Keys.Down when currentDirection != Direction.Up:
                    nextDirection = Direction.Down;
                    break;
                case Keys.Left when currentDirection != Direction.Right:
                    nextDirection = Direction.Left;
                    break;
                case Keys.Right when currentDirection != Direction.Left:
                    nextDirection = Direction.Right;
                    break;
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake
    {
        public List<Point> Body { get; }
        public Point Head => Body.First();

        public Snake(Point startPosition)
        {
            Body = new List<Point> { startPosition };
        }

        public void Move(Direction direction)
        {
            var newHead = Head;

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y--;
                    break;
                case Direction.Down:
                    newHead.Y++;
                    break;
                case Direction.Left:
                    newHead.X--;
                    break;
                case Direction.Right:
                    newHead.X++;
                    break;
            }

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);
        }

        public void Grow()
        {
            Body.Add(Body.Last());
        }
    }
}
