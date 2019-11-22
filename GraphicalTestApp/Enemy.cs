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
        private int _hp = 100;
        private int _counter = 0;
        private bool _moveleft = true;

        //Variables used for attacks
        private bool _invincible = false;
        private int nextAttack = 0;
        private bool _attacking = false;

        //Checks if a beam or such has fired.
        private bool _usedAttack = false;

        private Random rnd = new Random();
        private Timer attackTimer = new Timer();

        private float Speed { get; set; } = 75f;

        public Enemy()
        {
            //OnUpdate += Move;
            OnUpdate += AI;
            OnUpdate += HealthBar;
        }

        private void HealthBar(float deltaTime)
        {
            if (!_invincible)
            {
                RL.DrawRectangle(298, 298, 152, 14, Color.GRAY);
                RL.DrawRectangle(300, 300, _hp, 10, Color.GREEN);
            }
        }

        //The attacking AI that determines which attacks to use.
        private void AI (float deltaTime)
        {
            if (!_attacking)
            {
                nextAttack = rnd.Next(1, 2);
            }
            if (nextAttack == 1)
            {
                _attacking = true;
                OwnWarning();
            }
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
            if (_moveleft)
            {
                MoveLeft(deltaTime);
            }
            else
            {
                MoveRight(deltaTime);
            }
        }


        private void MoveLeft(float deltaTime)
        {
            //if possible to turn left
            if (X > 150)
            {
                XVelocity = -Speed * deltaTime;
                _counter++;
            }
            //otherwise turn right
            else
            {
                XVelocity = 0f;
                _counter = 0;
                _moveleft = false;
            }

        }

        //Moves right
        private void MoveRight(float deltaTime)
        {
            //if possible to move right
            if (X < 650)
            {
                XVelocity = +Speed * deltaTime;
                _counter++;
            }
            //otherwise turn left
            else
            {
                XVelocity = 0f;
                _counter = 0;
                _moveleft = true;
            }

        }

        //Function for taking damage
        public void TakeDamage()
        {
                //todo, add animations
                _hp--;
                if (_hp <= 0)
                {
                    RemoveChild(this);
                }
            
        }


    }
}
