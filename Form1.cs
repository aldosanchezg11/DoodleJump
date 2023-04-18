using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DoodleJump
{
    public partial class Form1 : Form
    {
        int jump = 0;  // jump height
        int yVel = 0;  // velocity
        int gravity = 1; // gravity constant
        int score = 0; // score counter
        Random rnd = new Random(); // random number generator
        List<Rectangle> platforms = new List<Rectangle>(); // list of platforms
        private List<Enemy> enemies = new List<Enemy>(); // New list for enemies
        private List<Bullet> bullets = new List<Bullet>(); // New list for bullets

        int pictureBoxHeight = 0;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void GameLoop(object sender, EventArgs e)
        {
            int yPos = pictureBoxHeight - 50;
            while (true)
            {
                // Clear the player
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.FillRectangle(Brushes.White, pictureBox1.Width / 2 - 25, yPos, 50, 50);
                }
                // Handle user input
                if (jump == 0 && ((KeyEventArgs)e).KeyCode == Keys.Space)
                {
                    jump = 20;
                }
                // Apply gravity and update player position
                yVel += gravity;
                yPos += yVel;
                if (yPos > pictureBoxHeight)
                {
                    MessageBox.Show("Game over!");
                    break;
                }
                // Check if the player has landed on a platform
                foreach (Rectangle platform in platforms)
                {
                    if (yPos + 50 == platform.Y && pictureBox1.Bounds.Width / 2 >= platform.X && pictureBox1.Bounds.Width / 2 <= platform.X + platform.Width)
                    {
                        yVel = 0;
                        score++;
                    }
                }
                // Update jump height
                if (jump > 0)
                {
                    yPos -= jump;
                    jump--;
                }

                // Update enemy positions
                foreach (Enemy enemy in enemies)
                {
                    enemy.UpdatePosition();
                }

                // Check for bullet collisions with enemies
                foreach (Bullet bullet in bullets)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        if (bullet.CheckCollision(enemy))
                        {
                            // Remove the enemy and bullet from their respective lists
                            enemies.Remove(enemy);
                            bullets.Remove(bullet);
                            break;
                        }
                    }
                }

                    // Draw the platforms
                    //DrawPlatforms();

                    // Draw the player
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.FillEllipse(Brushes.Blue, pictureBox1.Width / 2 - 25, yPos, 50, 50);
                }

                // Display the score
                toolStripStatusLabel1.Text = "Score: " + score;

                // Sleep for a short amount of time to slow down the loop
                System.Threading.Thread.Sleep(50);
            }
        }

        private void GeneratePlatforms()
        {
            platforms.Clear();

            // Create a new platform at the top of the screen
            Rectangle platform = new Rectangle(0, 0, pictureBox1.Width / 4, 20);
            platforms.Add(platform);

            // Create additional platforms below the first platform
            for (int i = 1; i < 10; i++)
            {
                int x = rnd.Next(0, pictureBox1.Width - platform.Width);
                int y = rnd.Next(i * 100, i * 100 + 100);
                platform = new Rectangle(x, y, pictureBox1.Width / 4, 20);
                platforms.Add(platform);
                DrawPlatforms();
                //using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                //{
                //    g.FillRectangle(Brushes.Black, platformX, platformY, platformWidth, platformHeight);
                //}
            }
            //for (int i = 0; i < 10; i++)
            //{
            //    int platformWidth = rnd.Next(50, 150);
            //    int platformHeight = 20;
            //    int platformX = rnd.Next(0, pictureBox1.Width - platformWidth);
            //    int platformY = rnd.Next(0, pictureBoxHeight - platformHeight - 50);

            //    platforms.Add(new Point(platformX, platformY));

            // Draw the platform
        }
        private void DrawPlatforms()
        {
            foreach (Rectangle platform in platforms)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.FillRectangle(Brushes.Blue, platform);
                } 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxHeight = pictureBox1.Height;
            GeneratePlatforms();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameLoop(sender, e);
        }
    }
}