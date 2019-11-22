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
        private bool _canShoot = true;
        private bool _canShootShot = true;
        private bool _canShootBeam = true;
        private int _shootTime = 200;
        private int _shootShotTime = 700;
        private int _beamTime = 8500;
        //Rotation
        private bool _rotateLeft = true;
        private bool _wiggleLeft = true;
        public float _rotation { get; set; }

        //Gun turret Constructor
        public Turret(Actor actor)
        {
            _root = actor;
            gun = new Gun(_root);
            TStartShot();
            OnUpdate += Shotgun;

            //AddChild(Hitbox = new AABB(75, 70));
        }

        //Rotating turret constructor
        public Turret(Actor actor, bool b)
        {
            _root = actor;
            gun = new Gun(_root);
            TStart();
            OnUpdate += FireGun;
            if (b)
            {
                OnUpdate += TurretRotation;
            }
            else
            {
                OnUpdate += TurretWiggle;
            }
            //Solid = true;
        }

        //Beam turret constructor
        public Turret(Actor actor, int z)
        {
            _root = actor;
            gun = new Gun(_root);
            TStartBeam();
            OnUpdate += FireGunBeam;
            //Solid = true;
        }

        //Starts the beam turret, will be deleted later
        private void TStartBeam()
        {
            //timer for allowing the gun to shoot
            System.Timers.Timer shootTime = new System.Timers.Timer();
            shootTime.Elapsed += FireBoolBeam;
            shootTime.Interval = _beamTime;
            shootTime.Enabled = true;
        }

        private void TStart()
        {
            //timer for allowing the gun to shoot
            System.Timers.Timer shootTime = new System.Timers.Timer();
            shootTime.Elapsed += FireBool;
            shootTime.Interval = _shootTime;
            shootTime.Enabled = true;
        }
        private void TStartShot()
        {
            //timer for allowing the gun to shoot
            System.Timers.Timer shootTime = new System.Timers.Timer();
            shootTime.Elapsed += FireBoolShot;
            shootTime.Interval = _shootShotTime;
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
            if (_canShootShot)
            {
                //shoot function
                gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation * -1f);
                //sets canshoot to false to prevent spamming
                _canShootShot = false;
            }

        }

        //Clean this up later UwU
        private void FireGun(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_canShoot)
            {
                //shoot function
                gun.Shoot(XAbsolute - 100, YAbsolute + 30, _rotation*-1f);
                //sets canshoot to false to prevent spamming
                _canShoot = false;
            }
        }

        private void FireGunBeam(float deltaTime)
        {
            //RL.DrawText(Convert.ToString(_rotation), 800, 355, 25, Color.WHITE);

            //checks if canshoot aka the timer is done.
            if (_canShootBeam)
            {
                //shoot function
                gun.ShootBeam(XAbsolute - 100 + (_rotation*-1f), YAbsolute + 30, _rotation*-1f, this);
                //sets canshoot to false to prevent spamming
                _canShootBeam = false;
            }
        }

        //sets the timer to true to let the gun fire
        private void FireBool(object source, ElapsedEventArgs e)
        {
            _canShoot = true;
        }
        //sets the timer to true to let the gun fire
        private void FireBoolShot(object source, ElapsedEventArgs e)
        {
            _canShootShot = true;
        }

        private void FireBoolBeam(object source, ElapsedEventArgs e)
        {
            _canShootBeam = true;
        }

    }
}
