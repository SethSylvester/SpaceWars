using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(1280, 760, "Graphical Test Application");

            Actor root = new Actor();
            game.Root = root;

            //the interface
            Interface _interface = new Interface();

            //player items
            Sprite toga = new Sprite("GFX/Toga.png");
            Sprite coin1 = new Sprite("GFX/Coin.png");
            Sprite coin2 = new Sprite("GFX/Coin.png");
            Player _player = new Player(_interface);
            Projectile b = new Projectile(false);

            root.AddChild(_interface);

            //The boss's thicc sprite
            Enemy enemyCenter = new Enemy();
            root.AddChild(enemyCenter);
            Sprite enemyCenterSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemyCenter.AddChild(enemyCenterSprite);
            enemyCenter.X = 300;
            enemyCenter.Y = 100;

            Entity enemLeft = new Entity();
            enemyCenter.AddChild(enemLeft);
            Sprite enemLeftSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemLeft.AddChild(enemLeftSprite);
            enemLeft.X = -110;

            Entity enemRight = new Entity();
            enemyCenter.AddChild(enemRight);
            Sprite enemRightSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemRight.AddChild(enemRightSprite);
            enemRight.X = 110;

            //Turrets

            Turret turretLeft = new Turret(root, 0);
            enemLeft.AddChild(turretLeft);
            Sprite turretLeftSprite = new Sprite("GFX/Tanks/barrelRed_outline.png");
            turretLeft.AddChild(turretLeftSprite);
            turretLeft.Y = 20;

            Turret turretRight = new Turret(root, 0);
            enemRight.AddChild(turretRight);
            Sprite turretRightSprite = new Sprite("GFX/Tanks/barrelBeige_outline.png");
            turretRight.AddChild(turretRightSprite);
            turretRight.Y = 20;


            Turret turretCenterLeft = new Turret(root);
            enemyCenter.AddChild(turretCenterLeft);
            Sprite turretCenterLeftSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterLeft.AddChild(turretCenterLeftSprite);
            turretCenterLeft.X = -20;
            turretCenterLeft.Y = 40;

            Turret turretCenterRight = new Turret(root);
            enemyCenter.AddChild(turretCenterRight);
            Sprite turretCenterRightSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterRight.AddChild(turretCenterRightSprite);
            turretCenterRight.X = 20;
            turretCenterRight.Y = 40;

            //Connector struts

            Entity bridgeLeft = new Entity();
            enemyCenter.AddChild(bridgeLeft);
            Sprite bridgeLeftSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeLeft.AddChild(bridgeLeftSprite);
            bridgeLeft.X = -50;
            bridgeLeft.Rotate(1.55f);

            Entity bridgeRight = new Entity();
            enemyCenter.AddChild(bridgeRight);
            Sprite bridgeRightSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeRight.AddChild(bridgeRightSprite);
            bridgeRight.X = 50;
            bridgeRight.Rotate(1.55f);

            //Turret tur = new Turret(game.Root);
            //root.AddChild(tur);
            //tur.AddChild(toga3);
            //tur.X = 300;
            //tur.Y = 200;

            _player.AddChild(toga);
            b.AddChild(coin1);
            root.AddChild(b);
            root.AddChild(_player);

            b.Y = 500;
            b.X = 300;
            _player.X = 100;
            _player.Y = 500;
            //## Set up game here ##//

            game.Run();
        }
    }
}
