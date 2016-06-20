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
    public class Entity
    {
        public Rectangle Rect;
        public Texture2D Tex;
        public Color Col;

        public Entity(Rectangle Rect, Texture2D Tex, Color Col)
        {
            this.Rect = Rect; this.Tex = Tex; this.Col = Col;
        }

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch SB)
        {
            SB.Draw(Tex, Rect, Col);
        }
    }
}
