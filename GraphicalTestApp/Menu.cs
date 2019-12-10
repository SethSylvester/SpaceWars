using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Menu : Actor
    {
        //The sprite displayer for the customization menu
        private Sprite _spriteDisplay;

        //The byte to determine which section of the menu you're selecting
        private byte _choice;
        //The byte that determines which stage you start at
        private byte _startingStage = 1;

        //The bool to determine which menu you're currently in
        private bool _mainMenu = true;
        private bool _stageSelect = false;
        private bool _customizeMenu = false;
        private bool _weenieMode = false;

        //Byte & String array for the skin previewer and selecter
        private byte _selectedSkinNum = 0;
        private string[] _skins = new string[4];

        //The cursor
        Cursor cursor;

        public Menu()
        {
            //The four skins you're allowed to pick from
            _skins[0] = "GFX/Toga.png";
            _skins[1] = "GFX/Carter.png";
            _skins[2] = "GFX/Jax.png";
            _skins[3] = "GFX/Hunter.png";

            //loads in the default skin
            _spriteDisplay = new Sprite(_skins[0]);

            //Spawns the cursor
            cursor = new Cursor();
            cursor.maxPos = 5;
            cursor.X = 425;
            cursor.Y = 260;
            AddChild(cursor);

            //OnUpdate that draws the menu and checks to see if you selected anything
            OnUpdate += Selected;
            OnDraw += MenuDraw;
        }

        private void Selected(float deltaTime)
        {
            //###MAIN MENU FUNCTIONS###
            //Starts the game
            if (_mainMenu && cursor.selected == true && cursor.Pos == 1)
            {
                StartGame();
            }
            //Brings up the stage selector
            else if (_mainMenu && cursor.selected == true && cursor.Pos == 2)
            {
                _mainMenu = false;
                _stageSelect = true;
                cursor.maxPos = 3;
                cursor.Pos = 1;
                cursor.Y = 260;
            }
            //Brings up the skin selector
            else if (_mainMenu && cursor.selected == true && cursor.Pos == 3)
            {
                AddChild(_spriteDisplay);
                _spriteDisplay.X = 500;
                _spriteDisplay.Y = 200;

                _mainMenu = false;
                _customizeMenu = true;
                cursor.maxPos = 3;
                cursor.Pos = 1;
                cursor.Y = 260;
            }
            //Turns on Weenie mode
            else if (_mainMenu && cursor.selected == true && cursor.Pos == 4 &&
                !_weenieMode)
            {
                _weenieMode = true;
            }
            //Turns off weenie mode
            else if (_mainMenu && cursor.selected == true && cursor.Pos == 4 &&
                _weenieMode)
            {
                _weenieMode = false;
            }
            //Closes the game
            else if (_mainMenu && cursor.selected == true && cursor.Pos == 5)
            {
                Game.Exit = true;
            }
            //###STAGE SELECTOR MENU###
            //Ensures the starting stage cannot be over six
            else if (_stageSelect && cursor.selected == true && cursor.Pos == 1 &&
                _startingStage < 6)
            {
                _startingStage++;
            }

            //Ensures the starting stage cannot be below 1
            else if (_stageSelect && cursor.selected == true && cursor.Pos == 2 &&
                _startingStage > 1)
            {
                _startingStage--;
            }

            //Exits out of the stage selector
            else if (_stageSelect && cursor.selected == true && cursor.Pos == 3)
            {
                _stageSelect = false;
                _mainMenu = true;
                cursor.maxPos = 5;
                cursor.Pos = 1;
                cursor.Y = 260;
            }

            //###SKIN SELECTION MENU###
            //Selects the next skin
            else if (_customizeMenu && cursor.selected == true && cursor.Pos == 1 &&
                _selectedSkinNum < _skins.Length - 1)
            {
                RemoveChild(_spriteDisplay);

                _selectedSkinNum++;
                _spriteDisplay = new Sprite(_skins[_selectedSkinNum]);
                AddChild(_spriteDisplay);
                _spriteDisplay.X = 500;
                _spriteDisplay.Y = 200;
            }
            //Selects the previous skin
            else if (_customizeMenu && cursor.selected == true && cursor.Pos == 2 &&
                _selectedSkinNum > 0)
            {
                RemoveChild(_spriteDisplay);

                _selectedSkinNum--;
                _spriteDisplay = new Sprite(_skins[_selectedSkinNum]);
                AddChild(_spriteDisplay);
                _spriteDisplay.X = 500;
                _spriteDisplay.Y = 200;
            }
            //Exits the skin selection menu
            else if (_customizeMenu && cursor.selected == true && cursor.Pos == 3)
            {
                RemoveChild(_spriteDisplay);

                _stageSelect = false;
                _mainMenu = true;
                cursor.maxPos = 5;
                cursor.Pos = 1;
                cursor.Y = 260;
            }
            //Tells the cursor to stop selecting something after a press
            cursor.selected = false;
        }

        //Draws the menu and writes out the text
        private void MenuDraw()
        {
            //Gets the current text
            _choice = cursor.Pos;
            //Text, PosX, PosY, Size, Color
            //Draws the main menus options
            if (_mainMenu)
            {
                RL.DrawText(Convert.ToString("Space Defender"), 275, 150, 75, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString("Start"), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Start"), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString("Select Stage"), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Select Stage"), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString("Customize"), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Customize"), 450, 315, 25, Color.WHITE);
                }
                if (_weenieMode)
                {
                    RL.DrawText(Convert.ToString("Weenie Mode"), 450, 345, 25, Color.PINK);
                }
                else if (_choice == 4)
                {
                    RL.DrawText(Convert.ToString("Weenie Mode"), 450, 345, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Weenie Mode"), 450, 345, 25, Color.WHITE);
                }
                if (_choice == 5)
                {
                    RL.DrawText(Convert.ToString("Exit"), 450, 375, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Exit"), 450, 375, 25, Color.WHITE);
                }
            }

            //The menu for selecting a stage
            else if (_stageSelect)
            {
                RL.DrawText(Convert.ToString("Select Stage"), 450, 150, 25, Color.WHITE);
                RL.DrawText(Convert.ToString(_startingStage), 520, 190, 50, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString("Next Stage"), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Next Stage"), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString("Previous Stage"), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Previous Stage"), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString("Back"), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Back"), 450, 315, 25, Color.WHITE);
                }
            }

            //The menu for the skin customization
            else if (_customizeMenu)
            {
                RL.DrawText(Convert.ToString("Select Skin"), 450, 150, 25, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString("Next Skin"), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Next Skin"), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString("Previous Skin"), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Previous Skin"), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString("Back"), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString("Back"), 450, 315, 25, Color.WHITE);
                }
            }
        }

        //Starts the game
        private void StartGame()
        {
            //the interface
            Interface _interface = new Interface();
            //Adds the interface
            Parent.AddChild(_interface);

            BossFightController level = new BossFightController(Parent, _startingStage);

            Parent.AddChild(level);

            //player items
            Player _player = new Player(_interface, Parent, _skins[_selectedSkinNum], _weenieMode);

            //Adds the players sprite and adds the player to the scene
            Parent.AddChild(_player);

            //Places the player
            _player.X = 100;
            _player.Y = 500;

            level.StartUp();

            Collectable collect = new Collectable(100, 400, "shootSpeed", "GFX/Hunter.png");
            Parent.AddChild(collect);

            //The menu deletes itself after starting the game to not hog space.
            Parent.RemoveChild(this);
        }

    }
}
