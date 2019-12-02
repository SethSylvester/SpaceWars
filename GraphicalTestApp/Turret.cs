using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Raylib;
using RL = Raylib.Raylib;


namespace GraphicalTestApp
{
    class Turret : Entity
    {
        private Actor _root;
        Gun gun;

        //Rotation
        private bool _rotateLeft = true;
        private bool _wiggleLeft = true;
        public float _rotation { get; set; }

        private Timer _timer = new Timer();

        //Gun turret Constructor
        public Turret(Actor actor)
        {
            _root = actor;
            gun = new Gun(_root);
            OnUpdate += Shotgun;
        }

        //Rotating turret constructor
        public Turret(Actor actor, string type)
        {
            _root = actor;
            gun = new Gun(_root);
            if (type == "rotate")
            {
                OnUpdate += TurretRotation;
                OnUpdate += FireGun;
            }
            else if (type == "wiggle")
            {
                OnUpdate += TurretWiggle;
                OnUpdate += FireGun;
            }
            else if (type == "beam")
            {
                OnUpdate += FireGunBeam;
            }
            else if (type == "reverse")
            {
                OnUpdate += FireGunReverse;
            }

        }

        private void TurretRotation(float deltaTime)
        {
            if (_rotateLeft)
            {
                RotateLeft(deltaTime);
            }
            else
            {
                RotateRight(deltaTime);
            }
        }

        private void RotateLeft(float deltaTime)
        {
            if (GetRotation() < 0.8f)
            {
                Rotate(0.1f * deltaTime);
                _rotation = GetRotation();
            }
            else
            {
                _rotateLeft = false;
            }
        }

        private void RotateRight(float deltaTime)
        {
            if (GetRotation() > -0.8f)
            {
                Rotate(-0.1f * deltaTime);
                _rotation = GetRotation();
            }
            else
            {
                _rotateLeft = true;
            }
        }

        private void TurretWiggle(float deltaTime)
        {
            if (_wiggleLeft)
            {
                WiggleLeft(deltaTime);
            }
            else
            {
                WiggleRight(deltaTime);
            }
        }

        private void WiggleLeft(float deltaTime)
        {
            if (GetRotation() < 0.3f)
            {
                Rotate(0.1f * deltaTime);
                _rotation = GetRotation();
            }
            else
            {
                _wiggleLeft = false;
            }
        }

        private void WiggleRight(float deltaTime)
        {
            if (GetRotation() > -0.3f)
            {
                Rotate(-0.1f * deltaTime);
                _rotation = GetRotation();
            }
            else
            {
                _wiggleLeft = true;
            }
        }

        private void Shotgun(float deltaTime)
        {
            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= 0.7f)
            {
                _timer.Restart();
                //shoot function
                gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation * -1f);
            }

        }

        //Clean this up later UwU
        private void FireGun(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= 0.2f)
            {
                _timer.Restart();
                //shoot function
                gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation*-1f);
            }
        }

        private void FireGunReverse(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= 0.2f)
            {
                _timer.Restart();
                //shoot function
                //Down Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverse");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverse");
                }
                //up shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseUp");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseUp");
                }
                //Left Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseLeft");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseLeft");
                }
                //Right Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseRight");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseRight");
                }
            }
        }

        private void FireGunBeam(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= 8.5f)
            {
                _timer.Restart();
                //shoot function
                gun.ShootBeam(XAbsolute - 100 + (_rotation*-1f), YAbsolute + 30, _rotation*-1f, this);
            }
        }

    }
}
