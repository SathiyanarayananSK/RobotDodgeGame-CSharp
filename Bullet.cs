using SplashKitSDK;


public class Bullet
{
    
    public double bx { get; set; }
    public double by { get; set; }
    public Circle BulletCircle { get{ return SplashKit.CircleAt(bx,by,20); }}
    private Vector2D Velocity {get; set;}
    public const int SPEED = 10;
    
    
    public Bullet(Player gamePlayer)
    {
        bx = gamePlayer.x + (gamePlayer.Width/2);
        by = gamePlayer.y;
        Vector2D dir;
        Point2D currentMousePosition = SplashKit.MousePosition();
        Point2D fromPt = new Point2D()
        {
            X=bx,Y=by
        };

        Point2D toPt = new Point2D()
        {
            X= currentMousePosition.X, Y = currentMousePosition.Y
        };
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir,SPEED);

    }

    public void Draw(Window gameWindow)
    {
        moveBullet();
        gameWindow.FillCircle(Color.Black, bx, by, 10);
    }

    public void moveBullet()
    {
        bx += Velocity.X;
        by += Velocity.Y;
    }

    public bool ColideWith(Robot r)
    {
        double distance = SplashKit.PointPointDistance(BulletCircle.Center, r.CollisionCircle.Center);
        if (distance <= 40)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
