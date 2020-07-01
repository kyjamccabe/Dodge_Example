using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dodge_Example
{
    public partial class FrmDodge : Form
    {
        bool left, right;
        int score, lives;
        string move;

        Graphics g; //declare a graphics object called g                   
        Planet[] planet = new Planet[7];  //declare space for an array of 7 objects called planet 
        Random yspeed = new Random();
        Spaceship spaceship = new Spaceship();

        public FrmDodge()
        {
            InitializeComponent();
            for (int i = 0; i < 7; i++)
            {
                int x = 10 + (i * 75);
                planet[i] = new Planet(x);
            }

        }

        private void PnlGame_Paint(object sender, PaintEventArgs e)
        {
            //get the graphics used to paint on the panel control
            g = e.Graphics;
            for (int i = 0; i < 7; i++)
            {
                //call the Planet class's drawPlanet method to draw the images
                planet[i].DrawPlanet(g);
                // generate a random number from 5 to 20 and put it in rndmspeed
                int rndmspeed = yspeed.Next(5, 20);
                planet[i].y += rndmspeed;

            }
            spaceship.DrawSpaceship(g);
        }

        private void FrmDodge_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyData == Keys.Left) { left = true; }
                if (e.KeyData == Keys.Right) { right = true; }
            }

        }

        private void FrmDodge_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }
        }

        private void FrmDodge_Load(object sender, EventArgs e)
        {
            txtName.Enabled = false;
        }

        private void TmrShip_Tick(object sender, EventArgs e)
        {
            if (right) // if right arrow key pressed
            {
                move = "right";
                spaceship.MoveSpaceship(move);
            }
            if (left) // if left arrow key pressed
            {
                move = "left";
                spaceship.MoveSpaceship(move);

            }
        }
            private void TmrPlanet_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                planet[i].MovePlanet();
                //if a planet reaches the bottom of the Game Area reposition it at the top
                if (planet[i].y >= PnlGame.Height)
                {
                    score += 1;//update the score
                    lblScore.Text = score.ToString();// display score
                    planet[i].y = 30;
                }
            }
            PnlGame.Invalidate(); //makes the paint event fire to redraw the panel
        }
    }
}
