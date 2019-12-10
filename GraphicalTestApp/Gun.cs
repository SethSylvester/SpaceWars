using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{

    class Gun
    {
        //Actor for the root of the scene
        private Actor _root;

        //Gun constructor
        public Gun(Actor actor)
        {
            _root = actor;
        }

        //Fires bullets straight downwards
        public void Shoot(float _x, float _y)
        {
            Projectile projectile = new Projectile(false);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            projectile.X = _x + 100;
            projectile.Y = _y;
        }

        //Handles bullets with an X velocity
        public void Shoot(float x, float y, float rotation)
        {
            if ( BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false);
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
        }

        //Shoots bullets upwards
        public void Shoot(float x, float y, float rotation, string type)
        {
            if (type == "up" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "up");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "down" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "down");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "rocket" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "rocket");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "left" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "left");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "right" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "right");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }

            else if (type == "playerUp" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(true, "playerUp");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }

            else if (type == "reverse" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "reverse");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "reverseRight" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "reverseRight");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "reverseLeft" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "reverseLeft");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
            else if (type == "reverseUp" && BossFightController.CutScene == false)
            {
                Projectile projectile = new Projectile(false, "reverseUp");
                _root.AddChild(projectile);
                Sprite projectileSprite = new Sprite("GFX/Coin.png");
                projectile.AddChild(projectileSprite);
                projectile.X = x + 100;
                projectile.Y = y;
                projectile.Rotation = rotation * 60;
            }
        }

    }

}
