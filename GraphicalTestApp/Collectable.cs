﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Collectable : Entity
    {
        private AABB _hitbox;
        private string _type;

        private Sprite _sprite;

        //Timer used for timing the effects
        private Timer _timer = new Timer();

        //Constructor
        public Collectable(float x, float y, string type, string sprite)
        {
            X = x;
            Y = y;

            _type = type;

            _hitbox = new AABB(16, 16);
            AddChild(_hitbox);

            _sprite = new Sprite(sprite);
            AddChild(_sprite);

            OnUpdate += Touch;
            OnUpdate += PowerUp;
        }

        //Checks to see if the player is touching the collectable
        private void Touch(float deltaTime)
        {
            if (_hitbox == null)
            {
                return;
            }

            if (_hitbox.DetectCollision(Player.Instance.HitBox))
            {
                RemoveChild(_hitbox);
                _hitbox = null;
                RemoveChild(_sprite);

                _timer.Restart();
            }

        }

        //Applies the powerup
        private void PowerUp(float deltaTime)
        {
            if (_hitbox == null)
            {
                if (_type == "shootSpeed")
                {
                    Player.Instance.shootSpeed = 0.05f;
                }

            }

            if (_hitbox == null && _timer.Seconds >= 3)
            {
                Player.Instance.shootSpeed = 0.1f;
                Parent.RemoveChild(this);
            }
        }



    }
}
