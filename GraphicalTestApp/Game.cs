using System;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Game
    {
        //The current root Actor
        private Actor _root = null;
        //The next root Actor
        private Actor _next = null;
        //The Timer for the entire Game
        private Timer _gameTimer = new Timer();

        public static bool Exit = false;

        //Creates a Game and new Scene instance as its active Scene
        public Game(int width, int height, string title)
        {
            RL.InitWindow(width, height, title);
            RL.SetTargetFPS(0);
        }
        
        //Run the game loop
        public void Run()
        {
            //Update and draw until the game is over
            while (!RL.WindowShouldClose() && !Exit)
            {
                //Change the Scene if needed
                if (_root != _next)
                {
                    _root = _next;
                }

                //Start the Scene if needed
                if (!_root.Started)
                {
                    _root.Start();
                }

                //Update the active Scene
                _root.Update(_gameTimer.GetDeltaTime());

                //Draw the active Scene
                RL.BeginDrawing();
                RL.ClearBackground(Color.BLACK);
                _root.Draw();
                RL.EndDrawing();
            }

            //End the game
            RL.CloseWindow();
        }

        //The Actor we are currently running
        public Actor Root
        {
            get { return _root; }
            set
            {
                _next = value;
                if (_root == null) _root = value;
            }
        }
    }
}
