using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Cursor : Entity
    {
        public byte pos = 1;
        //The cursors maximum Position, to be set by the menu
        public byte maxPos = 1;
        private bool _called = false;
        //An unfortunate workaround because OnUpdate is called twice for some reason.
        public bool selected { get; set; } = false;

        //Constructor
        public Cursor()
        {
            Sprite sprite = new Sprite("GFX/Cursor.png");
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
            switch (pos)
            {
                case 1:
                    pos = maxPos;
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
                    pos--;
                    Y = 260;
                    break;

                case 3:
                    pos--;
                    Y = 300;
                    break;

                case 4:
                    pos--;
                    Y = 325;
                    break;

                case 5:
                    pos--;
                    Y = 355;
                    break;

            }
        }

        //Teleports down, loops when needed
        private void MenuDown()
        {
            if (pos == 1 && maxPos != 1)
            {
                pos++;
                Y = 300;
            }
            else if (pos == 2 && maxPos != 2)
            {
                pos++;
                Y = 325;
            }
            else if (pos == 3 && maxPos != 3)
            {
                pos++;
                Y = 355;
            }
            else if (pos == 4 && maxPos != 4)
            {
                pos++;
                Y = 388;
            }
            else if (pos == 5 && maxPos != 5)
            {
                pos = 1;
                Y = 260;
            }
            else
            {
                pos = 1;
                Y = 260;
            }
        }


    }
}
