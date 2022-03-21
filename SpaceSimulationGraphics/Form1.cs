using SpaceSim;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalPlanetSim
{
    public partial class Form1 : Form
    {
        List<SpaceObject> list = Astronomy.getList();
        List<Moon> moons = Astronomy.getMoonList();
        int days = 0;
        bool visAlle = true;
        Planet planet;
        bool visTekst = true;
        int i = 1;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            BackColor = System.Drawing.Color.Black;
            this.MouseClick += Form1_MouseClick;

            //Timere for planetens rotasjon
            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += T_Tick;
            timer.Start();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
           if (e.Button == MouseButtons.Right)
            visAlle = true;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            days += 1*i;
            Astronomy.CalcAllPlanetPos(list, days);
            Astronomy.CalcMoonsPos(days, moons);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DoubleBuffered = true;

            SolidBrush sunB = new SolidBrush(Color.Yellow);
            Pen planetOrbit = new Pen(Color.White);

            g.DrawString("Day:  " + days.ToString(), DefaultFont, sunB, new System.Drawing.PointF(10, 10));

            if (visAlle)
            {
                foreach (SpaceObject obj in list) //Printer ut alle planeter
                {

                    if (obj is Star) //Printer ut Solen i sin posisjon
                    {
                        g.FillEllipse(new SolidBrush(obj.color), new Rectangle((int)obj.xPos, (int)obj.yPos, (int)obj.objectRadius, (int)obj.objectRadius));
                    }
                    else if (obj is Planet && !(obj is DwarfPlanet) && !(obj is Moon) && !(obj is Asteroid)) // Printer ut planeter 
                    {
                        g.FillEllipse(new SolidBrush(obj.color), new Rectangle(665 + (int)obj.xPos / 10000, 360 + (int)obj.yPos / 10000, (int)obj.objectRadius, (int)obj.objectRadius));
                    }
                }
            }
            else
            {
                g.FillEllipse(new SolidBrush(planet.color), new Rectangle(665, 300, (int)planet.objectRadius, (int)planet.objectRadius)); //printer planet

                int numMoons = 0;

                foreach (Moon moon in moons)
                {
                    if (moon.orbits.name.Equals(planet.name)) //Beregner månen(e) posisjon
                    {
                        numMoons++;
                        g.FillEllipse(new SolidBrush(moon.color), new Rectangle(665 + (int)moon.xPos / 10000, 360 + (int)moon.yPos / 10000, (int)moon.objectRadius, (int)moon.objectRadius));
                    }
                }

                if (visTekst)
                {
                    g.DrawString("Information about planet " + planet.name, DefaultFont, sunB, new System.Drawing.PointF(10, 30));
                    g.DrawString("x-position:  " + planet.xPos / 10000, DefaultFont, sunB, new System.Drawing.PointF(10, 50));
                    g.DrawString("y-position:  " + planet.yPos / 10000, DefaultFont, sunB, new System.Drawing.PointF(10, 70));
                    g.DrawString("Orbital period:  " + planet.orbitPeriod, DefaultFont, sunB, new System.Drawing.PointF(10, 90));
                    g.DrawString("Orbital radius:  " + planet.orbitRadius, DefaultFont, sunB, new System.Drawing.PointF(10, 110));
                    g.DrawString("Number of moons:  " + numMoons, DefaultFont, sunB, new System.Drawing.PointF(10, 130));
                }
            }
        }

        private void vScroller_Scroll (object sender, ScrollEventArgs e)
        {
            i = hScrollBar1.Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (visTekst)
            {
                visTekst = false;
            } else
            {
                visTekst = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedText = this.comboBox1.Text;

            if (selectedText.Equals("Show All"))
            {
                visAlle = true;
            }

            foreach (SpaceObject obj in list)
            {
                if (obj.name.Equals(selectedText))
                {
                    visAlle = false;
                    planet = (Planet) obj;
                }
            }
        }
    }
}
