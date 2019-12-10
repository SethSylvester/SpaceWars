using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class BossFightController : Actor
    {
        //Gives the Controller the root scene
        private Actor _root;
        //The various phases the boss goes through.
        private Actor _phaseOne;
        private Actor _phaseTwo;
        private Actor _phaseThree;
        private Actor _phaseFour;
        private Actor _phaseFive;
        private Actor _phaseSix;
        private Actor _phaseSeven;

        //The current phase so the spawn functions can be universal
        private Actor CurrentPhase { get; set; }

        //Bools to check if the stage has already been started
        private bool _phaseOneStarted = false;
        private bool _phaseTwoStarted = false;
        private bool _phaseThreeStarted = false;
        private bool _phaseFourStarted = false;
        private bool _phaseFiveStarted = false;
        private bool _phaseSixStarted = false;
        private bool _phaseSevenStarted = false;

        //Bool to check if the boss is currently attacking
        private bool _attacking = false;

        //Byte to change between two different attacks
        private byte attackNum = 1;

        //The bosses sprite
        Sprite enemyCenterSprite;

        //The main enemy.
        private Enemy enemyCenter;
        private Spinner spinner;

        //Timer class
        private Timer _attackTimer = new Timer();
        private Timer _attackTimer2 = new Timer();

    
        //Checks to see if the game is in a rudimentary cutscene.
        public static bool CutScene { get; set; } = false;

        //Constructor
        public BossFightController(Actor root, byte startPhase)
        {
            _root = root;
            enemyCenter = new Enemy(root, startPhase);
            OnUpdate += PhaseChecker;
        }

        //The startup function
        public void StartUp()
        {
            _root.AddChild(enemyCenter);
            //The boss's thicc sprite
            enemyCenterSprite = new Sprite("GFX/Boss.png");
            enemyCenter.AddChild(enemyCenterSprite);
            enemyCenter.X = 400;

            //Attaching things to this lets them orbit the boss
            spinner = new Spinner();
        }

        //Checks the phase every frame to see if it is updated and needs to be changed
        private void PhaseChecker(float deltaTime)
        {
            if (Enemy.Instance.Phase == 1 && !_phaseOneStarted)
            {
                PhaseOneStart();
                PhaseOne();
            }
            else if (Enemy.Instance.Phase == 2 && !_phaseTwoStarted)
            {
                //Clear previous spawns & start 2nd phase.
                PhaseTwoStart();
                PhaseTwo();
            }
            else if (Enemy.Instance.Phase == 3)
            {
                if (!_phaseThreeStarted)
                {
                    PhaseThreeStart();
                }
                PhaseThree();
            }
            else if (Enemy.Instance.Phase == 4 && !_phaseFourStarted)
            {
                PhaseFourStart();
                PhaseFour();
            }
            else if (Enemy.Instance.Phase == 5)
            {
                if (!_phaseFiveStarted)
                {
                    PhaseFiveStart();
                }
                PhaseFive(deltaTime);
            }
            else if (Enemy.Instance.Phase == 6 && !_phaseSixStarted)
            {
                PhaseSixStart();
                PhaseSix();
            }
            else if (Enemy.Instance.Phase == 7 && !_phaseSevenStarted)
            {
                PhaseSevenStart();
                PhaseSeven();
            }
        }

        //###CLEAR FUNCTIONS###
        //These clear the previous stage
        private void PhaseOneClear()
        {
            enemyCenter.RemoveChild(_phaseOne);
        }
        private void PhaseTwoClear()
        {
            enemyCenter.RemoveChild(_phaseTwo);
        }

        private void PhaseThreeClear()
        {
            enemyCenter.RemoveChild(_phaseThree);
            enemyCenter.RemoveChild(spinner);

            //Makes the enemy stop moving
            Enemy.Instance.XVelocity = 0;
            Enemy.Instance.YVelocity = 0;

            //recenters the enemy
            enemyCenter.X = 400;
            enemyCenter.Y = 100;
        }
        private void PhaseFourClear()
        {
            enemyCenter.RemoveChild(_phaseFour);
        }
        private void PhaseFiveClear()
        {
            enemyCenter.RemoveChild(_phaseFive);
        }
        
        private void PhaseSixClear()
        {
            enemyCenter.RemoveChild(_phaseSix);
            enemyCenter.RemoveChild(enemyCenterSprite);

            //Makes the enemy stop moving
            enemyCenter.XVelocity = 0f;
            enemyCenter.YVelocity = 0f;
        }

        //###STARTS###
        private void PhaseOneStart()
        {
            //Ensures the phase isn't started twice
            _phaseOneStarted = true;

            //Spawns the new phase
            _phaseOne = new Actor();
            enemyCenter.AddChild(_phaseOne);

            CurrentPhase = _phaseOne;
        }
        private void PhaseTwoStart()
        {
            //Ensures the phase isn't started twice
            _phaseTwoStarted = true;
       
            //Clears the previous stage
            PhaseOneClear();

            //Spawns the new phase
            _phaseTwo = new Actor();
            CurrentPhase = _phaseTwo;

            enemyCenter.AddChild(_phaseTwo);
        }
        private void PhaseThreeStart()
        {
            //Ensures the phase isn't started twice
            _phaseThreeStarted = true;
            //Clears the previous stage
            PhaseTwoClear();

            //Spawns the new phase
            _phaseThree = new Actor();
            enemyCenter.AddChild(_phaseThree);
            _phaseThree.AddChild(spinner);

            CurrentPhase = _phaseThree;
        }

        private void PhaseFourStart()
        {
            //Ensures the phase isn't started twice
            _phaseFourStarted = true;

            //Clears the previous stage
            PhaseThreeClear();

            //Spawns the new phase
            _phaseFour = new Actor();
            CurrentPhase = _phaseFour;

            enemyCenter.AddChild(_phaseFour);
        }

        private void PhaseFiveStart()
        {
            //Ensures the phase isn't started twice
            _phaseFiveStarted = true;
            //Clears the previous stage
            PhaseFourClear();

            //Spawns the new phase
            _phaseFive = new Actor();
            CurrentPhase = _phaseFive;

            enemyCenter.AddChild(_phaseFive);
            _attackTimer.Restart();
        }

        private void PhaseSixStart()
        {
            //Ensures the phase isn't started twice
            _phaseSixStarted = true;

            //Spawns the new phase
            _phaseSix = new Actor();

            //Clears the previous stage
            PhaseFiveClear();
            spinner = new Spinner();

            enemyCenter.AddChild(_phaseSix);
            _phaseSix.AddChild(spinner);

            CurrentPhase = _phaseSix;
        }


        //Calls everything required to start the new phase
        private void PhaseSevenStart()
        {
            //Ensures the phase isn't started twice
            _phaseSevenStarted = true;
            //Clears the previous stage
            PhaseSixClear();

            //Re-centers the enemy after he bounced everywhere
            enemyCenter.X = 400;
            enemyCenter.Y = 200;

            //Spawns the new phase
            _phaseSeven = new Actor();
            enemyCenter.AddChild(_phaseSeven);

            //He accepts defeat and changes his sprite to a more fitting one.
            enemyCenterSprite = new Sprite("GFX/BossEyeClosed.png");
            enemyCenter.AddChild(enemyCenterSprite);

            CurrentPhase = _phaseSeven;
        }


        //###PHASES###
        private void PhaseOne()
        {
            //Spawns the platforms and the turrets that sit upon them
            GunshipPlatforms();

            Turret turretCenterLeft = new Turret(_root);
            CurrentPhase.AddChild(turretCenterLeft);
            Sprite turretCenterLeftSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterLeft.AddChild(turretCenterLeftSprite);
            turretCenterLeft.X = -20;
            turretCenterLeft.Y = 40;

            Turret turretCenterRight = new Turret(_root);
            CurrentPhase.AddChild(turretCenterRight);
            Sprite turretCenterRightSprite = new Sprite("GFX/Tanks/barrelGreen.png");
            turretCenterRight.AddChild(turretCenterRightSprite);
            turretCenterRight.X = 20;
            turretCenterRight.Y = 40;

            //Connector struts
            Entity bridgeLeft = new Entity();
            CurrentPhase.AddChild(bridgeLeft);
            Sprite bridgeLeftSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeLeft.AddChild(bridgeLeftSprite);
            bridgeLeft.X = -50;
            bridgeLeft.Rotate(1.55f);

            Entity bridgeRight = new Entity();
            CurrentPhase.AddChild(bridgeRight);
            Sprite bridgeRightSprite = new Sprite("GFX/Tanks/barrelBlue.png");
            bridgeRight.AddChild(bridgeRightSprite);
            bridgeRight.X = 50;
            bridgeRight.Rotate(1.55f);
        }

        private void PhaseTwo()
        {
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

        void PhaseThree()
        {
            if (_attackTimer.Seconds >= 2f)
            {
                _attackTimer.Restart();
                for (int i = -415; i <= 380; i += 60)
                {
                    //X, Y
                    SpawnRotationGun(i, -90);
                }
            }
        }

        private void PhaseFour()
        {
            spinner = new Spinner("fast");
            CurrentPhase.AddChild(spinner);
            
            SpawnRotatingTurret(-105, 0);
            SpawnRotatingTurret(105, 0);
            SpawnRotatingTurret(0, 105);
            SpawnRotatingTurret(0, -105);
            
            SpawnReverseGunTurret2();
        }

        void PhaseFive(float deltaTime)
        {
            //Make the beams full size and throw them downwards since tehy come off the screen anyways
            BeamWarning();
            BeamLadder(deltaTime);
        }

        private void PhaseSix()
        {
            SpawnReverseGunTurret();

            SpawnRotatingTurret(-105, 0);
            SpawnRotatingTurret(105, 0);
            SpawnRotatingTurret(0, 105);
            SpawnRotatingTurret(0, -105);
            SpawnRotatingTurret(65f, 65f);
            SpawnRotatingTurret(-65f, 65f);
            SpawnRotatingTurret(65f, -65f);
            SpawnRotatingTurret(-65f, -65f);
        }

        private void PhaseSeven()
        {
            Ladder hand = new Ladder(true);
            CurrentPhase.AddChild(hand);
            hand.Y = -1000;
        }

        //###SPAWNERS
        void SpawnReverseGunTurret()
        {
            //Simple simple rotating turrets
            Turret turret3 = new Turret(_root, "reverse");
            spinner.AddChild(turret3);
            Sprite turret3Sprite = new Sprite("GFX/Tanks/tankGreen.png");
            turret3.AddChild(turret3Sprite);
        }
        void SpawnReverseGunTurret2()
        {
            //Simple simple rotating turrets
            Turret turret3 = new Turret(_root, "reverse2");
            spinner.AddChild(turret3);
        }

        //The gunship platforms for phase 1
        private void GunshipPlatforms()
        {
            //Gun Platforms
            Entity enemLeft = new Entity();
            CurrentPhase.AddChild(enemLeft);
            Sprite enemLeftSprite = new Sprite("GFX/Tanks/tankBlue.png");
            enemLeft.AddChild(enemLeftSprite);
            enemLeft.X = -110;

            Entity enemLeft2 = new Entity();
            CurrentPhase.AddChild(enemLeft2);
            Sprite enemLeftSprite2 = new Sprite("GFX/Tanks/tankBlue.png");
            enemLeft2.AddChild(enemLeftSprite2);
            enemLeft2.X = -180;

            Entity enemRight = new Entity();
            CurrentPhase.AddChild(enemRight);
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

            Turret turretRight = new Turret(_root, "rocket");
            enemRight.AddChild(turretRight);
            Sprite turretRightSprite = new Sprite("GFX/Tanks/barrelBeige_outline.png");
            turretRight.AddChild(turretRightSprite);
            turretRight.Y = 20;

        }

        //Spawns the "shotgun", which is a load of turrets firing in an arc
        private void SpawnShotgun(float tX, float tY)
        {
            for (int i = 0; i < 4; i++)
            {
                //Simple simple gun turrets
                Turret turret1 = new Turret(_root);
                CurrentPhase.AddChild(turret1);
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
                CurrentPhase.AddChild(turret2);
                Sprite turret2Sprite = new Sprite("");
                turret2.AddChild(turret2Sprite);
                turret2.X = tX;
                turret2.Y = tY - 50;
                turret2._rotation = i * -0.25f;
            }
        }

        //Spawns turrets that wiggle back adn forth quickly
        void SpawnWiggleGunTurret(float tX, float tY)
        {
            //Simple simple gun turrets
            Turret turret1 = new Turret(_root, "wiggle");
            CurrentPhase.AddChild(turret1);
            Sprite turret1Sprite = new Sprite("GFX/Tanks/tankBlue.png");
            turret1.AddChild(turret1Sprite);
            turret1.X = tX;
            turret1.Y = tY;
        }

        //Spawns turrets that wiggle back and forth slowly
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

        //Spawns a gun that fires in 360 degrees whose bullets eventualy return to it
        private void SpawnRotationGun(float x, float y)
        {
            spinner = new Spinner(x, y, 50, "fast");
            Projectile proj = new Projectile();
            Projectile proj1 = new Projectile();
            Projectile proj2 = new Projectile();
            Projectile proj3 = new Projectile();
            Projectile proj4 = new Projectile();
            Projectile proj5 = new Projectile();

            CurrentPhase.AddChild(spinner);

            spinner.AddChild(proj);
            spinner.AddChild(proj1);
            spinner.AddChild(proj2);
            spinner.AddChild(proj3);
            spinner.AddChild(proj4);
            spinner.AddChild(proj5);

            Sprite projectileSprite = new Sprite("GFX/Coin.png");
            Sprite projectileSprite3 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite2 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite4 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite5 = new Sprite("GFX/Coin.png");
            Sprite projectileSprite6 = new Sprite("GFX/Coin.png");

            proj.AddChild(projectileSprite);
            proj1.AddChild(projectileSprite3);
            proj2.AddChild(projectileSprite4);
            proj3.AddChild(projectileSprite2);
            proj4.AddChild(projectileSprite5);
            proj5.AddChild(projectileSprite6);
            proj.X = 10;
            proj1.X = 20;
            proj2.X = 30;
            proj3.X = -10;
            proj4.X = -20;
        }

        //Gives a warning beams are coming
        private void BeamWarning()
        {
            if (_attackTimer.Seconds >= 3f && _attacking)
            {
                _attacking = false;
                _attackTimer.Restart();
            }

            if (!_attacking && _attackTimer.Seconds <= 0.6f ||
                !_attacking && _attackTimer.Seconds >= 1 && _attackTimer.Seconds <= 1.6f)
            {
                if (attackNum == 1)
                {
                    for (int i = 0; i < 800; i += 100)
                    {
                        //posx, posy, width, height
                        RL.DrawRectangle(i + 7, 0, 46, 75, Color.MAGENTA);
                    }
                }

                else
                {
                    for (int i = 50; i < 800; i += 100)
                    {
                        //posx, posy, width, height
                        RL.DrawRectangle(i + 7, 0, 46, 75, Color.MAGENTA);
                    }
                }
            }
            else if (_attackTimer.Seconds >= 1.7 && !_attacking)
            {
                switch (attackNum)
                {
                    case 1:
                        _attacking = true;
                        BeamAttack1();
                        break;
                    case 2:
                        _attacking = true;
                        BeamAttack2();
                        break;
                }
            }
        }

        //A vertical beam attack
        private void BeamAttack1()
        {
            _attackTimer.Restart();
            attackNum++;
            for (int i = 0; i < 800; i += 100)
            {
                Ladder lad = new Ladder(50, 400);
                CurrentPhase.AddChild(lad);
                lad.X = i - 370;
                lad.Y = -500;
            }
        }
        private void BeamAttack2()
        {
            _attackTimer.Restart();
            attackNum--;
            for (int i = 50; i < 800; i += 100)
            {
                Ladder lad = new Ladder(50, 400);
                CurrentPhase.AddChild(lad);
                lad.X = i - 370;
                lad.Y = -500;
            }
        }

        //The beam ladder that goes up and down during phase 5
        private void BeamLadder(float deltaTime)
        {
            if (_attackTimer2.Seconds >= 1.1)
            {
                _attackTimer2.Restart();
                Ladder ladder = new Ladder(1);
                Ladder ladder2 = new Ladder(2);
                _phaseFive.AddChild(ladder);
                _phaseFive.AddChild(ladder2);
                ladder.X = 210;
                ladder2.X = -200;
                ladder.Y = 749;
                ladder2.Y = -120;
            }
        }

    }

}
