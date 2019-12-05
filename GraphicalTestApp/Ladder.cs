using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Ladder : Entity
    {
        private float _speed = 160f;
        private AABB _hitbox;
        public Ladder(byte dir)
        {
            _hitbox = new AABB(400, 25);
            AddChild(_hitbox);
            OnUpdate += Touch;

            if (dir == 1)
            {
                OnUpdate += MoveUp;
            }
            else
            {
                OnUpdate += MoveDown;
            }
        }

        private void Touch(float deltaTime)
        {
            if (_hitbox == null)
            {
                return;
            }

            if (_hitbox.DetectCollision(Player.Instance.HitBox))
            {
                Player.Instance.TakeDamage();
            }

        }

        //###Movement Directions###
        private void MoveUp(float deltaTime)
        {
            YVelocity = -_speed * deltaTime;

            if (YAbsolute < 0)
            {
                Parent.RemoveChild(this);
            }

        }

        private void MoveDown(float deltaTime)
        {
            YVelocity = +_speed * deltaTime;

            if (YAbsolute > 750)
            {
                Parent.RemoveChild(this);
            }
        }

    }
}
