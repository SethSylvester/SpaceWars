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
        //1 = north, 2 = south
        private int _direction = 2;

        //Rotation float.
        public float Rotation = 0;

        //Projectile speed
        private float _speed = 150f;
        public float Speed { get { return _speed; } }

        //Determines if the projectile is friendly, so the player can't own themselves
        private bool _friendly = false;

        //###Constructors###
        //Basic constructor to lessen the codes load
        public Projectile()
        {
            //Gives the projectile a hitbox
            AddChild(Hitbox = new AABB(16, 16));
            OnUpdate += TouchPlayer;
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
                OnUpdate += TouchOrigin;
            }
            else if (type == "reverseUp")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseUp;
                OnUpdate += TouchOrigin;
            }
            else if (type == "reverseRight")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseRight;
                OnUpdate += TouchOrigin;
            }
            else if (type == "reverseLeft")
            {
                //Checks to see if the projectile is friendly and thus will not damage the player
                OnUpdate += MoveReverseLeft;
                OnUpdate += TouchOrigin;
            }

        }

        private void TouchPlayer(float deltaTime)
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

        private void TouchPlayerBeam(float deltaTime)
        {
            if (Hitbox == null)
            {
                return;
            }

            if (Hitbox.DetectCollision(Player.Instance.HitBox))
            {
                Player.Instance.TakeDamage();
            }

        }

        private void TouchOrigin(float deltaTime)
        {

            if (_timer.Seconds >= 2.8f)
            {
                Parent.RemoveChild(this);
            }

        }


        //###Movement Directions###
        private void RocketDown(float deltaTime)
        {
            YAcceleration = 1f;
            YVelocity = (+_speed + YAcceleration)  * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        private void MoveUp(float deltaTime)
        {
            YVelocity = -_speed * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }

        }

        private void MoveDown(float deltaTime)
        {
            YVelocity = +_speed * deltaTime;
            XVelocity = Rotation * deltaTime;


            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

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
        
        private void MoveLeft(float deltaTime)
        {
            XVelocity = -_speed * deltaTime;
            YVelocity = Rotation * deltaTime;
        }

        private void MoveRight(float deltaTime)
        {
            XVelocity = +_speed * deltaTime;
            YVelocity = Rotation * deltaTime;
        }

    }
}
