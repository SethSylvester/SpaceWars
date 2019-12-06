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
        private bool _moveleft = true;
        private bool _MoveUp = true;

        //Phase tracker & Property
        private int _phase;
        private int _maxPhase = 7;
        public int Phase { get { return _phase; } }

        //Variables used for attacks
        private bool _invincible = false;
        private int nextAttack = 0;
        private bool _attacking = false;
        private bool _healing = false;

        //Checks if a beam or such has fired.
        private bool _usedAttack = false;

        //Random class and timers
        private Random rnd = new Random();
        private Timer cutSceneTimer = new Timer();
        public Timer attackTimer = new Timer();


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
            OnUpdate += AI;
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
            Y = 100;
            BossFightController.CutScene = false;
        }

        //Keeps track of the bosses HP and tells it to heal if its not time to die.
        private void HealthBar(float deltaTime)
        {
            if (!_invincible)
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
        }

        //Healing function
        private void Heal(float deltaTime)
        {
            //If the boss is healing, it becomes invcible and regains HP
            if (_healing)
            {
                if (_hp < 200 && cutSceneTimer.Seconds > 0.009f)
                {
                    _hp++;
                    cutSceneTimer.Restart();
                }
                //Tells the boss to stop healing
                else if (_hp >= 200)
                {
                    _healing = false;
                    _phase++;
                }
            }
        }

        //The attacking AI that determines which attacks to use.
        private void AI (float deltaTime)
        {
            //Gives the boss a special attack on phase 2
            if (_phase == 2)
            {
                if (attackTimer.Seconds > 0.01)
                {
                    _attacking = true;
                    OwnWarning();
                }
            }
        }

        //###Attacks###
        //Flashes a warning that the own attack is coming
        //Currently has a bug to make a solid rectangle instead of an outline
        private void OwnWarning()
        {
            if (attackTimer.Seconds <= 0.6f ||
                attackTimer.Seconds >= 1 && attackTimer.Seconds <= 1.6f)
            {
                RL.DrawRectangle(300, 560, 250, 200, Color.MAGENTA);
            }
            else if (attackTimer.Seconds >= 1.7)
            {
                Own();
            }
        }

        //The attack from the bottom of the screen.
        private void Own()
        {
            if (!_usedAttack)
            {
                _usedAttack = true;
                Projectile proj = new Projectile(1, false, this, 200, 250, 1);
                proj.Y = 660;
            }
            if (attackTimer.Seconds >= 5)
            {
                _attacking = false;
                _usedAttack = false;
                attackTimer.Restart();
            }
        }

        //The master move function
        private void Move(float deltaTime)
        {
            if (BossFightController.CutScene)
            {
                YVelocity = 20f * deltaTime;
            }
            if (cutSceneTimer.Seconds >= 5.2)
            {
                YVelocity = 0f;
                BossFightController.CutScene = false;
            }
            //During phase 6 the boss gains movement for that phase only
            if (_phase == 6)
            {
                if (_moveleft)
                {
                    MoveLeft(deltaTime);
                }
                else if (!_moveleft)
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
                    _moveleft = false;
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
                _moveleft = true;
            }

        }

        //private void Touch()
        //{ }

        //Function for taking damage
        public void TakeDamage()
        {
            //todo, add animations
            if (!_healing)
            {
                _hp--;
                if (_hp <= 0)
                {
                    RemoveChild(_hitbox);
                    Parent.RemoveChild(this);
                }
            }

        }

    }
}
