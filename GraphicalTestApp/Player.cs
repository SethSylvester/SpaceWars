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
        private int _hp = 300;
        private int _coins = 0;
        private bool _armor = false;
        private bool _iFrames = false;
        private Timer _iframesTimer = new Timer();

        //lets the player shoot
        private Gun _playerGun;
        private bool _canShoot = true;
        private string _currentGun = "playerUp";
        public float shootSpeed = 0.1f;

        //The players speed
        private float Speed { get; set; } = 80f;
        //Sets the diagonal speed
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

        //Armor Property
        public bool Armor
        {
            get
            {
                return _armor;

            }
        }

        //Instances the player and their hitbox
        private static Player _instance;
        private AABB _hitbox;

        public AABB HitBox
        {
            get { return _hitbox; }
        }

        private Timer _shootTimer = new Timer();

        public static Player Instance
        {
            get { return _instance; }
        }

        //Stuff
        private Interface _interface;

        //###Constructor###
        //Allows a custom icon to be used
        public Player(Interface _inter, Actor actor)
        {
            //Gives the player an interface
            _interface = _inter;

            //Gives the player a Hitbox
            _hitbox = new AABB(8, 8);
            Sprite toga = new Sprite("GFX/Toga.png");

            AddChild(toga);
            AddChild(_hitbox);
            _instance = this;

            //Gives the player a gun
            _playerGun = new Gun(actor);

            //Reads the keys every frame
            OnUpdate += Move;
            //Updates the interface
            OnUpdate += StatCount;
            //Checks to see if touched
            OnUpdate += Touch;
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

        void Touch(float deltaTime)
        {
            if (_iframesTimer.Seconds >= 0.5f)
            {
                _iFrames = false;
            }

        }

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

        private void MoveUp(float deltaTime)
        {
            if (Input.IsKeyDown(87) && Y > 10)
            {
                Y -= Speed * deltaTime;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (Input.IsKeyDown(83) && Y < 750)
            {
                Y += Speed * deltaTime;
            }

        }

        //Moves right one space
        private void MoveRight(float deltaTime)
        { 
            //Checks to make sure D is pressed & not OOB
            if (Input.IsKeyDown(68) && X < 800)
            {
                X += Speed * deltaTime;
            }
        }

        //character moves left
        private void MoveLeft(float deltaTime)
        {
            //Checks to make sure A is pressed & not OOB
            if (Input.IsKeyDown(65) && X > 10)
            {
                X -= Speed * deltaTime;
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
            //Sends the coins number to the interface
            _interface.SetCoins(_coins);
        }

        //Stat changing functions
        public void CoinInc()
        {
            _coins++;
        }

        //###Damage and Iframes###
        public void TakeDamage()
        {
            //todo, add animations
            if (!_iFrames)
            {
                IFrames();
                if (!_armor)
                {
                    _hp--;
                    if (_hp <= 0)
                    {
                        Die();
                    }
                }
                else
                {
                    _armor = false;
                }
            }
        }

        //void Touch(float DeltaTime)
        //{
        //    if (_Sprite.Hitbox.Overlaps(new Vector3(X, Y, 1)))
        //    {
        //        RemoveChild(this);
        //    }
        //}


            //IFrames function, need to replace system.timers with the timer class
        public void IFrames()
        {
            _iFrames = true;
            _iframesTimer.Restart();
        }

        //Instantly kills the player
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