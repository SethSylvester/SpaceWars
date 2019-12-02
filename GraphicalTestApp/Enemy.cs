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
        private int _hp = 70;
        private int _counter = 0;
        private bool _moveleft = true;
        private bool _MoveUp = true;
        private bool _phaseThreeStarted = false;

        private int _phase = 1;
        public int Phase { get { return _phase; } }

        //Variables used for attacks
        private bool _invincible = false;
        private int nextAttack = 0;
        private bool _attacking = false;

        //Checks if a beam or such has fired.
        private bool _usedAttack = false;
        private bool _healing = false;

        private Random rnd = new Random();
        private Timer cutSceneTimer = new Timer();
        public Timer attackTimer = new Timer();

        public float Speed { get; set; } = 140f;
        public int HP { get { return _hp; } }

        private AABB _hitbox;
        public AABB HitBox
        {
            get { return _hitbox; }
        }

        private static Enemy _instance;
        public static Enemy Instance
        {
            get { return _instance; }
        }

        private Actor _root;

        public Enemy(Actor root)
        {
            //Sets the root
            _root = root;
            //Gives the player a Hitbox
            _hitbox = new AABB(140, 140);
            AddChild(_hitbox);

            _instance = this;

            OnUpdate += AI;
            OnUpdate += HealthBar;
            OnUpdate += Move;
            OnUpdate += Heal;

            StartUp();
        }

        private void StartUp()
        {
            Y = -20;
            BossFightController.CutScene = true;
        }

        private void HealthBar(float deltaTime)
        {
            if (!_invincible)
            {
                //X, Y, Width, Height, Color
                RL.DrawRectangle(318, 5, 152, 14, Color.GRAY);
                if (!_healing)
                {
                    RL.DrawRectangle(320, 5, _hp, 10, Color.GREEN);
                }
                else if (_healing)
                {
                    RL.DrawRectangle(320, 5, _hp, 10, Color.RED);
                }
                if (_hp <= 10 && _phase < 3)
                {
                    _healing = true;
                }

            }
        }

        private void Heal(float deltaTime)
        {
            if (_healing)
            {
                if (_hp < 70 && cutSceneTimer.Seconds > 0.02f)
                {
                    _hp++;
                    cutSceneTimer.Restart();
                }
                else if (_hp >= 70)
                {
                    _healing = false;
                    _phase++;
                }
            }
        }

        //The attacking AI that determines which attacks to use.
        private void AI (float deltaTime)
        {
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

         private void RotationGun()
        {
            Spinner test = new Spinner(X, Y, 50, "fast");
            Projectile proj = new Projectile();
            Projectile proj1 = new Projectile();
            Projectile proj2 = new Projectile();
            Projectile proj3 = new Projectile();
            Projectile proj4 = new Projectile();
            Projectile proj5 = new Projectile();

            _root.AddChild(test);

            test.AddChild(proj);
            test.AddChild(proj1);
            test.AddChild(proj2);
            test.AddChild(proj3);
            test.AddChild(proj4);
            test.AddChild(proj5);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            Sprite projectileSprite3 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite2 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite4 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite5 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite6 = new Sprite("GFX/Coin.png");
            proj.AddChild(projectileSprite);
            proj1.AddChild(projectileSprite3);
            proj2.AddChild(projectileSprite4);
            proj3.AddChild(projectileSprite2);
            proj4.AddChild(projectileSprite5);
            proj5.AddChild(projectileSprite6);
            proj.X = 10;
            proj1.X = 20;
            proj2.X = 30;
            proj3.X = -10;
            proj4.X = -20;

        }

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

        //The sans style bone attack.
        private void Own()
        {
            if (!_usedAttack)
            {
                _usedAttack = true;
                Projectile proj = new Projectile(false, this, 200, 250, 1);
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
            if (_phase == 3)
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


        private void MoveUp(float deltaTime)
        {
            if (_phase == 3)
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
        private void MoveDown(float deltaTime)
        {
            if (_phase == 3)
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
        private void MoveLeft(float deltaTime)
        {
            if (_phase == 3)
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

        //Moves right
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
