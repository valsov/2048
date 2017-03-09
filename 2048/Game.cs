using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2048
{
    public class Game
    {
        public Grid grid { get; }

        public static Dictionary<int, string> images { get; set; }

        public int score { get; set; }


        public Game()
        {
            #region Dictionnary of images
            images = new Dictionary<int, string>
            {
                {0, "../Ressources_sfw_sfw/0.png"},
                {2, "../Ressources_sfw/2.png"},
                {4, "../Ressources_sfw/4.png"},
                {8, "../Ressources_sfw/8.png"},
                {16, "../Ressources_sfw/16.png"},
                {32, "../Ressources_sfw/32.png"},
                {64, "../Ressources_sfw/64.png"},
                {128, "../Ressources_sfw/128.png"},
                {256, "../Ressources_sfw/256.png"},
                {512, "../Ressources_sfw/512.png"},
                {1024, "../Ressources_sfw/1024.png"},
                {2048, "../Ressources_sfw/2048.png"}
            };
            #endregion

            grid = new Grid();
            grid.InitGrid();

            UpdateScore();
        }


        public void Play(char direction)
        {

            Move(direction);

            if (grid.Moved())                   //if the play made at least one tile move
            {
                grid.AssignValueToEmptyTile();  //"add" a tile to the grid

                UpdateScore();
            }                                   //else, we do nothing, the player can move once again
        }
        
        void Move(char dir)
        {
            if (dir == 'U')
            {
                MoveToTop();
            }
            else if (dir == 'D')
            {
                MoveToBottom();
            }
            else if (dir == 'R')
            {
                MoveToRight();
            }
            else
            {
                MoveToLeft();
            }


            foreach (var tile in grid.gameGrid)
            {
                tile.merged = false;    //reset all the merged
            }
        }


        #region Move directions
        void MoveToTop()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)             //foreach tile, starting at the top left corner of the grid and line by line
                {
                    if (grid.gameGrid[i, j].Value != 0) //if the current tile isn't an empty one
                    {
                        var coordI = i - 1;             //starting at the tile on top
                        var coordJ = j;
                        var passed = false;

                        while (coordI >= 0 && !passed)                  //while we aren't looking out of the grid
                        {
                            if (grid.gameGrid[i, j].Value == grid.gameGrid[coordI, coordJ].Value)  //if the tile we are looking at has the same Value as the current tile
                            {
                                if(!grid.gameGrid[coordI, coordJ].merged)         //if the tile didn't merge in this turn
                                {
                                    grid.gameGrid[i, j].Value = 0;                      //empty the origine tile
                                    grid.gameGrid[coordI, coordJ].DoubleValue();        //new tile get Value * 2
                                    grid.gameGrid[coordI, coordJ].merged = true;        //the tile did merge
                                }
                                else
                                {
                                    if (i != coordI + 1)                    //if the current tile isn't just before the tile
                                    {
                                        grid.gameGrid[coordI + 1, coordJ].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                        grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                    }
                                }

                                passed = true;
                            }
                            else if (grid.gameGrid[coordI, coordJ].Value != 0)      //we are looking at a tile with a different Value
                            {
                                if (i != coordI + 1)                    //if the current tile isn't just before the tile
                                {
                                    grid.gameGrid[coordI + 1, coordJ].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                    grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                }
                                passed = true;
                            }
                            else if (coordI == 0)                       //we are looking at the empty top tile
                            {
                                grid.gameGrid[coordI, coordJ].Value = grid.gameGrid[i, j].Value;        //tile get the origine Value 
                                grid.gameGrid[i, j].Value = 0;          //empty the origine tile
                                passed = true;
                            }
                            else
                            {
                                coordI--;       //if we are looking at an empty tile that isn't at the top end side
                            }
                        }
                    }
                }
            }
        }

        void MoveToBottom()
        {
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)             //foreach tile, starting at the bottom left corner of the grid and line by line
                {
                    if (grid.gameGrid[i, j].Value != 0) //if the current tile isn't an empty one
                    {
                        var coordI = i + 1;             //starting at the tile on the bottom
                        var coordJ = j;
                        var passed = false;

                        while (coordI <= 3 && !passed)                  //while we aren't looking out of the grid
                        {
                            if (grid.gameGrid[i, j].Value == grid.gameGrid[coordI, coordJ].Value)  //if the tile we are looking at has the same Value as the current tile
                            {
                                if (!grid.gameGrid[coordI, coordJ].merged)         //if the tile didn't merge in this turn
                                {
                                    grid.gameGrid[i, j].Value = 0;                      //empty the origine tile
                                    grid.gameGrid[coordI, coordJ].DoubleValue();        //new tile get Value * 2
                                    grid.gameGrid[coordI, coordJ].merged = true;        //the tile did merge
                                }
                                else
                                {
                                    if (i != coordI - 1)                    //if the current tile isn't just before the tile
                                    {
                                        grid.gameGrid[coordI - 1, coordJ].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                        grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                    }
                                }

                                passed = true;
                            }
                            else if (grid.gameGrid[coordI, coordJ].Value != 0)      //we are looking at a tile with a different Value
                            {
                                if (i != coordI - 1)                //if the tile isn't just before the tile
                                {
                                    grid.gameGrid[coordI - 1, coordJ].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                    grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                }
                                passed = true;
                            }
                            else if (coordI == 3)                       //we are looking at the empty bottom tile
                            {
                                grid.gameGrid[coordI, coordJ].Value = grid.gameGrid[i, j].Value;        //tile get the origine Value 
                                grid.gameGrid[i, j].Value = 0;          //empty the origine tile
                                passed = true;
                            }
                            else
                            {
                                coordI++;       //if we are looking at an empty tile that isn't at the bottom end side
                            }
                        }
                    }
                }
            }
        }

        void MoveToRight()
        {
            for (int j = 3; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)             //foreach tile, starting at the top right corner of the grid and column by column
                {
                    if (grid.gameGrid[i, j].Value != 0) //if the current tile isn't an empty one
                    {
                        var coordI = i;                 //starting at the tile on Right
                        var coordJ = j + 1;
                        var passed = false;

                        while (coordJ <= 3 && !passed)                  //while we aren't looking out of the grid
                        {
                            if (grid.gameGrid[i, j].Value == grid.gameGrid[coordI, coordJ].Value)  //if the tile we are looking at has the same Value as the current tile
                            {
                                if (!grid.gameGrid[coordI, coordJ].merged)         //if the tile didn't merge in this turn
                                {
                                    grid.gameGrid[i, j].Value = 0;                      //empty the origine tile
                                    grid.gameGrid[coordI, coordJ].DoubleValue();        //new tile get Value * 2
                                    grid.gameGrid[coordI, coordJ].merged = true;        //the tile did merge
                                }
                                else
                                {
                                    if (j != coordJ - 1)                    //if the tile isn't just before the tile
                                    {
                                        grid.gameGrid[coordI, coordJ - 1].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                        grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                    }
                                }

                                passed = true;
                            }
                            else if (grid.gameGrid[coordI, coordJ].Value != 0)      //we are looking at a tile with a different Value
                            {
                                if (j != coordJ - 1)                    //if the tile isn't just before the tile
                                {
                                    grid.gameGrid[coordI, coordJ - 1].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                    grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                }
                                passed = true;
                            }
                            else if (coordJ == 3)                       //we are looking at the empty right tile
                            {
                                grid.gameGrid[coordI, coordJ].Value = grid.gameGrid[i, j].Value;        //tile get the origine Value 
                                grid.gameGrid[i, j].Value = 0;          //empty the origine tile
                                passed = true;
                            }
                            else
                            {
                                coordJ++;       //if we are looking at an empty tile that isn't at the right end side
                            }
                        }
                    }
                }
            }
        }

        void MoveToLeft()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)             //foreach tile, starting at the top left corner of the grid and column by column
                {
                    if (grid.gameGrid[i, j].Value != 0) //if the current tile isn't an empty one
                    {
                        var coordI = i;                 //starting at the tile on top
                        var coordJ = j - 1;
                        var passed = false;

                        while (coordJ >= 0 && !passed)                  //while we aren't looking out of the grid
                        {
                            if (grid.gameGrid[i, j].Value == grid.gameGrid[coordI, coordJ].Value)  //if the tile we are looking at has the same Value as the current tile
                            {
                                if (!grid.gameGrid[coordI, coordJ].merged)         //if the tile didn't merge in this turn
                                {
                                    grid.gameGrid[i, j].Value = 0;                      //empty the origine tile
                                    grid.gameGrid[coordI, coordJ].DoubleValue();        //new tile get Value * 2
                                    grid.gameGrid[coordI, coordJ].merged = true;        //the tile did merge
                                }
                                else
                                {
                                    if (j != coordJ + 1)                    //if the tile isn't just before the tile
                                    {
                                        grid.gameGrid[coordI, coordJ + 1].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                        grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                    }
                                }

                                passed = true;
                            }
                            else if (grid.gameGrid[coordI, coordJ].Value != 0)      //we are looking at a tile with a different Value
                            {
                                if (j != coordJ + 1)                //if the tile isn't just before the tile
                                {
                                    grid.gameGrid[coordI, coordJ + 1].Value = grid.gameGrid[i, j].Value;    //previous tile get the origine Value
                                    grid.gameGrid[i, j].Value = 0;                  //empty the origine tile
                                }
                                passed = true;
                            }
                            else if (coordJ == 0)                       //we are looking at the empty left tile
                            {
                                grid.gameGrid[coordI, coordJ].Value = grid.gameGrid[i, j].Value;        //tile get the origine Value 
                                grid.gameGrid[i, j].Value = 0;          //empty the origine tile
                                passed = true;
                            }
                            else
                            {
                                coordJ--;       //if we are looking at an empty tile that isn't at the left end side
                            }
                        }
                    }
                }
            }
        }
        #endregion


        void UpdateScore()
        {
            var newScore = 0;

            foreach(var tile in grid.gameGrid)
            {
                newScore += tile.Value;
            }

            score = newScore;
        }
    }
}
