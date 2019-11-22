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
            Projectile projectile = new Projectile(false);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            projectile.X = x + 100;
            projectile.Y = y;
            projectile.Rotation = rotation*60;
        }

        //Shoots bullets upwards
        public void Shoot(float x, float y, float rotation, byte direction)
        {
            Projectile projectile = new Projectile(false, 0);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            projectile.X = x + 100;
            projectile.Y = y;
            projectile.Rotation = rotation*60;
        }

        //Handles the beam
        public void ShootBeam(float _x, float _y, float rotation, Actor tParent)
        {
            Projectile projectile = new Projectile(false, tParent);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
        }

    }

}
