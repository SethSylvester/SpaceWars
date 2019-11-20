using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Spinner : Entity
    {

        public Spinner()
        {
            OnUpdate += Orbit;
        }

        private void Orbit(float deltaTime)
        {
            Rotate(0.3f * deltaTime);
        }

    }
}
