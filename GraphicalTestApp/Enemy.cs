using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Enemy : Entity
    {
        private int _hp = 1;
        private int _counter = 0;
        bool _moveleft = true;

        private float Speed { get; set; } = 75;

        public Enemy()
        {
            OnUpdate += Move;

        }

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
            if (X > 150)
            {
                XVelocity = -Speed * deltaTime;
                _counter++;
            }
            else
            {
                XVelocity = 0f;
                _counter = 0;
                _moveleft = false;
            }

        }

        private void MoveRight(float deltaTime)
        {
            if (X < 650)
            {
                XVelocity = +Speed * deltaTime;
                _counter++;
            }
            else
            {
                XVelocity = 0f;
                _counter = 0;
                _moveleft = true;
            }

        }

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
