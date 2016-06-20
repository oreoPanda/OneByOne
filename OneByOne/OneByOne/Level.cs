using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OneByOne
{
    public static class LevelManager
    {
        public const int BlockSize = 25;
        public static Block[,] Field = new Block[(int)Values.WindowSize.X / BlockSize, (int)Values.WindowSize.Y / BlockSize];
        public static Player ThisPlayer;
        public static Enemy blob;

        public static void Load()
        {
            ThisPlayer = new Player(new Rectangle(0, 0, BlockSize, BlockSize));
            blob = new Enemy(new Rectangle(100, 100, BlockSize, BlockSize));

            for (int x =0; x < Field.GetLength(0);x++){
                for (int y = 0; y < Field.GetLength(1); y++ )
                {
                    Field[x, y] = new Block(new Rectangle(x*BlockSize, y*BlockSize, BlockSize, BlockSize));

                    if (x == 0 || y == 0 || x == Field.GetLength(0) - 1 || y == Field.GetLength(1) - 1)
                        Field[x, y].State = BlockState.Filled;
                }
            
            }

        }

        public static void Update()
        {
            ThisPlayer.Update();
            blob.Update();
        }

        public static void Draw(SpriteBatch SB)
        {
            for (int x = 0; x < Field.GetLength(0); x++)
            {
                for (int y = 0; y < Field.GetLength(1); y++)
                {
                    Field[x, y].Draw(SB); 
                }

            }
            ThisPlayer.Draw(SB);
            blob.Draw(SB);
        }

        public static void TurnAllSelectedBlocksFilled() 
        {
            for (int x = 0; x < Field.GetLength(0); x++)
            {
                for (int y = 0; y < Field.GetLength(1); y++)
                {
                    if (Field[x,y].State == BlockState.Selected)
                        Field[x,y].State = BlockState.Filled; 
                }

            }
        }
    }
}
