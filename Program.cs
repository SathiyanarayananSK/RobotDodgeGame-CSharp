using SplashKitSDK;

namespace RobotDodgeGame_CSharp
{
    public class Program
    {
        
        public static void Main()
        {
            Window canvas = new Window("Robot Dodge", 800, 600);
            RobotDodge game = new RobotDodge(canvas);
            
            do
            {
                game.HandleInput();
                
                
            }while(!canvas.CloseRequested && !game.Quit );
        }

    }
}
