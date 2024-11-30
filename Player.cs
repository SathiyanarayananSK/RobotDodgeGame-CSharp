using SplashKitSDK;


public class Player
{
    private Bitmap _PlayerBitmap;
    
    public double x { get; set; }
    public double y { get; set; }
    public bool quit { get; set; }
    public int Width { get { return _PlayerBitmap.Width; } }
    public int Height { get { return _PlayerBitmap.Height; } }
    const int GAP = 10;
    public int Life { get; set; }
    public string Score { get; set; }
    
    
    public Player(Window WindowObject)
    {
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        x = (WindowObject.Width - Width) / (double)2;
        y = (WindowObject.Height - Height) / (double)2;
        WindowObject.DrawBitmap(_PlayerBitmap, x, y);
        quit = false;
        Life = 5;
        Score = "0";
    }

    public void Draw(Window screen)
    {
        screen.DrawBitmap(_PlayerBitmap, x, y);  
    }

    public void HandleInput()
    {
        SplashKit.ProcessEvents();
        if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Move(0,-5);
        }
        else if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Move(0,5);
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            Move(-5,0);
        }
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            Move(5,0);
        }
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            quit = true;
        }
        
    }

    public void StayOnWindow(Window limit)
    {
        if (x < GAP)
        {
            x = GAP; 
        }
        else if (x > (limit.Width - _PlayerBitmap.Width - GAP))
        {
            x = limit.Width - _PlayerBitmap.Width - GAP;
        }

        if (y < GAP)
        {
            y = GAP;
        }
        else if (y > (limit.Height -_PlayerBitmap.Height - GAP))
        {
            y = limit.Height -_PlayerBitmap.Height - GAP;
        }


    }

    public void Move(int x_dis, int y_dis)
    {
        x += x_dis;
        y += y_dis;
    }

    public bool ColideWith(Robot r)
    {
        return _PlayerBitmap.CircleCollision(x,y, r.CollisionCircle);
    }

}
