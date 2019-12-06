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

        //Constructor
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

        //Constructor for custom Ladder
        public Ladder(int width, int height)
        {
            _hitbox = new AABB(width, height);
            AddChild(_hitbox);
            OnUpdate += MoveDownFast;
            OnUpdate += Touch;
        }

        //Here comes the giant fist.
        public Ladder(bool hand)
        {
            _hitbox = new AABB(600, 943);
            AddChild(_hitbox);
            Sprite sprite = new Sprite("GFX/Angry.png");
            AddChild(sprite);
            OnUpdate += MoveDownVeryFast;
            OnUpdate += Touch2;
        }

        //Checks to see if the ladder is touching hte player
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

        //Same as previous touch but will instantly kill the player
        private void Touch2(float deltaTime)
        {
            if (_hitbox == null)
            {
                return;
            }

            if (_hitbox.DetectCollision(Player.Instance.HitBox))
            {
                Player.Instance.Die();
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

        private void MoveDownFast(float deltaTime)
        {
            YVelocity = +(_speed*2.2f) * deltaTime;

            if (YAbsolute > 900)
            {
                Parent.RemoveChild(this);
            }
        }

        //Here comes the giant fist.
        private void MoveDownVeryFast(float deltaTime)
        {
            YVelocity = +(_speed*6f) * deltaTime;

            if (YAbsolute > 1500)
            {
                Parent.RemoveChild(this);
            }
        }

    }
}
