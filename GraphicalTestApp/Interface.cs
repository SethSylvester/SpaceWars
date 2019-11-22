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
        private int _hp = 0;
        private int _coins = 0;
        private bool _armor = false;

        //Interface constructor
        public Interface()
        {
            OnDraw += InterfaceDraw;
        }

        //Draws the interface
        private void InterfaceDraw()
        {
            //Coins
            RL.DrawText(Convert.ToString("Coins: " + _coins), 1000, 255, 25, Color.WHITE);

            //HP
            if (_hp <= 0)
            {
                RL.DrawText(Convert.ToString("HP: Dead"), 1000, 155, 25, Color.RED);
            }
            else
            {
                RL.DrawText(Convert.ToString("HP: " + _hp), 1000, 155, 25, Color.WHITE);
            }

            //Armor/Shields
            RL.DrawText(Convert.ToString("Armor: " + _armor), 1000, 355, 25, Color.WHITE);
            RL.DrawRectangleLines(810, 0, 1, 1200, Raylib.Color.WHITE);
        }

        public void SetHP(int hp)
        {
            _hp = hp;
        }

        public void SetCoins(int coins)
        {
            _coins = coins;
        }

    }
}