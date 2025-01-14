﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Spinner : Entity
    {
        //How fast the spinner moves
        private float _speed;

        //The spinner class who's sole purpose is to spin
        public Spinner()
        {
            OnUpdate += Orbit;
        }

        //variable speed spinner
        public Spinner(string rotationSpeed)
        {
            if (rotationSpeed == "fast")
            {
                OnUpdate += FastOrbit;
            }
        }
        //Fully custom spinner
        public Spinner(float x, float y, float speed, string rotationSpeed)
        {
            X = x;
            Y = y;
            if (rotationSpeed == "fast")
            {
                OnUpdate += FastOrbit;
            }
            _speed = speed;
            OnUpdate += MoveDown;
        }

        //Two orbits at varying speeds
        private void Orbit(float deltaTime)
        {
            Rotate(0.3f * deltaTime);
        }

        private void FastOrbit(float deltaTime)
        {
            Rotate(1.1f * deltaTime);
        }

        //Tells the spinner to move down for one of the attacks
        private void MoveDown(float deltaTime)
        {
            YVelocity = +_speed * deltaTime;

            if (YAbsolute < 0 || YAbsolute > 750 || XAbsolute <= 0 || XAbsolute >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

    }
}
