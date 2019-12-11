using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Enemy : Entity
    {
        //Stats
        private int _hp = 200;
        public int HP { get { return _hp; } }
        public float Speed { get; set; } = 140f;

        //Movement Bools for Phase III
        private bool _moveLeft = true;
        private bool _MoveUp = true;

        //Phase tracker & Property
        private int _phase;
        private int _maxPhase = 7;
        public int Phase { get { return _phase; } }

        //Variables used for attacks
        private bool _healing = false;

        private Timer _mainTimer = new Timer();

        //The Hitbox
        private AABB _hitbox;
        public AABB HitBox
        {
            get { return _hitbox; }
        }

        //Empty instance class for other files to use
        private static Enemy _instance;
        public static Enemy Instance
        {
            get { return _instance; }
        }

        //Empty root class for the parent
        private Actor _root;

        //Constructor for the boss
        public Enemy(Actor root, byte phase)
        {
            _phase = phase;
            //Sets the root
            _root = root;

            //Gives the enemy a Hitbox
            _hitbox = new AABB(140, 140);
            AddChild(_hitbox);

            //Sets the static instance of the enemy
            _instance = this;

            //All the onUpdate functions to make the enemy function
            OnUpdate += HealthBar;
            OnUpdate += Move;
            OnUpdate += Heal;

            //Startup function
            StartUp();
        }

        //Startup function
        private void StartUp()
        {
            Y = -20;
            BossFightController.CutScene = true;
        }

        //Keeps track of the bosses HP and tells it to heal if its not time to die.
        private void HealthBar(float deltaTime)
        {
            //X, Y, Width, Height, Color
            RL.DrawRectangle(318, 5, 205, 14, Color.GRAY);
            if (!_healing)
            {
                RL.DrawRectangle(320, 5, _hp, 10, Color.GREEN);
            }
            else if (_healing)
            {
                RL.DrawRectangle(320, 5, _hp, 10, Color.RED);
            }
            if (_hp <= 10 && _phase < _maxPhase)
            {
                _healing = true;
            }
        }

        //Healing function
        private void Heal(float deltaTime)
        {
            //If the boss is healing, it becomes invcible and regains HP
            if (_healing)
            {
                if (_hp < 200 && _mainTimer.Seconds > 0.009f)
                {
                    _hp++;
                    _mainTimer.Restart();
                }
                //Tells the boss to stop healing
                else if (_hp >= 200)
                {
                    _healing = false;
                    _phase++;
                }
            }
        }

        //The master move function
        private void Move(float deltaTime)
        {
            if (BossFightController.CutScene)
            {
                YVelocity = 20f * deltaTime;
            }
            if (_mainTimer.Seconds >= 5.5)
            {
                YVelocity = 0f;
                BossFightController.CutScene = false;
            }
            //During phase 6 the boss gains movement for that phase only
            if (_phase == 6)
            {
                if (_moveLeft)
                {
                    MoveLeft(deltaTime);
                }
                else if (!_moveLeft)
                {
                    MoveRight(deltaTime);
                }
                if (_MoveUp)
                {
                    MoveUp(deltaTime);
                }
                else
                {
                    MoveDown(deltaTime);
                }
            }
        }

        //Tells the boss to move up, used during phase 6
        private void MoveUp(float deltaTime)
        {
            if (_phase == 6)
            {
                //if possible to turn left
                if (Y > 100)
                {
                    YVelocity = -Speed * deltaTime;
                }
                //otherwise turn right
                else
                {
                    YVelocity = 0f;
                    _MoveUp = false;
                }
            }
        }

        //Tells the boss to move down, used during phase 6
        private void MoveDown(float deltaTime)
        {
            if (_phase == 6)
            {
                //if possible to turn left
                if (Y < 650)
                {
                    YVelocity = +Speed * deltaTime;
                }
                //otherwise turn right
                else
                {
                    YVelocity = 0f;
                    _MoveUp = true;
                }
            }
        }

        //Tells the boss to move left, used during phase 6
        private void MoveLeft(float deltaTime)
        {
            if (_phase == 6)
            {
                //if possible to turn left
                if (X > 130)
                {
                    XVelocity = -Speed * deltaTime;
                }
                //otherwise turn right
                else
                {
                    XVelocity = 0f;
                    _moveLeft = false;
                }
            }
        }

        //Tells the boss to move right, used during phase 6
        private void MoveRight(float deltaTime)
        {
            //if possible to move right
            if (X < 670)
            {
                XVelocity = +Speed * deltaTime;
            }
            //otherwise turn left
            else
            {
                XVelocity = 0f;
                _moveLeft = true;
            }

        }

        //Function for taking damage
        public void TakeDamage()
        {
            //todo, add animations
            if (!_healing)
            {
                _hp--;
                if (_hp <= 0)
                {
                    Y = -500;
                    RemoveChild(_hitbox);
                    Parent.RemoveChild(this);
                }
            }

        }

    }
}
