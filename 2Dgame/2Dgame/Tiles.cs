using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _2Dgame
{
    public class Tiles
    {
        Texture2D tile;
        Color tileColor = Color.Black;
        Rectangle tileRect;
        List<Rectangle> tilesRectList;
        Vector2 tilePosition = new Vector2(0,0);

        public Tiles(Texture2D texture2D)
        {
            tile = texture2D;
        }

        public List<Rectangle> TilesRectList
        {
            get
            {
                return tilesRectList;
            }
        }

        public void LoadContent(string[] mapString)
        {
            tilesRectList = new List<Rectangle>();
            
            for (int i = 0; i < mapString.Length; i++)
            {
                string[] tempString = mapString[i].Split(',');

                foreach (string symbol in tempString)
                {
                    if (symbol == "X")
                    {
                        tileRect = new Rectangle((int)tilePosition.X, (int)tilePosition.Y, 40, 40);
                        tilesRectList.Add(tileRect);
                    }
                    tilePosition.X += 40;
                }
                tilePosition.X = 0;
                tilePosition.Y += 40;
                tempString = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rectangle tileRectInList in tilesRectList)
            {
                spriteBatch.Draw(tile, tileRectInList, Color.White);
            }
        }
    }
}
