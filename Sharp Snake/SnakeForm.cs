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

//        private Snake _snake;
//        private Point _food;

//        private FormsTimer _gameTimer;
//        private const int _fieldWidth = 4;
//        private const int _fieldHeight = 4;
//        private const int _cellSize = 20;

//        private bool _isGameStarted;

//        private Direction _currentDirection;
//        private Direction _nextDirection;

//        private Random _random;

//        private int _speed = 500;

//        public SnakeForm()
//        {
//            InitializeComponent();
//            InitializeGame();
//        }

//        private void InitializeGame()
//        {
//            _gameTimer = new FormsTimer();
//            _random = new Random();
//            _snake = new Snake(new Point(_fieldWidth / 2, _fieldHeight / 2));

//            btnRestart.Enabled = false;

//            _gameTimer.Tick += (_, _) => GameLoop();

//            btnStart.Click += (_, _) => StartGame();
//            btnRestart.Click += (_, _) => RestartGame();
//        }

//        private void StartGame()
//        {
//            if (_isGameStarted) return;

//            _snake = new Snake(new Point(_fieldWidth / 2, _fieldHeight / 2));

//            _nextDirection = Direction.Up;

//            btnStart.Enabled = false;
//            btnRestart.Enabled = false;

//            _gameTimer.Interval = _speed;

//            GenerateFood();

//            _isGameStarted = true;

//            _gameTimer.Start();
//            this.Focus();
//        }

//        private void RestartGame()
//        {
//            _isGameStarted = false;

//            StartGame();
//        }

//        private void GameLoop()
//        {
//            if (_isGameStarted == false) return;

//            _currentDirection = _nextDirection;

//            _snake.Move(_currentDirection);

//            if (_snake.Head.X < 0 ||
//                _snake.Head.Y < 0 ||
//                _snake.Head.Y >= _fieldHeight ||
//                _snake.Head.X >= _fieldWidth ||
//                _snake.Body.Skip(1).Any(segment => segment == _snake.Head))
//            {
//                GameOver();
//                return;
//            }

//            if (_snake.Head == _food)
//            {
//                _snake.Grow();

//                if (_snake.Body.Count - 1 == _fieldHeight * _fieldWidth)
//                {
//                    _snake.Insert(0, _food);

//                    _isGameStarted = false;

//                    Invalidate();

//                    Win();

//                    return;
//                }

//                GenerateFood();
//            }

//            Invalidate();
//        }

//        private void Win()
//        {
//            _gameTimer.Stop();

//            btnRestart.Enabled = true;

//            MessageBox.Show("Победа!");
//        }

//        private void GameOver()
//        {
//            _isGameStarted = false;

//            _gameTimer.Stop();

//            btnRestart.Enabled = true;

//            MessageBox.Show("Поражение.");
//        }

//        protected override void OnKeyDown(KeyEventArgs e)
//        {
//            base.OnKeyDown(e);

//            if (_isGameStarted == false) return;

//            switch (e.KeyCode)
//            {
//                case Keys.Up when _currentDirection != Direction.Down:
//                    _nextDirection = Direction.Up;
//                    break;
//                case Keys.Down when _currentDirection != Direction.Up:
//                    _nextDirection = Direction.Down;
//                    break;
//                case Keys.Left when _currentDirection != Direction.Right:
//                    _nextDirection = Direction.Left;
//                    break;
//                case Keys.Right when _currentDirection != Direction.Left:
//                    _nextDirection = Direction.Right;
//                    break;
//            }
//        }
//        private void GenerateFood()
//        {
//            do
//            {
//                _food = new Point(
//                _random.Next(0, _fieldWidth),
//                _random.Next(0, _fieldHeight));
//            }
//            while (_snake.Body.Contains(_food));
//        }

//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);


//            for (int x = 0; x < _fieldWidth; x++)
//            {
//                for (int y = 0; y < _fieldHeight; y++)
//                {
//                    e.Graphics.DrawRectangle(
//                        Pens.LightGray,
//                        x * _cellSize,
//                        y * _cellSize,
//                        _cellSize,
//                        _cellSize);
//                }
//            }

//            foreach (var segment in _snake.Body)
//            {
//                e.Graphics.FillRectangle(
//                    Brushes.Green,
//                    segment.X * _cellSize,
//                    segment.Y * _cellSize,
//                    _cellSize,
//                    _cellSize);
//            }

//            e.Graphics.FillRectangle(
//                Brushes.DarkGreen,
//                _snake.Head.X * _cellSize,
//                _snake.Head.Y * _cellSize,
//                _cellSize,
//                _cellSize);

//            if (_isGameStarted)
//            {
//                e.Graphics.FillEllipse(
//                    Brushes.Red,
//                    _food.X * _cellSize,
//                    _food.Y * _cellSize,
//                    _cellSize,
//                    _cellSize);
//            }
//        }

//        public enum Direction
//        {
//            Up,
//            Down,
//            Left,
//            Right
//        }

//        private class Snake
//        {
//            private readonly List<Point> _body = new List<Point>();

//            public IReadOnlyList<Point> Body => _body;

//            public Point Head => _body.First();

//            public Snake(Point startPosition)
//            {
//                _body.Add(startPosition);
//            }

//            public void Grow()
//            {
//                _body.Add(_body.Last());
//            }

//            public void Insert(int index, Point element)
//            {
//                _body.Insert(index, element);
//            }

