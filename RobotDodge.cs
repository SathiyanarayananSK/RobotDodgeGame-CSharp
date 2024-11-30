using SplashKitSDK;


public class RobotDodge
{
    private Player _Player;

    private List<Robot> _Robots = new List<Robot>();
    private List<Bullet> _Bullets = new List<Bullet>();
    private List<Bullet> _RemoveBullets = new List<Bullet>();

    private Window _GameWindow;
    private Bullet _Bullet;
    public bool Quit { get { return _Player.quit; } }

    private Bitmap _LifeBitmap;

    private SplashKitSDK.Timer myTimer;

    public bool isFired;
    public List<Robot> RemoveRobots;

    



    public RobotDodge(Window GameWindow)
    {
        myTimer = new SplashKitSDK.Timer("My Timer");
        myTimer.Start();
        _LifeBitmap = new Bitmap("Life", "Life.png");
        _GameWindow = GameWindow;
        _Player = new Player(_GameWindow);
        _Robots.Add(RandomRobot());
        isFired = false;
    }

    public void HandleInput()
    {
        _Player.HandleInput();
        Draw();
        _Player.StayOnWindow(_GameWindow);
        Fire();
        if (LifeOut()) { _Player.quit = true; }
    }

    public void Draw()
    {
        _GameWindow.Clear(Color.White);

        foreach (Robot r in _Robots)
        {
            r.DrawRobot(_GameWindow);
        }

        _Player.Draw(_GameWindow);
        DrawBullet();
        Update();
        DrawLife(_GameWindow);
        ScoreCard();
        _GameWindow.Refresh(60);

    }

    public void DrawBullet()
    {
        if (isFired)
        {
            foreach (Bullet b in _Bullets)
            {
                b.Draw(_GameWindow);
                if (b.by <= 0 || b.bx < 0 || b.by > _GameWindow.Height || b.bx > _GameWindow.Width)
                {
                 _RemoveBullets.Add(b);   
                }
                CheckBulletCollision(b);
            }
            foreach (Bullet b in _RemoveBullets)
            {
                _Bullets.Remove(b);
                
            }
        }
    }

    public void Update()
    {
        if (SplashKit.Rnd() < 0.01)
        {
            _Robots.Add(RandomRobot());
        }

        foreach (Robot robo in _Robots)
        {
            robo.Update();
        }


        CheckCollisions();

    }

    public Robot RandomRobot()
    {
        double randomness = SplashKit.Rnd();

    
        if (randomness < 0.2)
        {
            return new Robot.Boxy(_GameWindow, _Player);
        }
        else if (randomness > 0.2 && randomness < 0.4)
        {
            return new Robot.Hatty(_GameWindow, _Player);
        }
        else if(randomness > 0.4 && randomness < 0.6)
        {
            return new Robot.Roundy(_GameWindow, _Player);
        }
        else if (randomness> 0.6 && randomness< 0.8)
        {
            return new Robot.Noob(_GameWindow, _Player);
        }
        else
        {
            return new Robot.ColorRoundy(_GameWindow, _Player);
        }
        
    }


    public void CheckCollisions()
    {
        RemoveRobots = new List<Robot>();

        foreach (Robot r in _Robots)
        {


            if (_Player.ColideWith(r))
            {
                RemoveRobots.Add(r);
                _Player.Life -= 1;
            }

            if (r.IsOffScreen(_GameWindow))
            {
                RemoveRobots.Add(r);
            }
        }
        foreach (Robot r in RemoveRobots)
        {
            _Robots.Remove(r);
        }

    }

    public bool LifeOut()
    {
        if (_Player.Life == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void DrawLife(Window Screen)
    {
        if (_Player.Life > 0)
        {
            Screen.DrawBitmap(_LifeBitmap, 650, 5);
            if (_Player.Life > 1)
            {
                Screen.DrawBitmap(_LifeBitmap, 680, 5);
                if (_Player.Life > 2)
                {
                    Screen.DrawBitmap(_LifeBitmap, 710, 5);
                    if (_Player.Life > 3)
                    {
                        Screen.DrawBitmap(_LifeBitmap, 740, 5);
                        if (_Player.Life > 4)
                        {
                            Screen.DrawBitmap(_LifeBitmap, 770, 5);

                        }
                    }
                }
            }
        }
    }

    private void ScoreCard()
    {
        uint timeInSec = myTimer.Ticks / 1000;
        _Player.Score = ($"Your Score: {timeInSec.ToString()}");
        _GameWindow.DrawText(_Player.Score,Color.Blue,10,10);

    }

    public void Fire()
    {
        SplashKit.ProcessEvents();
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            _Bullet = new Bullet(_Player);
            _Bullets.Add(_Bullet);
            isFired = true;
        }

    }

    public void CheckBulletCollision(Bullet b)
    {
        foreach (Robot r in _Robots)
        {
            if (b.ColideWith(r))
            {
                RemoveRobots.Add(r);
                _RemoveBullets.Add(b);
            }

        }
        foreach (Robot r in RemoveRobots)
        {
            _Robots.Remove(r);
        }
    }

}