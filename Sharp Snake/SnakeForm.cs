//using FormsTimer = System.Windows.Forms.Timer;

//namespace Sharp_Snake
//{
//    public partial class SnakeForm : Form
//    {
//        private void SnakeForm_Load(object sender, EventArgs e)
//        {
//            this.Text = "Змейка";
//            this.DoubleBuffered = true;
//            this.KeyPreview = true;
//            this.FormBorderStyle = FormBorderStyle.FixedSingle;
//            this.MaximizeBox = false;
//        }

//        private const int CellSize = 40;
//        private const int WidthInCells = 3;
//        private const int HeightInCells = 3;

//        private FormsTimer gameTimer;
//        private Snake snake;
//        private Point food;
//        private int score;
//        private bool isGameRunning;
//        private Random random;
//        private Direction currentDirection;
//        private Direction nextDirection;

//        public SnakeForm()
//        {
//            gameTimer = new FormsTimer();
//            snake = new Snake(new Point());
//            random = new Random();

//            InitializeComponent();
//            InitializeGame();
//        }

//        private void InitializeGame()
//        {
//            gameTimer.Interval = 500;
//            gameTimer.Tick += GameLoop;

//            btnStart.Click += (s, e) => StartGame();
//            btnRestart.Click += (s, e) => RestartGame();

//            btnRestart.Enabled = false;
//        }

//        private void StartGame()
//        {
//            if (isGameRunning) return;

//            snake = new Snake(new Point(WidthInCells / 2, HeightInCells / 2));
//            currentDirection = Direction.Right;
//            nextDirection = Direction.Right;
//            score = 0;
//            GenerateFood();

//            isGameRunning = true;
//            btnStart.Enabled = false;
//            btnRestart.Enabled = false;

//            gameTimer.Start();
//            this.Focus();
//        }

//        private void RestartGame()
//        {
//            gameTimer.Stop();
//            isGameRunning = false;
//            btnStart.Enabled = true;
//            btnRestart.Enabled = false;
//            Invalidate();
//        }

//        private void GenerateFood()
//        {
//            do
//            {
//                food = new Point(
//                    random.Next(0, WidthInCells),
//                    random.Next(0, HeightInCells));
//            }
//            while (snake.Body.Contains(food));
//        }

//        private void GameLoop(object sender, EventArgs e)
//        {
//            currentDirection = nextDirection;
//            snake.Move(currentDirection);

//            // Проверка столкновений
//            if (snake.Head.X < 0 || snake.Head.X >= WidthInCells ||
//                snake.Head.Y < 0 || snake.Head.Y >= HeightInCells ||
//                snake.Body.Skip(1).Any(segment => segment == snake.Head))
//            {
//                GameOver();
//                return;
//            }

//            // Проверка съедания еды
//            if (snake.Head == food)
//            {
//                snake.Grow();
//                score++;
//                GenerateFood();

//                // Ускорение игры
//                if (gameTimer.Interval > 50 && score % 5 == 0)
//                {
//                    gameTimer.Interval -= 5;
//                }
//            }

//            Invalidate();
//        }

//        private void GameOver()
//        {
//            gameTimer.Stop();
//            isGameRunning = false;
//            btnRestart.Enabled = true;
//            MessageBox.Show($"Игра окончена! Ваш счет: {score}", "Змейка");
//        }

//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);

//            if (!isGameRunning) return;

//            // Рисуем сетку
//            for (int x = 0; x < WidthInCells; x++)
//            {
//                for (int y = 0; y < HeightInCells; y++)
//                {
//                    e.Graphics.DrawRectangle(
//                        Pens.LightGray,
//                        x * CellSize,
//                        y * CellSize,
//                        CellSize,
//                        CellSize);
//                }
//            }

//            // Рисуем змейку
//            foreach (var segment in snake.Body)
//            {
//                e.Graphics.FillRectangle(
//                    Brushes.Green,
//                    segment.X * CellSize,
//                    segment.Y * CellSize,
//                    CellSize,
//                    CellSize);
//            }

//            // Рисуем голову змейки другим цветом
//            e.Graphics.FillRectangle(
//                Brushes.DarkGreen,
//                snake.Head.X * CellSize,
//                snake.Head.Y * CellSize,
//                CellSize,
//                CellSize);

//            // Рисуем еду
//            e.Graphics.FillEllipse(
//                Brushes.Red,
//                food.X * CellSize,
//                food.Y * CellSize,
//                CellSize,
//                CellSize);

//            // Рисуем счет
//            e.Graphics.DrawString(
//                $"Счет: {score}",
//                new Font("Arial", 12),
//                Brushes.Black,
//                10, 10);
//        }

//        protected override void OnKeyDown(KeyEventArgs e)
//        {
//            base.OnKeyDown(e);

//            if (!isGameRunning) return;

//            switch (e.KeyCode)
//            {
//                case Keys.Up when currentDirection != Direction.Down:
//                    nextDirection = Direction.Up;
//                    break;
//                case Keys.Down when currentDirection != Direction.Up:
//                    nextDirection = Direction.Down;
//                    break;
//                case Keys.Left when currentDirection != Direction.Right:
//                    nextDirection = Direction.Left;
//                    break;
//                case Keys.Right when currentDirection != Direction.Left:
//                    nextDirection = Direction.Right;
//                    break;
//            }
//        }
//    }

//    public enum Direction
//    {
//        Up,
//        Down,
//        Left,
//        Right
//    }

//    public class Snake
//    {
//        public List<Point> Body { get; }
//        public Point Head => Body.First();

