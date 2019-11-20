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
        private int _hp = 3;
        private int _coins = 0;
        private bool _armor = false;
        private bool _iFrames = false;

        private static Player _instance;
        private AABB _hitbox;

        public static Player Instance
        {
            get { return _instance; }
        }

        public AABB Hitbox
        {
            get { return _hitbox; }
        }

        //Stuff
        private Interface _interface;

        //###Constructors###

        //Allows a custom icon to be used
        public Player(Interface _inter)
        {
            _interface = _inter;

            //Reads the keys every frame
            OnUpdate += MoveUp;
            OnUpdate += MoveDown;
            OnUpdate += MoveLeft;
            OnUpdate += MoveRight;
            //Updates the interface
            OnUpdate += StatCount;
            OnUpdate += TouchPlayer;
            //OnUpdate += Touch;

            _hitbox = new AABB(16, 16);
            AddChild(_hitbox);
            _instance = this;
        }

        void TouchPlayer(float deltaTime)
        {
            //if (Hitbox.DetectCollision
            //{
            //    _hp = 0;
            //    RemoveChild(this);
            //}

        }

        //Moves right one space
        private void MoveRight(float deltaTime)
        {
            if (Input.IsKeyDown(68) && X < 800)
            {
                X += 90 * deltaTime;
            }
        }

        //character moves left
        private void MoveLeft(float deltaTime)
        {
            if (Input.IsKeyDown(65) && X > 10)
            {
                X -= 90 * deltaTime;
            }
        }

        private void MoveUp(float deltaTime)
        {
            if (Input.IsKeyDown(87) && Y > 10)
            {
                Y -= 90 * deltaTime;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (Input.IsKeyDown(83) && Y < 750)
            {
                Y += 90 * deltaTime;
            }

        }

        //Tells the interface what to write for the stats
        private void StatCount(float deltaTime)
        {
            _interface.SetHP(_hp);
            if (Parent != null)
            {
                return;
            }
            if (_hp <= 0)
            {
                RemoveChild(this);
            }
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

        public void IFrames()
        {
            _iFrames = true;
            System.Timers.Timer IFTimer = new System.Timers.Timer();
            IFTimer.Elapsed += new ElapsedEventHandler(IFramesOver);
            IFTimer.Interval = 500; IFTimer.Enabled = true;
        }
        private void IFramesOver(object source, ElapsedEventArgs e)
        {
            //Todo: Put animations in here
            _iFrames = false;
        }


        public void Die()
        {
            if (_armor)
            {
                IFrames();
                _armor = false;
            }
            else
            {
                //Todo: Add animation
                _hp = 0;
            }
        }

        public bool GetArmor()
        {
            return _armor;
        }

        public bool Armor
        {
            get
            {
                return _armor;

            }
        }
    }
}