using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GraphicalTestApp
{
    class Turret : Entity
    {
        private Actor _root;
        //Gun gun;
        private bool _canShoot = true;
        int _shootTime = 700;
        //Rotation
        private bool _rotateLeft = true;

        public Turret(Actor actor)
        {
            _root = actor;
            //gun = new Gun(_root);
            TStart();
            OnUpdate += FireGun;
            //Solid = true;
        }

        public Turret(Actor actor, int i)
        {
            OnUpdate += TurretRotation;
        }

        private void TStart()
        {
            //timer for allowing the gun to shoot
            System.Timers.Timer shootTime = new System.Timers.Timer();
            shootTime.Elapsed += FireBool;
            shootTime.Interval = _shootTime;
            shootTime.Enabled = true;
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
            }
            else
            {
                _rotateLeft = true;
            }

        }

        private void FireGun(float deltaTime)
        {
            //checks if canshoot aka the timer is done.
            if (_canShoot)
            {
                //shoot function
                //gun.Shoot(X, Y);

                //sets canshoot to false to prevent spamming
                _canShoot = false;
            }
        }

        //sets the timer to true to let the gun fire
        private void FireBool(object source, ElapsedEventArgs e)
        {
            _canShoot = true;
        }

    }
}
