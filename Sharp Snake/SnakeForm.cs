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
        private const int _fieldWidth = 4;
        private const int _fieldHeight = 4;
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
            _gameTimer = new FormsTimer();
            _random = new Random();
            _snake = new Snake(new Point(_fieldWidth / 2, _fieldHeight / 2));

            btnRestart.Enabled = false;

            _gameTimer.Tick += (_, _) => GameLoop();

            btnStart.Click += (_, _) => StartGame();
            btnRestart.Click += (_, _) => RestartGame();
        }

        private void StartGame()
        {
            if (_isGameStarted) return;

            _snake = new Snake(new Point(_fieldWidth / 2, _fieldHeight / 2));

            _nextDirection = Direction.Up;

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
            _isGameStarted = false;

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

                if (_snake.Body.Count - 1 == _fieldHeight * _fieldWidth)
                {
                    _snake.Body.Insert(0, _food);

                    _isGameStarted = false;

                    Invalidate();

                    Win();

                    return;
                }

                GenerateFood();
            }

            Invalidate();
        }

        private void Win()
        {
            _gameTimer.Stop();

            btnRestart.Enabled = true;

            MessageBox.Show("Победа!");
        }

        private void GameOver()
        {
            _isGameStarted = false;

            _gameTimer.Stop();

            btnRestart.Enabled = true;

            MessageBox.Show("Поражение.");
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

            if (_isGameStarted)
            {
                e.Graphics.FillEllipse(
                    Brushes.Red,
                    _food.X * _cellSize,
                    _food.Y * _cellSize,
                    _cellSize,
                    _cellSize);
            }
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
