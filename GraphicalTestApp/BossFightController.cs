using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class BossFightController : Actor
    {
        //Gives the Controller the root scene
        Actor _root;
        Actor _phaseOne;
        Actor _phaseTwo;
        Actor _phaseThree;
        private bool _phaseTwoStarted = false;
        private bool _phaseThreeStarted = false;

        //The main enemy.
        Enemy enemyCenter;
        Spinner spinner;

        public static bool CutScene { get; set; } = false;

        public BossFightController(Actor root)
        {
            _root = root;
            enemyCenter = new Enemy(root);
            OnUpdate += PhaseChecker;
        }

        private void PhaseChecker(float deltaTime)
        {
            if (Enemy.Instance.Phase == 2 && !_phaseTwoStarted)
            {
                //Clear previous spawns & start 2nd phase.
                PhaseOneClear();
                PhaseTwo();
            }
            else if (Enemy.Instance.Phase == 3 && !_phaseThreeStarted)
            {
                PhaseTwoClear();
                PhaseThree();
            }
        }

        public void StartUp()
        {
            _root.AddChild(enemyCenter);
            //The boss's thicc sprite
            Sprite enemyCenterSprite = new Sprite("GFX/Boss.png");
            enemyCenter.AddChild(enemyCenterSprite);
            enemyCenter.X = 400;

            //Attaching things to this lets them orbit the boss
            spinner = new Spinner();
            enemyCenter.AddChild(spinner);
            PhaseOne();
        }

        private void PhaseOne()
        {
            _phaseOne = new Actor();
            enemyCenter.AddChild(_phaseOne);
            //Spawns the platforms and the turrets that sit upon them
            GunshipPlatforms();

            Turret turretCenterLeft = new Turret(_root);
            _phaseOne.AddChild(turretCenterLeft);
            Sprite turretCenterLeftSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterLeft.AddChild(turretCenterLeftSprite);
            turretCenterLeft.X = -20;
            turretCenterLeft.Y = 40;

            Turret turretCenterRight = new Turret(_root);
            _phaseOne.AddChild(turretCenterRight);
            Sprite turretCenterRightSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterRight.AddChild(turretCenterRightSprite);
            turretCenterRight.X = 20;
            turretCenterRight.Y = 40;

            //Connector struts
            Entity bridgeLeft = new Entity();
            _phaseOne.AddChild(bridgeLeft);
            Sprite bridgeLeftSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeLeft.AddChild(bridgeLeftSprite);
            bridgeLeft.X = -50;
            bridgeLeft.Rotate(1.55f);

            Entity bridgeRight = new Entity();
            _phaseOne.AddChild(bridgeRight);
            Sprite bridgeRightSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeRight.AddChild(bridgeRightSprite);
            bridgeRight.X = 50;
            bridgeRight.Rotate(1.55f);
        }

        private void PhaseOneClear()
        {
            enemyCenter.RemoveChild(_phaseOne);
        }


        private void PhaseTwo()
        {
            _phaseTwoStarted = true;
            _phaseTwo = new Actor();
            enemyCenter.AddChild(_phaseTwo);

            Enemy.Instance.attackTimer.Restart();
            //Left Turrets
            SpawnWiggleGunTurret(-105, 0);
            SpawnWiggleGunTurret(-175, 0);
            SpawnWiggleGunTurret(-245, 0);
            SpawnWiggleGunTurret(-315, 0);

            //Right Turrets
            SpawnWiggleGunTurret(105, 0);
            SpawnWiggleGunTurret(175, 0);
            SpawnWiggleGunTurret(245, 0);
            SpawnWiggleGunTurret(315, 0);

            //Center Shotgun
            SpawnShotgun(0, 0);
        }
        private void PhaseTwoClear()
        {
            enemyCenter.RemoveChild(_phaseTwo);
        }

        private void PhaseThree()
        {
            _phaseThreeStarted = true;
            _phaseThree = new Actor();
            enemyCenter.AddChild(_phaseThree);

            SpawnReverseGunTurret(5, 0);

            SpawnRotatingTurret(-105, 0);
            SpawnRotatingTurret(105, 0);
            SpawnRotatingTurret(0, 105);
            SpawnRotatingTurret(0, -105);
            SpawnRotatingTurret(65f, 65f);
            SpawnRotatingTurret(-65f, 65f);
            SpawnRotatingTurret(65f, -65f);
            SpawnRotatingTurret(-65f, -65f);

        }

        //###SPAWNERS
        void SpawnReverseGunTurret(float tX, float tY)
        {
            //Simple simple rotating turrets
            Turret turret3 = new Turret(_root, "reverse");
            spinner.AddChild(turret3);
            Sprite turret3Sprite = new Sprite("GFX/Tanks/tankGreen.png");
            turret3.AddChild(turret3Sprite);
            turret3.X = tX;
            turret3.Y = tY;
        }


        private void GunshipPlatforms()
        {
            //Gun Platforms
            Entity enemLeft = new Entity();
            _phaseOne.AddChild(enemLeft);
            Sprite enemLeftSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemLeft.AddChild(enemLeftSprite);
            enemLeft.X = -110;

            Entity enemLeft2 = new Entity();
            _phaseOne.AddChild(enemLeft2);
            Sprite enemLeftSprite2 = new Sprite("GFX/Tanks/tankBlue.png");
            enemLeft2.AddChild(enemLeftSprite2);
            enemLeft2.X = -180;

            Entity enemRight = new Entity();
            _phaseOne.AddChild(enemRight);
            Sprite enemRightSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemRight.AddChild(enemRightSprite);
            enemRight.X = 110;

            //Turrets for the platforms
            for (int i = 0; i < 4; i++)
            {
                Turret turretLeft2 = new Turret(_root, "rotate");
                enemLeft2.AddChild(turretLeft2);
                Sprite turretLeftSprite2 = new Sprite("GFX/Tanks/barrelRed.png");
                turretLeft2.AddChild(turretLeftSprite2);
                turretLeft2.X = (i * 20) - 30;
                turretLeft2.Y = 20;
            }

            Turret turretLeft = new Turret(_root);
            enemLeft.AddChild(turretLeft);
            Sprite turretLeftSprite = new Sprite("GFX/Tanks/barrelRed_outline.png");
            turretLeft.AddChild(turretLeftSprite);
            turretLeft.Y = 20;

            Turret turretRight = new Turret(_root, "beam");
            enemRight.AddChild(turretRight);
            Sprite turretRightSprite = new Sprite("GFX/Tanks/barrelBeige_outline.png");
            turretRight.AddChild(turretRightSprite);
            turretRight.Y = 20;

        }

        void SpawnShotgun(float tX, float tY)
        {
            for (int i = 0; i < 4; i++)
            {
                //Simple simple gun turrets
                Turret turret1 = new Turret(_root);
                _phaseTwo.AddChild(turret1);
                Sprite turret1Sprite = new Sprite("");
                turret1.AddChild(turret1Sprite);
                turret1.X = tX;
                turret1.Y = tY - 50;
                turret1._rotation = i * 0.25f;
            }
            for (int i = 0; i < 3; i++)
            {
                //Simple simple gun turrets
                Turret turret2 = new Turret(_root);
                _phaseTwo.AddChild(turret2);
                Sprite turret2Sprite = new Sprite("");
                turret2.AddChild(turret2Sprite);
                turret2.X = tX;
                turret2.Y = tY - 50;
                turret2._rotation = i * -0.25f;
            }
        }
        void SpawnWiggleGunTurret(float tX, float tY)
        {
            //Simple simple gun turrets
            Turret turret1 = new Turret(_root, "wiggle");
            _phaseTwo.AddChild(turret1);
            Sprite turret1Sprite = new Sprite("GFX/Tanks/tankBlue.png");
            turret1.AddChild(turret1Sprite);
            turret1.X = tX;
            turret1.Y = tY;
        }

        void SpawnRotatingTurret(float tX, float tY)
        {
            //Simple simple rotating turrets
            Turret turret3 = new Turret(_root, "rotate");
            spinner.AddChild(turret3);
            Sprite turret3Sprite = new Sprite("GFX/Tanks/tankGreen.png");
            turret3.AddChild(turret3Sprite);
            turret3.X = tX;
            turret3.Y = tY;
        }

    }

}
