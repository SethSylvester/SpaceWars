using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    public delegate void ShootFuncs();

    class Gun
    {
        private Actor _root;

        public Projectile projectile;
        public ShootFuncs OnShoot;
        public Gun(Actor actor)
        {
            _root = actor;
        }

        public void Shoot(float _x, float _y)
        {
            Projectile projectile = new Projectile(false);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            projectile.X = _x + 100;
            projectile.Y = _y;
            OnShoot?.Invoke();
        }

        //Handles bullets with aX velocity
        public void Shoot(float x, float y, float rotation)
        {
            Projectile projectile = new Projectile(false);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            projectile.X = x + 100;
            projectile.Y = y;
            projectile.Rotation = rotation*60;
            OnShoot?.Invoke();
        }

        //Handles the beam
        public void ShootBeam(float _x, float _y, float _z, Actor tParent)
        {
            Projectile projectile = new Projectile(false, tParent);
            _root.AddChild(projectile);
            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            projectile.AddChild(projectileSprite);
            OnShoot?.Invoke();
        }

    }

}
