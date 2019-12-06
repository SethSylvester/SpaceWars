using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Cursor : Entity
    {
        public byte Pos = 1;
        public byte maxPos = 1;
        private bool _called = false;
        //An unfortunate workaround because OnUpdate is called twice for some reason.
        public bool selected { get; set; } = false;

        //Constructor
        public Cursor()
        {
            Sprite sprite = new Sprite("GFX/Carter.png");
            AddChild(sprite);
            //binds moveright to the WSAD.
            OnUpdate += Controls;
        }

        //##Menu stuff
        private void Controls(float deltaTime)
        {
            if (!_called)
            {
                _called = true;
            }
            else if (_called)
            {
                //Up
                if (Input.IsKeyPressed(87))
                {
                    MenuUp();
                }
                //Down
                else if (Input.IsKeyPressed(83))
                {
                    MenuDown();
                }
                else if (Input.IsKeyPressed(69))
                {
                    selected = true;
                }
                _called = false;
            }
        }

        //Teleports up, loops when needed
        private void MenuUp()
        {
            switch (Pos)
            {
                case 1:
                    Pos = maxPos;
                    if (maxPos == 5)
                    {
                        Y = 388;
                    }
                    else if (maxPos == 3)
                    {
                        Y = 325;
                    }
                    break;
                case 2:
                    Pos--;
                    Y = 260;
                    break;

                case 3:
                    Pos--;
                    Y = 300;
                    break;

                case 4:
                    Pos--;
                    Y = 325;
                    break;

                case 5:
                    Pos--;
                    Y = 355;
                    break;

            }
        }

        //Teleports down, loops when needed
        private void MenuDown()
        {
            if (Pos == 1 && maxPos != 1)
            {
                Pos++;
                Y = 300;
            }
            else if (Pos == 2 && maxPos != 2)
            {
                Pos++;
                Y = 325;
            }
            else if (Pos == 3 && maxPos != 3)
            {
                Pos++;
                Y = 355;
            }
            else if (Pos == 4 && maxPos != 4)
            {
                Pos++;
                Y = 388;
            }
            else if (Pos == 5 && maxPos != 5)
            {
                Pos = 1;
                Y = 260;
            }
            else
            {
                Pos = 1;
                Y = 260;
            }
        }


    }
}