//            public void Add(Point element)
//            {
//                _body.Add(element);
//            }

//            public void Move(Direction direction)
//            {
//                Point newHead = Head;

//                newHead = direction switch
//                {
//                    Direction.Up => new Point(newHead.X, newHead.Y - 1),
//                    Direction.Down => new Point(newHead.X, newHead.Y + 1),
//                    Direction.Left => new Point(newHead.X - 1, newHead.Y),
//                    Direction.Right => new Point(newHead.X + 1, newHead.Y)
//                };

//                _body.Insert(0, newHead);
//                _body.RemoveAt(_body.Count - 1);
//            }
//        }
//    }
//}

using FormsTimer = System.Windows.Forms.Timer;

namespace Sharp_Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake
    {
        public LinkedList<Point> Body { get; private set; }
        public Point Head => Body.First.Value;

        private Direction _currentDirection;
        private bool _grow;

        public Snake(int startX, int startY)
        {
            Body = new LinkedList<Point>();
            Body.AddFirst(new Point(startX, startY));
            _currentDirection = Direction.Right;
        }

        public void Move()
        {
            Point newHead = GetNextPoint();
            Body.AddFirst(newHead);

            if (!_grow)
                Body.RemoveLast();
            else
                _grow = false;
        }

        public void Grow() => _grow = true;

        public void ChangeDirection(Keys key)
        {
            Direction newDir = key switch
            {
                Keys.Up => Direction.Up,
                Keys.Down => Direction.Down,
                Keys.Left => Direction.Left,
                Keys.Right => Direction.Right,
                _ => _currentDirection
            };

            if (!IsOpposite(newDir))
                _currentDirection = newDir;
        }

        private bool IsOpposite(Direction newDir)
        {
            return (_currentDirection == Direction.Left && newDir == Direction.Right) ||
                   (_currentDirection == Direction.Right && newDir == Direction.Left) ||
                   (_currentDirection == Direction.Up && newDir == Direction.Down) ||
                   (_currentDirection == Direction.Down && newDir == Direction.Up);
        }

        private Point GetNextPoint()
        {
            Point head = Head;
            return _currentDirection switch
            {
                Direction.Up => new Point(head.X, head.Y - 1),
                Direction.Down => new Point(head.X, head.Y + 1),
                Direction.Left => new Point(head.X - 1, head.Y),
                Direction.Right => new Point(head.X + 1, head.Y),
                _ => head
            };
        }
    }

    public class Food
    {
        private readonly int _cols;
        private readonly int _rows;
        private readonly Random _rand = new();
        public Point Position { get; private set; }

        public Food(int cols, int rows)
        {
            _cols = cols;
            _rows = rows;
            GenerateNewPosition(new List<Point>());
        }

        public void GenerateNewPosition(IEnumerable<Point> occupied)
        {
            Point newPos;
            do
            {
                newPos = new Point(_rand.Next(_cols), _rand.Next(_rows));
            } while (occupied.Contains(newPos));

            Position = newPos;
        }
    }

    public class CollisionService
    {
        public bool IsCollisionWithWalls(Point head, int cols, int rows)
        {
            return head.X < 0 || head.Y < 0 || head.X >= cols || head.Y >= rows;
        }

        public bool IsCollisionWithSelf(Snake snake)
        {
            return snake.Body.Skip(1).Any(p => p.Equals(snake.Head));
        }
    }

    // Основной контроллер игры
    public class GameController
    {
        private readonly Form _form;
        private readonly FormsTimer _timer;
        private readonly Snake _snake;
        private readonly Food _food;
        private readonly CollisionService _collisionService;

        private readonly int _cellSize = 20;
        private readonly int _rows = 25;
        private readonly int _cols = 25;

        public GameController(Form form)
        {
            _form = form;
            _form.ClientSize = new Size(_cols * _cellSize, _rows * _cellSize);

            _snake = new Snake(_cols / 2, _rows / 2);
            _food = new Food(_cols, _rows);
            _collisionService = new CollisionService();

            _timer = new FormsTimer { Interval = 100 };
            _timer.Tick += GameLoop;
            _timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            _snake.Move();

            if (_collisionService.IsCollisionWithWalls(_snake.Head, _cols, _rows) ||
                _collisionService.IsCollisionWithSelf(_snake))
            {
                _timer.Stop();
                MessageBox.Show("Game Over!", "Snake", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            if (_snake.Head.Equals(_food.Position))
            {
                _snake.Grow();
                _food.GenerateNewPosition(_snake.Body);
            }

            _form.Invalidate();
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Змейка
            foreach (var part in _snake.Body)
                g.FillRectangle(Brushes.Green, part.X * _cellSize, part.Y * _cellSize, _cellSize, _cellSize);

            // Еда
            g.FillEllipse(Brushes.Red, _food.Position.X * _cellSize, _food.Position.Y * _cellSize, _cellSize, _cellSize);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            _snake.ChangeDirection(e.KeyCode);
        }
    }

    // Окно игры
    public partial class SnakeForm : Form
    {
        private readonly GameController _controller;

        public SnakeForm()
        {
            Text = "Snake Game - Windows Forms";
            DoubleBuffered = true;
            _controller = new GameController(this);
            KeyDown += _controller.OnKeyDown;
            Paint += _controller.OnPaint;
        }
    }
}
