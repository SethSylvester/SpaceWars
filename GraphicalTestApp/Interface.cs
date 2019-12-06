using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Interface : Entity
    {
        //The interfaces numbers that it updates
        private int _hp = 0;

        //Interface constructor
        public Interface()
        {
            OnDraw += InterfaceDraw;
        }

        //Draws the interface
        private void InterfaceDraw()
        {

            //HP
            if (_hp <= 0)
            {
                RL.DrawText(Convert.ToString("HP: Dead"), 1000, 155, 25, Color.RED);
            }
            else
            {
                RL.DrawText(Convert.ToString("HP: " + _hp), 1000, 155, 25, Color.WHITE);
            }

            RL.DrawRectangleLines(810, 0, 1, 1200, Raylib.Color.WHITE);

            if (Enemy.Instance.HP <= 0)
            {
                RL.DrawText("You win!", 300, 20, 45, Color.GOLD);
            }

        }

        //Sets the current HP & Coins
        public void SetHP(int hp)
        {
            _hp = hp;
        }

    }
}