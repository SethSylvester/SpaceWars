using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace GraphicalTestApp
{
    class Player : Entity
    {
        //Stats
        private int _hp;
        private int _coins = 0;

        //Handles IFrames
        private bool _iFrames = false;
        private Timer _iframesTimer = new Timer();

        //lets the player shoot
        private Gun _playerGun;
        //Tells the player they can shoot
        private bool _canShoot = true;
        //Sets the players gun incase they change guns
        private string _currentGun = "playerUp";
        //How fast the player can shoot
        public float shootSpeed = 0.1f;

        //The players speed
        private float Speed { get; set; } = 85f;

        //Sets the diagonal speed so the player doesn't double their movement when
        //moving diagonally
        private float DiagonalSpeed
        {
            get
            {
                return (float)Math.Sqrt((Speed * Speed) + (Speed * Speed))/2;
            }
            set
            {
               DiagonalSpeed = (float)Math.Sqrt((Speed * Speed) + (Speed * Speed))/2;
            }
        }

        //Instances the player and their hitbox
        private static Player _instance;
        private AABB _hitbox;

        //the hitbox property for when it must be refrenced
        public AABB HitBox
        {
            get { return _hitbox; }
        }

        //Limits how often the player can shoot
        private Timer _shootTimer = new Timer();

        //Sets the instance of the player to be static, allowing for the players hitbox to
        //be detected
        public static Player Instance
        {
            get { return _instance; }
        }

        //The players interface
        private Interface _interface;

        //###Constructor###
        //Allows a custom icon to be used
        public Player(Interface _inter, Actor actor, string skin, bool weenie)
        {
            //Gives the player an interface
            _interface = _inter;

            //Gives the player a Hitbox
            _hitbox = new AABB(8, 8);
            Sprite playerSprite = new Sprite(skin);

            AddChild(playerSprite);
            AddChild(_hitbox);
            _instance = this;

            //Gives the player a gun
            _playerGun = new Gun(actor);

            //Checks for Weenie mode and adjusts HP accordingly.
            if (weenie)
            {
                _hp = 3000;
            }
            else
            {
                _hp = 3;
            }

            //Reads the keys every frame
            OnUpdate += Move;
            //Updates the interface
            OnUpdate += StatCount;
            //Checks to see if touched
            OnUpdate += IFramesOff;
            //OnUpdate += Touch;
            OnUpdate += Shoot;
        }

        //The shooting function
        private void Shoot(float deltaTime)
        {
            //Gives a small delay between shots as to not spam
            if (_shootTimer.Seconds >= shootSpeed)
            {
                _canShoot = true;
                _shootTimer.Restart();
            }

            //Angles the bullets a little bit infront of the plane so you don't clip
            if (Input.IsKeyDown(32) && Input.IsKeyDown(87) && _canShoot)
            {
                _playerGun.Shoot(X - 100, Y - 20, 0, _currentGun);
                _playerGun.Shoot(X - 100, Y - 20, -0.25f, _currentGun);
                _playerGun.Shoot(X - 100, Y - 20, 0.25f, _currentGun);
                _canShoot = false;
            }

            //Normal shoot function
            else if (Input.IsKeyDown(32) && _canShoot)
            {
            _playerGun.Shoot(X - 100, Y - 10, 0, _currentGun);
            _playerGun.Shoot(X - 100, Y - 10, -0.25f, _currentGun);
            _playerGun.Shoot(X - 100, Y - 10, 0.25f, _currentGun);
                _canShoot = false;
            }
        }

        //Turns off the Iframes
        void IFramesOff(float deltaTime)
        {
            if (_iframesTimer.Seconds >= 0.5f)
            {
                _iFrames = false;
            }

        }

        //The move functions for the player
        private void Move(float deltaTime)
        {
            //Up down right
            if (Input.IsKeyDown(87) && Y > 10 &&
                    Input.IsKeyDown(83) && Y < 750 &&
                    Input.IsKeyDown(68) && X < 800)
            {
                X += Speed * deltaTime;
            }
            //Up down left
            else if (Input.IsKeyDown(87) && Y > 10 &&
                    Input.IsKeyDown(83) && Y < 750 &&
                    Input.IsKeyDown(65) && X > 10)
            {
                X -= Speed * deltaTime;
            }
            //left right up
            else if (Input.IsKeyDown(65) && X > 10 &&
                    Input.IsKeyDown(68) && X < 800 &&
                    Input.IsKeyDown(87) && Y > 10)
            {
                Y -= Speed * deltaTime;
            }
            //left right down
            else if (Input.IsKeyDown(65) && X > 10 &&
                    Input.IsKeyDown(68) && X < 800 &&
                    Input.IsKeyDown(83) && Y < 750)
            {
                Y += Speed * deltaTime;
            }

            //Up Right
            else if (Input.IsKeyDown(87) && Y > 10 &&
                Input.IsKeyDown(68) && X < 800)
            {
                Y -= DiagonalSpeed * deltaTime;
                X += DiagonalSpeed * deltaTime;
            }
            //Up Left
            else if (Input.IsKeyDown(87) && Y > 10 &&
                    Input.IsKeyDown(65) && X > 10)
            {
                Y -= DiagonalSpeed * deltaTime;
                X -= DiagonalSpeed * deltaTime;
            }
            //Down Right
            else if (Input.IsKeyDown(83) && Y < 750 &&
                Input.IsKeyDown(68) && X < 800)
            {
                Y += DiagonalSpeed * deltaTime;
                X += DiagonalSpeed * deltaTime;
            }
            //Down Left
            else if (Input.IsKeyDown(83) && Y < 750 &&
                       Input.IsKeyDown(65) && X > 10)
            {
                Y += DiagonalSpeed * deltaTime;
                X -= DiagonalSpeed * deltaTime;
            }
            //Up Down
            else if (Input.IsKeyDown(87) && Y > 10 &&
                    Input.IsKeyDown(83) && Y < 750)
            { }
            //Left Right
            else if (Input.IsKeyDown(65) && X > 10 &&
                    Input.IsKeyDown(68) && X < 800)
            { }
            //Up
            else if (Input.IsKeyDown(87) && Y > 10)
            {
                Y -= Speed * deltaTime;
            }
            //Down
            else if (Input.IsKeyDown(83) && Y < 750)
            {
                Y += Speed * deltaTime;
            }
            //Left
            else if (Input.IsKeyDown(65) && X > 10)
            {
                X -= Speed * deltaTime;
            }
            //Right
            else if (Input.IsKeyDown(68) && X < 800)
            {
                X += Speed * deltaTime;
            }

        }

        //###INTERFACE STUFF###
        //Tells the interface what to write for the stats
        private void StatCount(float deltaTime)
        {
            //Sends the HP number to the interface
            _interface.SetHP(_hp);
            if (Parent != null)
            {
                return;
            }
            if (_hp <= 0)
            {
                RemoveChild(this);
            }
        }

        //Stat changing functions
        public void CoinInc()
        {
            _coins++;
        }

        //###Damage and Iframes###
        //The function for taking damage.
        public void TakeDamage()
        {
            //todo, add animations
            if (!_iFrames)
            {
                IFrames();
                _hp--;

                if (_hp <= 0)
                {
                    Die();
                }
            }
        }

        //IFrames function, need to replace system.timers with the timer class
        public void IFrames()
        {
            _iFrames = true;
            _iframesTimer.Restart();
        }

        //Kills the player and removes them from the scene
        public void Die()
        {
            //Todo: Add animation
            _hp = 0;
            StatCount(0f);
            RemoveChild(_hitbox);
            Parent.RemoveChild(this);
        }

    }
}