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
        int hp = 0;
        int coins = 0;
        bool armor = false;

        public Interface()
        {
            OnDraw += InterfaceDraw;
        }

        //Draws the interface
        private void InterfaceDraw()
        {
            //Coins
            RL.DrawText(Convert.ToString("Coins: " + coins), 1000, 255, 25, Color.WHITE);

            //HP
            if (hp <= 0)
            {
                RL.DrawText(Convert.ToString("HP: Dead"), 1000, 155, 25, Color.RED);
            }
            else
            {
                RL.DrawText(Convert.ToString("HP: " + hp), 1000, 155, 25, Color.WHITE);
            }

            //Armor/Shields
            RL.DrawText(Convert.ToString("Armor: " + armor), 1000, 355, 25, Color.WHITE);
            RL.DrawRectangleLines(810, 0, 1, 1200, Raylib.Color.WHITE);
        }

        public void SetHP(int _hp)
        {
            hp = _hp;
        }

        public void SetCoins(int _coins)
        {
            coins = _coins;
        }

    }
}