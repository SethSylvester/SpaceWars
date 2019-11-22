using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Projectile : Entity
    {
        //###Beam Stuff###
        //timer used for beams
        private Timer _beamTime = new Timer();

        //Sets custom BeamWidth and BeamHeight
        int _beamWidth = 16;
        int _beamHeight = 650;

        //Variables for the height of beams
        int _currentBeamHeight = 0;
        int _currentBeamHeightShrinker = 0;
        float incrementer = 0;

        //###General Projectile stuff###
        //1 = north, 2 = south
        int _direction = 2;

        //Rotation float.
        public float Rotation = 0;

        //Projectile speed
        private float _speed = 150f;

        //Determines if the projectile is friendly, so the player can't own themselves
        private bool _friendly = false;

        //###Constructors###
        //Basic constructor to lessen the codes load
        public Projectile()
        {
            //Gives the projectile a hitbox
            AddChild(Hitbox = new AABB(16, 16));
            OnUpdate += TouchPlayer;
        }

        //Basic projectile, moves downwards
        public Projectile(bool friend) : this()
        {
            //Checks to see if the projectile is friendly and thus will not damage the player
            _friendly = friend;
            OnUpdate += MoveDown;
        }

        //Basic projectile that moves up, the byte identifies it as such
        public Projectile(bool friend, byte z) : this()
        {
            //Checks to see if the projectile is friendly and thus will not damage the player
            _friendly = friend;
            OnUpdate += MoveUp;
        }

        //Basic Beam constructor
        public Projectile(bool friend, Actor tParent)
        {
            //Checks to see if the beam is friendly and thus will not damage the player
            _friendly = friend;
            //Checks to see if the beam is touching the player
            OnUpdate += TouchPlayer;
            //Gives the beam a parent
            Beam(tParent);
            //Starts the time checker function to make sure the beam has proper timing
            //Which means that it is growing and shrinking at an appropriate rate
            OnUpdate += TimeChecker;
        }

        //Custom parameter beam constructor
        public Projectile(bool friend, Actor tParent, int height, int width, int direction)
        {
            //###Custom beam parameters###
            //Which way the beam is firing
            _direction = direction;
            //The beams height and width
            _beamHeight = height;
            _beamWidth = width;

            //Checks to see if the beam is friendly and thus will not damage the player
            _friendly = friend;
            //Checks to see if the beam is touching the player
            OnUpdate += TouchPlayer;
            //Gives the beam a parent
            Beam(tParent);
            //Starts the time checker function to make sure the beam has proper timing
            //Which means that it is growing and shrinking at an appropriate rate
            OnUpdate += TimeChecker;
        }
        private void Beam(Actor tParent)
        {
            //Shoots the beam from its parent and keeps it anchored cuz its a beam
            tParent.AddChild(this);
            //Stops the beam from moving
            _speed = 0;
        }

        void TimeChecker(float deltaTime)
        {
            //Beam appears
            if (_beamTime.Seconds >= 0.003f && _currentBeamHeight < _beamHeight && _direction == 1)
            {
                //Tells the beam to fire up
                BeamFireUp();
            }
            else if (_beamTime.Seconds >= 0.003f && _currentBeamHeight < _beamHeight && _direction == 2)
            {
                //Tells the beam to fire down
                BeamFireDown();
            }

            //Beam disappears
            else if (_beamTime.Seconds >= 0.005f && _currentBeamHeight == _beamHeight && _direction == 1)
            {
                //Tells the beam to disappear if it was fired upwards
                BeamDisappearUp();
            }
            else if (_beamTime.Seconds >= 0.005f && _currentBeamHeight == _beamHeight && _direction == 2)
            {
                //Tells the beam to disappear if it was fired downwards
                BeamDisappearDown();
            }
        }

        //Currently unchanged code
        void BeamFireUp()
        {
            _beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(_beamWidth, _currentBeamHeight));
            Hitbox.Y = -_currentBeamHeight/2;
            _currentBeamHeight++;
            _currentBeamHeightShrinker = _currentBeamHeight;
        }

        void BeamFireDown()
        {
            _beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(_beamWidth, _currentBeamHeight));
            Hitbox.Y = _currentBeamHeight/2;
            _currentBeamHeight++;
            _currentBeamHeightShrinker = _currentBeamHeight;
        }

        void BeamDisappearUp()
        {
            _beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(_beamWidth, _currentBeamHeightShrinker));
            Hitbox.Y = incrementer/2 - _currentBeamHeight/2;
            incrementer++;
            _currentBeamHeightShrinker--;
            if (_currentBeamHeightShrinker == 0)
            {
                _currentBeamHeight = 0;
                incrementer = 0;
                Parent.RemoveChild(this);
            }
        }

        void BeamDisappearDown()
        {
            _beamTime.Restart();
            if (Hitbox != null)
            {
                RemoveChild(Hitbox);
            }
            AddChild(Hitbox = new AABB(_beamWidth, _currentBeamHeightShrinker));
            Hitbox.Y = incrementer + _currentBeamHeightShrinker/2;
            incrementer++;
            _currentBeamHeightShrinker--;
            if (_currentBeamHeightShrinker == 0)
            {
                _currentBeamHeight = 0;
                incrementer = 0;
                Parent.RemoveChild(this);
            }
        }

        private void TouchPlayer(float deltaTime)
        {

            if (Hitbox == null)
            {
                return;
            }
            if (Hitbox.DetectCollision(new Vector3(X, Y, 1)))
            {
                RemoveChild(this);
            }

        }


        //###Movement Directions###
        private void MoveUp(float deltaTime)
        {
            YVelocity = -_speed * deltaTime;
            XVelocity = Rotation * deltaTime;

            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }

        }

        private void MoveDown(float deltaTime)
        {
            YVelocity = +_speed * deltaTime;
            XVelocity = Rotation * deltaTime;


            if (Y < 0 || Y > 750 || X <= 0 || X >= 800)
            {
                Parent.RemoveChild(this);
            }
        }

        
        private void MoveLeft(float deltaTime)
        {
            XVelocity = -_speed * deltaTime;
        }

        private void MoveRight(float deltaTime)
        {
            XVelocity = +_speed * deltaTime;
        }

    }
}
