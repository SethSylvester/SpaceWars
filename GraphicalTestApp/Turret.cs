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
        //Instance of the root
        private Actor _root;
        //Gives the turret a gun so it can shoot
        private Gun _gun;

        //Gun firing interval times
        private float _gunFireInterval = 0.2f;
        private float _shotGunFireInterval = 0.7f;
        private float _rocketFireInterval = 1f;

        //Rotation
        private bool _rotateLeft = true;
        private bool _wiggleLeft = true;
        public float _rotation { get; set; }

        //private timer class to determine firing speeds
        private Timer _timer = new Timer();

        //Gun turret Constructor
        public Turret(Actor actor)
        {
            _root = actor;
            _gun = new Gun(_root);
            OnUpdate += Shotgun;
        }

        //Rotating turret constructor
        public Turret(Actor actor, string type)
        {
            _root = actor;
            _gun = new Gun(_root);
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
            else if (type == "rocket")
            {
                OnUpdate += FireRocket;
            }
            else if (type == "reverse")
            {
                OnUpdate += FireGunReverse;
            }
            else if (type == "reverse2")
            {
                OnUpdate += FireGunReverse2;
            }

        }
        
        //Larger rotation functions
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

        //Mini rotation functions
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
            if (_timer.Seconds >= _shotGunFireInterval)
            {
                _timer.Restart();
                //shoot function
                _gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation * -1f);
            }

        }

        //Clean this up later UwU
        private void FireGun(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= _gunFireInterval)
            {
                _timer.Restart();
                //shoot function
                _gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation*-1f);
            }
        }  
        
        //Clean this up later UwU
        private void FireRocket(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= _rocketFireInterval)
            {
                _timer.Restart();
                //shoot function
                _gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation*-1f, "rocket");
            }
        }

        private void FireGunReverse(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= _gunFireInterval)
            {
                _timer.Restart();
                //shoot function
                //Down Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverse");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverse");
                }
                //up shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseUp");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseUp");
                }
                //Left Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseLeft");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseLeft");
                }
                //Right Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i, "reverseRight");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i, "reverseRight");
                }
            }
        }

        private void FireGunReverse2(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_timer.Seconds >= _gunFireInterval)
            {
                _rotation = GetRotation();
                _timer.Restart();
                //shoot function
                //Down Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i + _rotation, "down");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i + _rotation, "down");
                }
                //up shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i + _rotation, "up");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i + _rotation, "up");
                }
                //Left Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i + _rotation, "left");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i + _rotation, "left");
                }
                //Right Shots
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, i + _rotation, "right");
                }
                for (float i = 0; i < 3; i += 0.5f)
                {
                    _gun.Shoot(XAbsolute - 100, YAbsolute + 30, -i + _rotation, "right");
                }
            }
        }


    }
}