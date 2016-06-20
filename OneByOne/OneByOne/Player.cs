﻿using System;
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
    public enum Direction { Up, Left, Right, Down }
    public class Player : Entity
    {
        Direction FacingDir;
        bool Moving;
        int Speed;
        bool IsCapturing;

        public Player(Rectangle Rect)
            : base(Rect, Imports.White, Color.White)
        {
            FacingDir = Direction.Right;
            Speed = 6;
        }

        public void UpdateState()
        {
            if (Control.CurKS.IsKeyDown(Keys.A))
                FacingDir = Direction.Left;

            if (Control.CurKS.IsKeyDown(Keys.S))
                FacingDir = Direction.Down;

            if (Control.CurKS.IsKeyDown(Keys.W))
                FacingDir = Direction.Up;

            if (Control.CurKS.IsKeyDown(Keys.D))
                FacingDir = Direction.Right;

            if (Control.CurKS.IsKeyDown(Keys.Left))
                FacingDir = Direction.Left;

            if (Control.CurKS.IsKeyDown(Keys.Down))
                FacingDir = Direction.Down;

            if (Control.CurKS.IsKeyDown(Keys.Up))
                FacingDir = Direction.Up;

            if (Control.CurKS.IsKeyDown(Keys.Right))
                FacingDir = Direction.Right;

            if (Control.CurKS.IsKeyDown(Keys.D) || Control.CurKS.IsKeyDown(Keys.W) ||
                Control.CurKS.IsKeyDown(Keys.S) || Control.CurKS.IsKeyDown(Keys.A) ||
                Control.CurKS.IsKeyDown(Keys.Right) || Control.CurKS.IsKeyDown(Keys.Up) ||
                Control.CurKS.IsKeyDown(Keys.Down) || Control.CurKS.IsKeyDown(Keys.Left))
                Moving = true;
            else
                Moving = false;
        }
        public void SnapToGrid()
        {
            if (FacingDir == Direction.Right || FacingDir == Direction.Left || !Moving && !IsCapturing)
            {
                if (Rect.Y % LevelManager.BlockSize != 0)
                {
                    if (Rect.Y % LevelManager.BlockSize > LevelManager.BlockSize / 2)
                        Rect.Y += LevelManager.BlockSize - Rect.Y % LevelManager.BlockSize;
                    else
                        Rect.Y -= Rect.Y % LevelManager.BlockSize;
                }
            }
            if (FacingDir == Direction.Down || FacingDir == Direction.Up || !Moving && !IsCapturing)
            {
                if (Rect.X % LevelManager.BlockSize != 0)
                {
                    if (Rect.X % LevelManager.BlockSize > LevelManager.BlockSize / 2)
                        Rect.X += LevelManager.BlockSize - Rect.X % LevelManager.BlockSize;
                    else
                        Rect.X -= Rect.X % LevelManager.BlockSize;
                }
            }
        }
        public void MovePlayer()
        {
            switch (FacingDir)
            {
                case Direction.Up:
                    Rect.Y -= Speed;
                    break;

                case Direction.Down:
                    Rect.Y += Speed;
                    break;

                case Direction.Left:
                    Rect.X -= Speed;
                    break;

                case Direction.Right:
                    Rect.X += Speed;
                    break;
            }

            if (Rect.X < 0)
                Rect.X = 0;

            if (Rect.Y < 0)
                Rect.Y = 0;

            if (Rect.X > (int)Values.WindowSize.X - LevelManager.BlockSize)
                Rect.X = (int)Values.WindowSize.X - LevelManager.BlockSize;

            if (Rect.Y > (int)Values.WindowSize.Y - LevelManager.BlockSize)
                Rect.Y = (int)Values.WindowSize.Y - LevelManager.BlockSize;
        }
        public Block StandingOn() 
        {
            return LevelManager.Field[(Rect.X + Rect.Width/2)/ LevelManager.BlockSize, (Rect.Y  + Rect.Height/2)/ LevelManager.BlockSize];
        }
        public Block Next()
        {
            switch (FacingDir)
            {
                case Direction.Up:
                    return LevelManager.Field[(Rect.X + Rect.Width / 2) / LevelManager.BlockSize,
                        (Rect.Y + Rect.Height / 2) / LevelManager.BlockSize - 1];
                case Direction.Down:
                    return LevelManager.Field[(Rect.X + Rect.Width / 2) / LevelManager.BlockSize,
                        (Rect.Y + Rect.Height / 2) / LevelManager.BlockSize + 1];
                case Direction.Right:
                    return LevelManager.Field[(Rect.X + Rect.Width / 2) / LevelManager.BlockSize + 1,
                        (Rect.Y + Rect.Height / 2) / LevelManager.BlockSize];
                case Direction.Left:
                    return LevelManager.Field[(Rect.X + Rect.Width / 2) / LevelManager.BlockSize - 1,
                        (Rect.Y + Rect.Height / 2) / LevelManager.BlockSize];
            }
            return null;
        }
        public void OnDeath()
        {
            LevelManager.Load();
        }
        public override void Update()
        {
            UpdateState();
            if (Moving || IsCapturing)
                MovePlayer();
            SnapToGrid();
            if (StandingOn().State == BlockState.Empty)
            {
                IsCapturing = true;
                StandingOn().State = BlockState.Selected;
            }
            if (StandingOn().State == BlockState.Filled && IsCapturing ) {
                IsCapturing = false;
                LevelManager.TurnAllSelectedBlocksFilled();
            }
            if (IsCapturing && Next().State == BlockState.Selected)
                OnDeath();

        }
        public override void Draw(SpriteBatch SB)
        {
            Imports.DrawCircle(new Vector2(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2), 
                Rect.Width / 2, Col, SB);
        }
        
    }
}
