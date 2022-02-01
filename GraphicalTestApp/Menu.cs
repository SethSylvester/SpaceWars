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
        //The cursor
        private Cursor _cursor;

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

        //Displays the menus text
        private string[] _menuText = new string[13];

        public Menu()
        {
            //The four skins you're allowed to pick from
            _skins[0] = "GFX/StarShip1.png";
            _skins[1] = "GFX/StarShip2.png";
            _skins[2] = "GFX/StarShip3.png";
            _skins[3] = "GFX/StarShip4.png";

            //loads in the default skin
            _spriteDisplay = new Sprite(_skins[0]);

            //Spawns the cursor
            _cursor = new Cursor();
            _cursor.maxPos = 5;
            _cursor.X = 425;
            _cursor.Y = 260;
            AddChild(_cursor);

            //OnUpdate that draws the menu and checks to see if you selected anything
            TextInitalizer();
            OnUpdate += Selected;
            OnDraw += MenuDraw;
        }

        private void TextInitalizer()
        {
            //Title
            _menuText[0] = "SpaceWars";
            //Main Menu
            _menuText[1] = "Start";
            _menuText[2] = "Select Stage";
            _menuText[3] = "Customize";
            _menuText[4] = "Infinite HP";
            //Stage Selecter
            _menuText[5] = "Select Stage";
            _menuText[6] = "Next Stage";
            _menuText[7] = "Previous Stage";

            //Customization Menu
            _menuText[8] = "Select Skin";
            _menuText[9] = "Next Skin";
            _menuText[10] = "Previous Skin";

            //Exit & Back
            _menuText[11] = "Back";
            _menuText[12] = "Exit";
        }

        private void Selected(float deltaTime)
        {
            //###MAIN MENU FUNCTIONS###
            if (_mainMenu)
            {
                //Starts the game
                if (_cursor.selected && _cursor.pos == 1)
                {
                    StartGame();
                }
                //Brings up the stage selector
                else if (_cursor.selected && _cursor.pos == 2)
                {
                    _mainMenu = false;
                    _stageSelect = true;
                    _cursor.maxPos = 3;
                    _cursor.pos = 1;
                    _cursor.Y = 260;
                }
                //Brings up the skin selector
                else if (_cursor.selected && _cursor.pos == 3)
                {
                    AddChild(_spriteDisplay);
                    _spriteDisplay.X = 500;
                    _spriteDisplay.Y = 200;

                    _mainMenu = false;
                    _customizeMenu = true;
                    _cursor.maxPos = 3;
                    _cursor.pos = 1;
                    _cursor.Y = 260;
                }
                //Turns on Weenie mode
                else if (_cursor.selected && _cursor.pos == 4 &&
                    !_weenieMode)
                {
                    _weenieMode = true;
                }
                //Turns off weenie mode
                else if (_cursor.selected && _cursor.pos == 4 &&
                    _weenieMode)
                {
                    _weenieMode = false;
                }
                //Closes the game
                else if (_cursor.selected && _cursor.pos == 5)
                {
                    Game.Exit = true;
                }
            }

            //###STAGE SELECTOR MENU###
            else if (_stageSelect)
            {
                //Ensures the starting stage cannot be over six
                if (_cursor.selected && _cursor.pos == 1 &&
                    _startingStage < 6)
                {
                    _startingStage++;
                }

                //Ensures the starting stage cannot be below 1
                else if (_cursor.selected && _cursor.pos == 2 &&
                    _startingStage > 1)
                {
                    _startingStage--;
                }

                //Exits out of the stage selector
                else if (_cursor.selected && _cursor.pos == 3)
                {
                    _stageSelect = false;
                    _mainMenu = true;
                    _cursor.maxPos = 5;
                    _cursor.pos = 1;
                    _cursor.Y = 260;
                }
            }

            //###SKIN SELECTION MENU###
            else if (_customizeMenu)
            {
            //Selects the next skin
            if (_cursor.selected && _cursor.pos == 1 &&
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
                else if (_cursor.selected && _cursor.pos == 2 &&
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
                else if (_cursor.selected && _cursor.pos == 3)
                {
                    RemoveChild(_spriteDisplay);

                    _customizeMenu = false;
                    _mainMenu = true;
                    _cursor.maxPos = 5;
                    _cursor.pos = 1;
                    _cursor.Y = 260;
                }
            }
            //Tells the cursor to stop selecting something after a press
            _cursor.selected = false;
        }

        //Draws the menu and writes out the text
        private void MenuDraw()
        {
            //Gets the current text
            _choice = _cursor.pos;
            //Text, PosX, PosY, Size, Color
            //Draws the main menus options
            if (_mainMenu)
            {
                RL.DrawText(Convert.ToString(_menuText[0]), 275, 150, 75, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString(_menuText[1]), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[1]), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString(_menuText[2]), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[2]), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString(_menuText[3]), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[3]), 450, 315, 25, Color.WHITE);
                }
                if (_weenieMode)
                {
                    RL.DrawText(Convert.ToString(_menuText[4]), 450, 345, 25, Color.PINK);
                }
                else if (_choice == 4)
                {
                    RL.DrawText(Convert.ToString(_menuText[4]), 450, 345, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[4]), 450, 345, 25, Color.WHITE);
                }
                if (_choice == 5)
                {
                    RL.DrawText(Convert.ToString(_menuText[12]), 450, 375, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[12]), 450, 375, 25, Color.WHITE);
                }
            }

            //The menu for selecting a stage
            else if (_stageSelect)
            {
                RL.DrawText(Convert.ToString(_menuText[5]), 450, 150, 25, Color.WHITE);
                RL.DrawText(Convert.ToString(_startingStage), 520, 190, 50, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString(_menuText[6]), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[6]), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString(_menuText[7]), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[7]), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString(_menuText[11]), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[11]), 450, 315, 25, Color.WHITE);
                }
            }

            //The menu for the skin customization
            else if (_customizeMenu)
            {
                RL.DrawText(Convert.ToString(_menuText[8]), 450, 150, 25, Color.WHITE);
                if (_choice == 1)
                {
                    RL.DrawText(Convert.ToString(_menuText[9]), 450, 250, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[9]), 450, 250, 25, Color.WHITE);
                }
                if (_choice == 2)
                {
                    RL.DrawText(Convert.ToString(_menuText[10]), 450, 283, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[10]), 450, 283, 25, Color.WHITE);
                }
                if (_choice == 3)
                {
                    RL.DrawText(Convert.ToString(_menuText[11]), 450, 315, 25, Color.BLUE);
                }
                else
                {
                    RL.DrawText(Convert.ToString(_menuText[11]), 450, 315, 25, Color.WHITE);
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
            _player.X = 390f;
            _player.Y = 500f;

            level.StartUp();

            Collectable collect = new Collectable(100, 400, "shootSpeed", "GFX/Hunter.png");
            Parent.AddChild(collect);

            //The menu deletes itself after starting the game to not hog space.
            Parent.RemoveChild(this);
        }

    }
}
