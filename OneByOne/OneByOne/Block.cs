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
    public enum BlockState { Empty, Selected, Filled }
    public class Block : Entity
    {
        public BlockState State;

        public Block(Rectangle Rect)
            : base(Rect, Imports.Block, Color.White)
        {
            State = BlockState.Empty;
        }

        public override void Draw(SpriteBatch SB)
        {
            switch (State)
            { 
                case BlockState.Filled:
                    SB.Draw(Imports.Block, Rect, Color.White);
                    break;

                case BlockState.Selected:
                    SB.Draw(Imports.Block, Rect, Color.Gray);
                    break;
            }
        }

    }
}
