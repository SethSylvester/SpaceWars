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
            projectile.X = _x + 100;
            projectile.Y = _y;
            OnShoot?.Invoke();
        }

        /*
        public void Shoot(String type, Direction _direction, float _x, float _y)
        {
            _facing = _direction;
            if (type == "metal")
            {
                Projectile projectile = new Projectile("GFX/MoltenGold.png", _facing, true);
                Game.CurrentScene.AddEntity(projectile);
                projectile.X = _x;
                projectile.Y = _y;
                OnShoot?.Invoke();
            }
            else
            {
                Projectile projectile = new Projectile("GFX/Coin.png", _facing, false);
                Game.CurrentScene.AddEntity(projectile);
                projectile.X = _x;
                projectile.Y = _y;
                OnShoot?.Invoke();
            }
        }
        */
    }

}
