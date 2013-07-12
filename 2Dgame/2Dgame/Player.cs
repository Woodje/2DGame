using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace _2Dgame
{
    public class Player
    {
        Texture2D player;
        public Vector2 position = new Vector2(250, 0);
        Vector2 prevPosition, velocity = Vector2.Zero;
        Color playerColor = Color.White;
        Rectangle playerRect;
        float moveSpeed = 500f, jumpSpeed = 1000f;
        const float gravity = 50f;
        bool jump = false, canYouJump = false, directionLeft = false;
        int y = 0;

        public Player(Texture2D texture2D)
        {
            player = texture2D;
        }

        public void Update(GameTime gameTime, KeyboardState keyState, List<Rectangle> tileRect, ContentManager content)
        {
            if (directionLeft && keyState.IsKeyUp(Keys.Left))
            {
                player = content.Load<Texture2D>("MarioStandLeft");
            }
            else if(!directionLeft && keyState.IsKeyUp(Keys.Right))
            {
                player = content.Load<Texture2D>("MarioStandRight");
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                directionLeft = false;
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                y++;
                if (y <= 8)
                {
                    player = content.Load<Texture2D>("MarioStandRight");
                }
                if (y >= 8)
                {
                    player = content.Load<Texture2D>("MarioRunRight");
                }
                if (y >= 16)
                {
                    y = 0;
                }
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                directionLeft = true;
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                y++;
                if (y <= 8)
                {
                    player = content.Load<Texture2D>("MarioStandLeft");
                }
                if (y >= 8)
                {
                    player = content.Load<Texture2D>("MarioRunLeft");
                }
                if (y >= 16)
                {
                    y = 0;
                }
            }
            else
                velocity.X = 0;

            if (keyState.IsKeyDown(Keys.Up) && jump && canYouJump)
            {
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
                canYouJump = false;
            }

            if (keyState.IsKeyUp(Keys.Up))
            {
                canYouJump = true;
            }

            prevPosition = position;
            
            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;

            position += velocity;

            jump = position.Y >= 658;

            if (jump)
                position.Y = 658;

            playerRect = new Rectangle((int)position.X, (int)position.Y, player.Width, player.Height);

            for (int i = 0; i < tileRect.Count; i++)
            {
                if (playerRect.Intersects(tileRect[i]))
                {
                    if (playerRect.Bottom >= tileRect[i].Top && prevPosition.Y + 38 <= tileRect[i].Y)
                    {
                        velocity = new Vector2(0, 0);
                        jump = true;
                        position.Y = tileRect[i].Y - playerRect.Height;
                    }
                    else if (playerRect.Top <= tileRect[i].Bottom && keyState.IsKeyDown(Keys.Up) && !keyState.IsKeyDown(Keys.Right) && !keyState.IsKeyDown(Keys.Left) || !jump && !keyState.IsKeyDown(Keys.Right) && !keyState.IsKeyDown(Keys.Left))
                    {
                        velocity = new Vector2(0, 0);
                        position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (playerRect.Right >= tileRect[i].Left && keyState.IsKeyDown(Keys.Right))
                    {
                        velocity = new Vector2(0, 0);
                        position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (playerRect.Left <= tileRect[i].Right && keyState.IsKeyDown(Keys.Left))
                    {
                        velocity = new Vector2(0, 0);
                        position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    playerColor = Color.Turquoise;

                }
                else
                {
                    playerColor = Color.White;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player, position, playerColor);
        }
    }
}