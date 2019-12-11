using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Projectile : Entity
    {
        //timer class
        private Timer _timer = new Timer();

        //###General Projectile stuff###
        //Rotation float.
        public float Rotation = 0;

        //Projectile speed
        private float _speed = 150f;
        public float Speed { get { return _speed; } }

        //Accelerator for the rocket
        private float YAccelerate = 0.35f;

        //Determines if the projectile is friendly, so the player can't own themselves
        private bool _friendly = false;

        //###Constructors###
        //Basic constructor to lessen the codes load
        public Projectile()
        {
            //Gives the projectile a hitbox
            AddChild(Hitbox = new AABB(16, 16));
            OnUpdate += TouchDetection;
        }

        //Basic projectile, moves downwards
        public Projectile(bool friend) : this()
        {
            //Checks to see if the projectile is friendly and thus will not damage the player
            _friendly = friend;
            OnUpdate += MoveDown;
        }

        //Basic projectile that moves up, the byte identifies it as such
        public Projectile(bool friend, string type) : this()
        {
            if (type == "up")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                _friendly = friend;
                OnUpdate += MoveUp;
            }

            else if (type == "rocket")
            {
                OnUpdate += RocketDown;
            }
            else if (type == "down")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                _friendly = friend;
                OnUpdate += MoveDown;
            }
            else if (type == "left")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                _friendly = friend;
                OnUpdate += MoveLeft;
            }
            else if (type == "right")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                _friendly = friend;
                OnUpdate += MoveRight;
            }

            else if (type == "playerUp")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                _friendly = friend;
                OnUpdate += MoveUp;
                _speed = 300f;
            }
            else if (type == "reverse")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverse;
                OnUpdate += DeletionTimer;
            }
            else if (type == "reverseUp")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseUp;
                OnUpdate += DeletionTimer;
            }
            else if (type == "reverseRight")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseRight;
                OnUpdate += DeletionTimer;
            }
            else if (type == "reverseLeft")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseLeft;
                OnUpdate += DeletionTimer;
            }

        }

        //The function that checks if the projectile is touching either an enemy or the player
        private void TouchDetection(float deltaTime)
        {
            if (Hitbox == null)
            {
                return;
            }

            //Ensures that only the players bullets hit the boss
            if (_friendly)
            {
                if (Hitbox.DetectCollision(Enemy.Instance.HitBox))
                {
                    Parent.RemoveChild(this);
                    Enemy.Instance.TakeDamage();
                }
            }

            //Prevents the players from shooting themselves
            else if (!_friendly)
            {
                if (Hitbox.DetectCollision(Player.Instance.HitBox))
                {
                    Parent.RemoveChild(this);
                    Player.Instance.TakeDamage();
                }
            }
        }

        //Deletes the bullet after an amount of time
        private void DeletionTimer(float deltaTime)
        {

            if (_timer.Seconds >= 2.8f)
            {
                Parent.RemoveChild(this);
            }

        }

        //###Movement Directions###
        private void RocketDown(float deltaTime)
        {
            //Tells the Rocket to speed up
            YVelocity = YVelocity + YAccelerate * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        //Fires the projectile up
        private void MoveUp(float deltaTime)
        {
            YVelocity = -_speed * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }

        }

        //Fires the projectile downwards

        private void MoveDown(float deltaTime)
        {
            YVelocity = +_speed * deltaTime;
            XVelocity = Rotation * deltaTime;


            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        //Fires the projectile downwards and then upwards

        private void MoveReverse(float deltaTime)
        {
            bool reversed = false;
            YVelocity = -_speed * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (_timer.Seconds >= 1.4f && !reversed)
            {
                YVelocity *= -1f;
                XVelocity *= -1f;
                reversed = true;
            }

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        //Fires the projectile upwards and then downwards

        private void MoveReverseUp(float deltaTime)
        {
            bool reversed = false;
            YVelocity = +_speed * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (_timer.Seconds >= 1.4f && !reversed)
            {
                YVelocity *= -1f;
                XVelocity *= -1f;
                reversed = true;
            }

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }
        
        //The bullets start out going right and return to their point of origin
        private void MoveReverseRight(float deltaTime)
        {
            bool reversed = false;
            XVelocity = +_speed * deltaTime;
            YVelocity = Rotation * deltaTime;

            if (_timer.Seconds >= 1.4f && !reversed)
            {
                YVelocity *= -1f;
                XVelocity *= -1f;
                reversed = true;
            }

            if (Y < 0 || Y > 750 || X <= 5 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        //The bullets start out going left and return to their point of origin
        private void MoveReverseLeft(float deltaTime)
        {
            bool reversed = false;
            XVelocity = -_speed * deltaTime;
            YVelocity = Rotation * deltaTime;

            if (_timer.Seconds >= 1.4f && !reversed)
            {
                YVelocity *= -1f;
                XVelocity *= -1f;
                reversed = true;
            }

            if (Y < 0 || Y > 750 || X <= 5 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        //Fires the projectile to the left

        private void MoveLeft(float deltaTime)
        {
            XVelocity = -_speed * deltaTime;
            YVelocity = Rotation * deltaTime;
        }

        //Fires the projectile to the right
        private void MoveRight(float deltaTime)
        {
            XVelocity = +_speed * deltaTime;
            YVelocity = Rotation * deltaTime;
        }

    }
}
