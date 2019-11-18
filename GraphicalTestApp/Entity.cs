using System;

namespace GraphicalTestApp
{
    class Entity : Actor
    {
        private Vector3 _velocity = new Vector3();
        private Vector3 _acceleration = new Vector3();

        public AABB Hitbox { get; set; }
    

        public float XVelocity
        {
            get
            {
                return _velocity.x;
                //return _translation.m13;
            }
            set
            {
                _velocity.x = value;
                //_translation.SetTranslation(value, YVelocity, 1);
            }
        }

        public float XAcceleration
        {
            //## Implement acceleration on the X axis ##//
            get { return 0; }
            set { }
        }
        public float YVelocity
        {
            get
            {
                return _velocity.y;
                //return _translation.m23;
            }
            set
            {
                _velocity.y = value;
                //_translation.SetTranslation(XVelocity, value, 1);
            }
        }
        public float YAcceleration
        {
            //## Implement acceleration on the Y axis ##//
            get { return 0; }
            set { }
        }

        public Sprite _Sprite { get; set; }

        //Empty constructor
        public Entity()
        {

        }

        //Creates an Entity at the specified coordinates
        public Entity(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override void Update(float deltaTime)
        {
            OnUpdate?.Invoke(deltaTime);

            //## Calculate velocity from acceleration ##//
            //## Calculate position from velocity ##//

            X += _velocity.x;
            Y += _velocity.y;

            base.Update(deltaTime);
        }
    }
}
