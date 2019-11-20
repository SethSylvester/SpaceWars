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
            Player _player = new Player(_interface);

            //Adds the players sprite and adds the player to the scene
            _player.AddChild(toga);
            root.AddChild(_player);

            //Places the player
            _player.X = 100;
            _player.Y = 500;

            //Adds the interface
            root.AddChild(_interface);

            //The boss's thicc sprite
            Enemy enemyCenter = new Enemy();
            root.AddChild(enemyCenter);
            Sprite enemyCenterSprite = new Sprite("GFX/Boss.png");
            enemyCenter.AddChild(enemyCenterSprite);
            enemyCenter.X = 400;
            enemyCenter.Y = 100;

            //Attaching things to this lets them orbit the boss
            Spinner spinner = new Spinner();
            enemyCenter.AddChild(spinner);

            Phase1();

            void Phase1()
            {
                //Left Turrets
                SpawnWiggleGunTurret(-105,0);
                SpawnWiggleGunTurret(-175,0);
                SpawnWiggleGunTurret(-245,0);
                SpawnWiggleGunTurret(-315,0);

                //Right Turrets
                SpawnWiggleGunTurret(105, 0);
                SpawnWiggleGunTurret(175, 0);
                SpawnWiggleGunTurret(245, 0);
                SpawnWiggleGunTurret(315, 0);

                //Center Shotgun
                SpawnShotgun(0, 0);
            }

            void SpawnShotgun(float tX, float tY)
            {
                for (int i = 0; i < 4; i++)
                {
                    //Simple simple gun turrets
                    Turret turret1 = new Turret(root);
                    enemyCenter.AddChild(turret1);
                    Sprite turret1Sprite = new Sprite("");
                    turret1.AddChild(turret1Sprite);
                    turret1.X = tX;
                    turret1.Y = tY - 50;
                    turret1._rotation = i*0.25f;
                }
                for (int i = 0; i < 3; i++)
                {
                    //Simple simple gun turrets
                    Turret turret2 = new Turret(root);
                    enemyCenter.AddChild(turret2);
                    Sprite turret2Sprite = new Sprite("");
                    turret2.AddChild(turret2Sprite);
                    turret2.X = tX;
                    turret2.Y = tY - 50;
                    turret2._rotation = i*-0.25f;
                }
            }

            void SpawnBeamTurret(float tX, float tY)
            {
                //Simple simple beam turrets
                Turret turret2 = new Turret(root, 0);
                spinner.AddChild(turret2);
                Sprite turret2Sprite = new Sprite("GFX/Tanks/tankRed.png");
                turret2.AddChild(turret2Sprite);
                turret2.X = tX;
                turret2.Y = tY;

            }

            void SpawnGunTurret(float tX, float tY)
            {
                //Simple simple gun turrets
                Turret turret1 = new Turret(root);
                spinner.AddChild(turret1);
                Sprite turret1Sprite = new Sprite("GFX/Tanks/tankBlue.png");
                turret1.AddChild(turret1Sprite);
                turret1.X = tX;
                turret1.Y = tY;
            }

            void SpawnStationaryGunTurret(float tX, float tY)
            {
                //Simple simple gun turrets
                Turret turret1 = new Turret(root);
                enemyCenter.AddChild(turret1);
                Sprite turret1Sprite = new Sprite("GFX/Tanks/tankBlue.png");
                turret1.AddChild(turret1Sprite);
                turret1.X = tX;
                turret1.Y = tY;
            }

            void SpawnWiggleGunTurret(float tX, float tY)
            {
                //Simple simple gun turrets
                Turret turret1 = new Turret(root, false);
                enemyCenter.AddChild(turret1);
                Sprite turret1Sprite = new Sprite("GFX/Tanks/tankBlue.png");
                turret1.AddChild(turret1Sprite);
                turret1.X = tX;
                turret1.Y = tY;
            }

            void SpawnRotatingTurret(float tX, float tY)
            {
                //Simple simple rotating turrets
                Turret turret3 = new Turret(root, true);
                spinner.AddChild(turret3);
                Sprite turret3Sprite = new Sprite("GFX/Tanks/tankGreen.png");
                turret3.AddChild(turret3Sprite);
                turret3.X = tX;
                turret3.Y = tY;
            }

            void EnemyBossSpawn()
            {
                Entity enemLeft = new Entity();
                enemyCenter.AddChild(enemLeft);
                Sprite enemLeftSprite = new Sprite("GFX/Tanks/tankBlue.png");
                enemLeft.AddChild(enemLeftSprite);
                enemLeft.X = -110;

                Entity enemLeft2 = new Entity();
                enemyCenter.AddChild(enemLeft2);
                Sprite enemLeftSprite2 = new Sprite("GFX/Tanks/tankBlue.png");
                enemLeft2.AddChild(enemLeftSprite2);
                enemLeft2.X = -180;

                Entity enemRight = new Entity();
                enemyCenter.AddChild(enemRight);
                Sprite enemRightSprite = new Sprite("GFX/Tanks/tankBlue.png");
                enemRight.AddChild(enemRightSprite);
                enemRight.X = 110;

                //Turrets

                for (int i = 0; i < 4; i++)
                {
                    Turret turretLeft2 = new Turret(root, true);
                    enemLeft2.AddChild(turretLeft2);
                    Sprite turretLeftSprite2 = new Sprite("GFX/Tanks/barrelRed.png");
                    turretLeft2.AddChild(turretLeftSprite2);
                    turretLeft2.X = (i * 20) - 30;
                    turretLeft2.Y = 20;
                }

                //Turret turretLeft3 = new Turret(root, 0);
                //enemLeft2.AddChild(turretLeft3);
                //Sprite turretLeftSprite3 = new Sprite("GFX/Tanks/barrelRed.png");
                //turretLeft3.AddChild(turretLeftSprite3);
                //turretLeft3.X = 0;
                //turretLeft3.Y = 20;



                Turret turretLeft = new Turret(root);
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
            }

            //Turret tur = new Turret(game.Root);
            //root.AddChild(tur);
            //tur.AddChild(toga3);
            //tur.X = 300;
            //tur.Y = 200;

            //## Set up game here ##//

            game.Run();
        }
    }
}
