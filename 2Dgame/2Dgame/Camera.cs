using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2Dgame
{
    class Camera
    {
        public Matrix transform;
        Viewport view;

        public Camera(Viewport newview)
        {
            view = newview;
        }

        public void Update(GameTime gametime, Vector2 playerPosition)
        {
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-(playerPosition.X - 500), 0, 0));
        }
    }
}