//        public Snake(Point startPosition)
//        {
//            Body = new List<Point> { startPosition };
//        }
//        public void Move(Direction direction)
//        {
//            var newHead = Head;

//switch (direction)
//{
//    case Direction.Up:
//        newHead.Y--;
//        break;
//    case Direction.Down:
//        newHead.Y++;
//        break;
//    case Direction.Left:
//        newHead.X--;
//        break;
//    case Direction.Right:
//        newHead.X++;
//        break;
//}

//            Body.Insert(0, newHead);
//            Body.RemoveAt(Body.Count - 1);
//        }

//        public void Grow()
//        {
//            Body.Add(Body.Last());
//        }
//    }
//}


using FormsTimer = System.Windows.Forms.Timer;

namespace Sharp_Snake
{
    public partial class SnakeForm : Form
    {
        private void SnakeForm_Load(object sender, EventArgs e)
        {
            this.Text = "Змейка";
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private Snake _snake;
        private Point _food;

        private FormsTimer _gameTimer;
        private const int _fieldWidth = 5;
        private const int _fieldHeight = 15;
        private const int _cellSize = 20;

        private bool _isGameStarted;

        private Direction _currentDirection;
        private Direction _nextDirection;

        private Random _random;

        private int _speed = 500;

        public SnakeForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _snake = new Snake(new Point(_fieldWidth / 2, _fieldHeight / 2));
            _gameTimer = new FormsTimer();
            _random = new Random();

            btnRestart.Enabled = false;

            _gameTimer.Tick += (_, _) => GameLoop();

            btnStart.Click += (_, _) => StartGame();
            btnRestart.Click += (_, _) => RestartGame();
        }

        private void StartGame()
        {
            if (_isGameStarted) return;

            btnStart.Enabled = false;
            btnRestart.Enabled = false;

            _gameTimer.Interval = _speed;

            GenerateFood();

            _isGameStarted = true;

            _gameTimer.Start();
            this.Focus();
        }

        private void RestartGame()
        {
            StartGame();
        }

        private void GameLoop()
        {
            if (_isGameStarted == false) return;

            _currentDirection = _nextDirection;

            _snake.Move(_currentDirection);

            if (_snake.Head.X < 0 ||
                _snake.Head.Y < 0 ||
                _snake.Head.Y >= _fieldHeight ||
                _snake.Head.X >= _fieldWidth ||
                _snake.Body.Skip(1).Any(segment => segment == _snake.Head))
            {
                GameOver();
                return;
            }

            if (_snake.Head == _food)
            {
                _snake.Grow();

                if(_snake.Body.Count == _fieldHeight * _fieldWidth)
                {
                    Win();
                }

                GenerateFood();
            }

            Invalidate();
        }

        private void Win()
        {
            _gameTimer.Stop();

            MessageBox.Show("Победа!");
        }

        private void GameOver()
        {
            _isGameStarted = false;

            _gameTimer.Stop();

            btnRestart.Enabled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (_isGameStarted == false) return;

            switch (e.KeyCode)
            {
                case Keys.Up when _currentDirection != Direction.Down:
                    _nextDirection = Direction.Up;
                    break;
                case Keys.Down when _currentDirection != Direction.Up:
                    _nextDirection = Direction.Down;
                    break;
                case Keys.Left when _currentDirection != Direction.Right:
                    _nextDirection = Direction.Left;
                    break;
                case Keys.Right when _currentDirection != Direction.Left:
                    _nextDirection = Direction.Right;
                    break;
            }
        }
        private void GenerateFood()
        {
            do
            {
                _food = new Point(
                _random.Next(0, _fieldWidth),
                _random.Next(0, _fieldHeight));
            }
            while (_snake.Body.Contains(_food));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int x = 0; x < _fieldWidth; x++)
            {
                for (int y = 0; y < _fieldHeight; y++)
                {
                    e.Graphics.DrawRectangle(
                        Pens.LightGray,
                        x * _cellSize,
                        y * _cellSize,
                        _cellSize,
                        _cellSize);
                }
            }

            foreach (var segment in _snake.Body)
            {
                e.Graphics.FillRectangle(
                    Brushes.Green,
                    segment.X * _cellSize,
                    segment.Y * _cellSize,
                    _cellSize,
                    _cellSize);
            }

            e.Graphics.FillRectangle(
                Brushes.DarkGreen,
                _snake.Head.X * _cellSize,
                _snake.Head.Y * _cellSize,
                _cellSize,
                _cellSize);

            e.Graphics.FillEllipse(
                Brushes.Red,
                _food.X * _cellSize,
                _food.Y * _cellSize,
                _cellSize,
                _cellSize);
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private class Snake
        {
            private readonly List<Point> _body = new List<Point>();

            public List<Point> Body => _body;

            public Point Head => _body.First();

            public Snake(Point startPosition)
            {
                Body.Add(startPosition);
            }

            public void Grow()
            {
                Body.Add(Body.Last());
            }

            public void Move(Direction direction)
            {
                Point newHead = Head;

                newHead = direction switch
                {
                    Direction.Up => new Point(newHead.X, newHead.Y - 1),
                    Direction.Down => new Point(newHead.X, newHead.Y + 1),
                    Direction.Left => new Point(newHead.X - 1, newHead.Y),
                    Direction.Right => new Point(newHead.X + 1, newHead.Y)
                };

                Body.Insert(0, newHead);
                Body.RemoveAt(_body.Count - 1);
            }
        }
    }
}
