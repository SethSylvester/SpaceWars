using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Projectile : Entity
    {

        private bool _friendly = false;
        public Projectile(bool friend)
        {
            _friendly = friend;
            //_facing = direction;
            OnUpdate += MoveUp;
            OnUpdate += TouchPlayer;
            AddChild(Hitbox = new AABB(16, 16));
            Hitbox.X = -7;
            Hitbox.Y = -7;
        }

        private float Speed { get; set; } = 85f;

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
