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
    public static class Control
    {
        public static KeyboardState CurKS;
        public static KeyboardState LastKS;

        public static MouseState CurMS;
        public static MouseState LastMS;

        public static void Update()
        {
            LastKS = CurKS;
            LastMS = CurMS;

            CurKS = Keyboard.GetState();
            CurMS = Mouse.GetState();
        }

        public static bool WasKeyJustPressed(Keys K) { return LastKS.IsKeyUp(K) && CurKS.IsKeyDown(K); }
    }
}
