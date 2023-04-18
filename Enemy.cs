using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleJump
{
    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Velocity { get; set; }

        public Enemy(int x, int y, int width, int height, int velocity)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Velocity = velocity;
        }
        public void UpdatePosition()
        {
            Y += Velocity;
        }

        //Check Collisions
        public bool CheckCollision(Rectangle playerBounds)
        {
            Rectangle enemyBounds = new Rectangle(X, Y, Width, Height);
            return enemyBounds.IntersectsWith(playerBounds);
        }

    }
}
