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
        Timer beamTime = new Timer();
        int i = 0;
        int x = 0;
        float incrementer = 0;

        public float Rotation = 0;

        private float _speed = 85f;
        private float Speed {
            get
            {
                return _speed;
            }
            set
            { if (_speed > 85f)
                {
                    _speed = 85f;
                }
            }
        }


        private bool _friendly = false;

        public Projectile(bool friend)
        {
            _friendly = friend;
            //_facing = direction;
            OnUpdate += MoveDown;
            OnUpdate += TouchPlayer;
            AddChild(Hitbox = new AABB(16, 16));
        }
        public Projectile(bool friend, Actor tParent)
        {
            _friendly = friend;
            //_facing = direction;
            OnUpdate += TouchPlayer;
            Beam(tParent);
            OnUpdate += TimeChecker;
        }

        void TimeChecker(float deltaTime)
        {
            if (beamTime.Seconds >= 0.005f && i < 650)
            {
                BeamFire();
            }
            else if (beamTime.Seconds >= 0.005f && i == 650)
            {
                BeamDisappear();
            }
        }

        void BeamFire()
        {
            beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(16, i));
            Hitbox.Y = i/2;
            i++;
            x = i;
        }

        void BeamDisappear()
        {
            beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(16, x));
            Hitbox.Y = incrementer + x/2;
            incrementer++;
            x--;
            if (x == 0)
            {
                i = 0;
                incrementer = 0;
                Parent.RemoveChild(this);
            }
        }

        private void Beam(Actor tParent)
        {
            tParent.AddChild(this);
            Speed = 0;
        }



        /*private Direction _facing;
        private void Move(float deltaTime)
        {
            switch (_facing)
            {
                case Direction.North:
                    MoveUp();
                    break;
                case Direction.South:
                    MoveDown();
                    break;
                case Direction.East:
                    MoveRight();
                    break;
                case Direction.West:
                    MoveLeft();
                    break;
            }
        }
        */

        private void TouchPlayer(float deltaTime)
        {

            if (Hitbox == null)
            {
                return;
            }
            if (Hitbox.DetectCollision(new Vector3(X, Y, 1)))
            {
                RemoveChild(this);
            }
            //List<Entity> touched = GetEntities(X, Y);

            //foreach (Entity e in touched)
            //{
            //    if (e is Player)
            //    {
            //        CurrentScene.RemoveEntity(this);
            //        ((Player)e).TakeDamage();
            //        break;
            //    }
            //}

        }


        //Move Directions
        private void MoveUp(float deltaTime)
        {
            YVelocity = -Speed * deltaTime;
        }

        private void MoveDown(float deltaTime)
        {
            YVelocity = +Speed * deltaTime;
            XVelocity = Rotation * deltaTime;


            if (Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }


        private void MoveLeft(float deltaTime)
        {
            XVelocity = -Speed * deltaTime;

        }

        private void MoveRight(float deltaTime)
        {
            XVelocity = +Speed * deltaTime;

        }

    }
}
