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
    public class Enemy : Entity
    {
        Direction movingdir;
        int speed;

        public Enemy(Rectangle Rect)
            : base(Rect, Imports.White, Color.White)
        {
            movingdir = Direction.Up;
            speed = 3;
        }

        public void moveEnemy()
        {
            switch(movingdir)
            {
                case Direction.Up:
                    Rect.Y -= speed;
                    break;
                case Direction.Down:
                    Rect.Y += speed;
                    break;
            }
        }

        /*This function allows to move into the walls one pixel (in one frame), which will not be visible to the user*/
        public Block Next()
        {
            switch (movingdir)
            {
                case Direction.Up:
                    return LevelManager.Field[Rect.X / LevelManager.BlockSize,
                        Rect.Y / LevelManager.BlockSize];
                case Direction.Down:
                    return LevelManager.Field[Rect.X / LevelManager.BlockSize,
                        (Rect.Y+Rect.Height) / LevelManager.BlockSize];
            }
            return null;
        }

        public override void Update()
        {
            if (Next().State == BlockState.Selected)
            {
                LevelManager.ThisPlayer.OnDeath();
            }
            else
            {
                if (movingdir == Direction.Up)
                {
                    if (Next().State == BlockState.Filled)
                    {
                        movingdir = Direction.Down;
                    }
                }
                else
                {
                    if (Next().State == BlockState.Filled)
                    {
                        movingdir = Direction.Up;
                    }
                }
            }

            moveEnemy();
        }
    }
}
