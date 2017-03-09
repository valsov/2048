using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048
{
    public class Grid
    {
        public Tile[,] gameGrid { get; set; }


        public Grid()
        {
            InitGrid();
        }


        public void InitGrid()
        {
            gameGrid = new Tile[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gameGrid[i, j] = new Tile();
                }
            }
            
            AssignValueToEmptyTile();       //the 2 first tiles of the game
            AssignValueToEmptyTile();
        }


        public void AssignValueToEmptyTile()
        {
            var nullTiles = GetEmptyTiles();

            if (nullTiles.Count != 0)
            {
                var r = new Random();
                var randomTileIndex = r.Next(0, nullTiles.Count - 1);

                nullTiles[randomTileIndex].Value = Tile.minValue;
                nullTiles[randomTileIndex].changed = false;         //we don't want to be notified that the tile value changed
            }
        }


        List<Tile> GetEmptyTiles()
        {
            return gameGrid.Cast<Tile>().Where(tile => tile.Value == 0).ToList();
        }


        public bool IsLocked()
        {
            if (GetEmptyTiles().Count == 0)     //if there are no empty tiles in the grid
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        try
                        {
                            if (gameGrid[i, j].Value == gameGrid[i - 1, j].Value)
                            {
                                return false;
                            }
                        }
                        catch (IndexOutOfRangeException) {}
                        try
                        {
                            if (gameGrid[i, j].Value == gameGrid[i + 1, j].Value)
                            {
                                return false;
                            }
                        }
                        catch (IndexOutOfRangeException) {}
                        try
                        {
                            if(gameGrid[i, j].Value == gameGrid[i, j - 1].Value)
                            {
                                return false;
                            }
                        }
                        catch (IndexOutOfRangeException) {}
                        try
                        {
                            if (gameGrid[i, j].Value == gameGrid[i, j + 1].Value)
                            {
                                return false;
                            }
                        }
                        catch (IndexOutOfRangeException) {}
                    }
                }

                return true;
            }

            return false;
        }


        public bool Moved()
        {
            var moved = false;

            foreach(var tile in gameGrid)
            {
                if (tile.changed)
                {
                    moved = true;
                }
            }
            
            ResetTilesChangeState();

            return moved;
        }


        void ResetTilesChangeState()
        {
            foreach (var tile in gameGrid)
            {
                tile.changed = false;
            }
        }
    }
}
