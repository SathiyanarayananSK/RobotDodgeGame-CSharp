using SplashKitSDK;


public abstract class Robot
{
    public double X { get; set; }
    public double Y { get; set; }
    public Color MainColor { get; set; }

    public int Width { get { return 50; } }
    public int Height { get { return 50; } }
    public Circle CollisionCircle { get { return SplashKit.CircleAt(X, Y, 20); } }
    private Vector2D Velocity { get; set; }

    public Robot(Window gamewindow, Player player)
    {

        if (SplashKit.Rnd() < 0.5)
        {
            X = SplashKit.Rnd(gamewindow.Width);

            if (SplashKit.Rnd() < 0.5)
            {
                Y = -Height;
            }
            else
            {
                Y = gamewindow.Height;
            }

        }
        else
        {
            Y = SplashKit.Rnd(gamewindow.Height);

            if (SplashKit.Rnd() < 0.5)
            {
                X = -Width;
            }
            else
            {
                X = gamewindow.Width;
            }

        }

        const int SPEED = 4;
        MainColor = Color.RandomRGB(200);

        Point2D fromPt = new Point2D()
        {
            X = X,
            Y = Y
        };

        Point2D toPt = new Point2D()
        {
            X = player.x,
            Y = player.y
        };

        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, SPEED);



    }



    public abstract void DrawRobot(Window gamewindow);

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }

    public Boolean IsOffScreen(Window gamewindow)
    {
        if (X < -Width || X > gamewindow.Width || Y < -Height || Y > gamewindow.Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public class Boxy : Robot
    {

        public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }


        public override void DrawRobot(Window gamewindow)
        {
            double leftX = X + 12;
            double rightX = X + 27;
            double eyeY = Y + 10;
            double mouthY = Y + 30;

            gamewindow.FillRectangle(Color.Gray, X, Y, 50, 50);
            gamewindow.FillRectangle(MainColor, leftX, eyeY, 10, 10);
            gamewindow.FillRectangle(MainColor, rightX, eyeY, 10, 10);
            gamewindow.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            gamewindow.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }

    public class Hatty : Robot
    {

        public Hatty(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void DrawRobot(Window gameWindow)
        {
            double headRadius = 30;
            double eyeSize = 10;

            SplashKit.FillCircle(MainColor, X, Y, headRadius);
            SplashKit.FillCircle(Color.White, X - 10, Y - 10, eyeSize);
            SplashKit.FillCircle(Color.White, X + 10, Y - 10, eyeSize);

            SplashKit.DrawLine(Color.Black, X - 10, Y + 10, X, Y + 20);
            SplashKit.DrawLine(Color.Black, X, Y + 20, X + 10, Y + 10);


            SplashKit.FillRectangle(Color.Red, X - headRadius, Y - headRadius - 5, headRadius * 2, 10);
            SplashKit.FillRectangle(Color.Red, X - 10, Y - headRadius - 30, 20, 30);
        }
    }

    public class Roundy : Robot
    {

        public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void DrawRobot(Window gamewindow)
        {
            double leftx, midx, rightx;
            double midy, eyey, mouthy;

            leftx = X + 17;
            midx = X + 25;
            rightx = X + 33;

            midy = Y + 25;
            eyey = Y + 20;
            mouthy = Y + 30;

            SplashKit.FillCircle(Color.White, midx, midy, 25);
            SplashKit.DrawCircle(Color.Gray, midx, midy, 25);
            SplashKit.FillCircle(MainColor, leftx, eyey, 5);
            SplashKit.FillCircle(MainColor, rightx, eyey, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyey, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthy, X + 50, Y + 35);

        }
    }

    public class Noob : Robot
    {

        public Noob(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void DrawRobot(Window gameWindow)
        {
            double headWidth = 40;
            double headHeight = 50;
            double cornerRadius = 7.5;

            SplashKit.FillRectangle(MainColor, X - headWidth / 2, Y - headHeight / 2, headWidth, headHeight);

            SplashKit.FillCircle(MainColor, X - headWidth / 2, Y - headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X + headWidth / 2, Y - headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X - headWidth / 2, Y + headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X + headWidth / 2, Y + headHeight / 2, cornerRadius);

            double eyeWidth = 7.5;
            double eyeHeight = 10;
            SplashKit.FillRectangle(Color.White, X - 10, Y - 10, eyeWidth, eyeHeight);
            SplashKit.FillRectangle(Color.White, X + 2.5, Y - 10, eyeWidth, eyeHeight);

            Point2D mouthStart = new Point2D() { X = X - 5, Y = Y + 10 };
            Point2D mouthEnd = new Point2D() { X = X + 5, Y = Y + 10 };
            SplashKit.DrawLine(Color.Red, mouthStart, mouthEnd);

            SplashKit.FillCircle(Color.Blue, X - 10, Y - headHeight / 2 - 5, 4);
            SplashKit.FillCircle(Color.Blue, X + 10, Y - headHeight / 2 - 5, 4);
        }
    }

    public class ColorRoundy : Robot
    {

        public ColorRoundy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void DrawRobot(Window gameWindow)
        {
            double headWidth = 40;
            double headHeight = 50;
            double cornerRadius = 7.5;

            SplashKit.FillRectangle(MainColor, X - headWidth / 2, Y - headHeight / 2, headWidth, headHeight);

            SplashKit.FillCircle(MainColor, X - headWidth / 2, Y - headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X + headWidth / 2, Y - headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X - headWidth / 2, Y + headHeight / 2, cornerRadius);
            SplashKit.FillCircle(MainColor, X + headWidth / 2, Y + headHeight / 2, cornerRadius);

            double eyeWidth = 7.5;
            double eyeHeight = 10;
            SplashKit.FillRectangle(Color.White, X - 10, Y - 10, eyeWidth, eyeHeight);
            SplashKit.FillRectangle(Color.White, X + 2.5, Y - 10, eyeWidth, eyeHeight);

            Point2D mouthStart = new Point2D() { X = X - 5, Y = Y + 10 };
            Point2D mouthEnd = new Point2D() { X = X + 5, Y = Y + 10 };
            SplashKit.DrawLine(Color.Red, mouthStart, mouthEnd);

            SplashKit.FillCircle(Color.Blue, X - 10, Y - headHeight / 2 - 5, 4);
            SplashKit.FillCircle(Color.Blue, X + 10, Y - headHeight / 2 - 5, 4);
        }
    }
}























