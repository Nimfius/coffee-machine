using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coffee_machine
{
    public partial class Form1 : Form
    {
        List<Button> buttons = new List<Button>();

        int y = 140;
        int y2 = 10;

        public Form1()
        {
            InitializeComponent();
            label4.Text = "Please, select a drink!";
            buttons.Add(button1);
            buttons.Add(button3);
            buttons.Add(button4);
            buttons.Add(button7);
            formGraphics= pictureBox1.CreateGraphics(); 
            water= pictureBox1.CreateGraphics(); 
            milk= pictureBox1.CreateGraphics(); 
            milk_foam= pictureBox1.CreateGraphics(); 
        }

        private void disable_buttons()
        {
            buttons.ForEach(delegate (Button b) {
                b.Enabled = false;
        });
        }

        private void enable_buttons()
        {
            buttons.ForEach(delegate (Button b) {
                b.Enabled = true;
            });
        }

        private bool check_quantity()
        {
            if (progressBar1.Value == 0)
            {
                disable_buttons();
                label4.Text ="Please, add coffee!";
                return false;
            }
            if (progressBar3.Value == 0)
            {
                disable_buttons();
                label4.Text = "Please, add milk!";
                return false;
            }
            if (progressBar2.Value == 0)
            {
                disable_buttons();
                label4.Text = "Please, add water!";
                return false;
            }
            if (progressBar2.Value < 3)
            {
                button7.Enabled = false;
            }
            if (progressBar3.Value < 3)
            {
                button3.Enabled = false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1, "Black coffee");
            toolTip2.SetToolTip(button7, "Black coffee with water");
            toolTip3.SetToolTip(button3, "Black coffee with milk and milk foam");
            toolTip4.SetToolTip(button4, "Black coffee with milk");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 20;
            progressBar2.Value = 20;
            progressBar3.Value = 20;
            enable_buttons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_quantity())
            {
                progressBar1.Value--;
                progressBar2.Value--;
                check_quantity();
                disable_buttons();
                draw_cup();
                timer1.Enabled = true;
            }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (check_quantity())
            {
                progressBar1.Value--;
                progressBar2.Value= progressBar2.Value-3;
                check_quantity();
                disable_buttons();
                draw_cup();
                timer2.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check_quantity())
            {
                progressBar1.Value--;
                progressBar3.Value= progressBar3.Value - 3;
                check_quantity();
                disable_buttons();
                draw_cup();
                timer3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (check_quantity())
            {
                progressBar1.Value--;
                progressBar2.Value--;
                progressBar3.Value--;
                check_quantity();
                disable_buttons();
                draw_cup();
                timer4.Enabled = true;
            }
        }

        private void draw_cup()
        {
            label4.Text = "Please, wait - your drink is preparing";
            Graphics cup = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Black, 3);
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 150);
            cup.DrawLine(p, p1, p2);
            p1 = new Point(10, 150);
            p2 = new Point(140, 150);
            cup.DrawLine(p, p1, p2);
            p1 = new Point(140, 150);
            p2 = new Point(140, 10);
            cup.DrawLine(p, p1, p2);
            cup.DrawArc(new Pen(new SolidBrush(Color.Black), 10),110,60,60,50, 270, 180);
            cup.Dispose();
            p.Dispose();
        }

        private void draw_coffee()
        {
            formGraphics.FillRectangle(Brush, new Rectangle(12, y, 127, y2));
            y -= 10;
            y2 += 10;
            if (y == 90)
                y2 = 10;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (is_cup_ready)
            {
                pictureBox1.Image = null;
                is_cup_ready = false;
                label4.Text = "please, select a drink!";
                enable_buttons();
            }
        }
        
        SolidBrush Brush = new SolidBrush(Color.Brown);
        SolidBrush Brush_water = new SolidBrush(Color.Blue);
        SolidBrush Brush_milk = new SolidBrush(Color.White);
        SolidBrush Brush_milk_foam = new SolidBrush(Color.WhiteSmoke);
        Graphics formGraphics, water, milk, milk_foam;

        private void cup_is_ready()
        {
            y = 140;
            y2 = 10;
            is_cup_ready = true;
            label4.Text = "Please, take your coffee!";
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (y < 20)
            {
                timer3.Enabled = false;
                cup_is_ready();
            }
            else
            {
                if (y <= 40)
                {
                    water.FillRectangle(Brush_milk_foam, new Rectangle(12, y, 127, y2));
                    y -= 10;
                    y2 += 10;
                }
                else
                {
                    if (y <= 90)
                    {
                        water.FillRectangle(Brush_milk, new Rectangle(12, y, 127, y2));
                        y -= 10;
                        y2 += 10;
                        if (y <= 40) y2 = 10;
                    }
                    else
                    {
                        draw_coffee();
                    }
                }

                
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (y <= 90)
            {
                if (y < 70)
                {
                    timer4.Enabled = false;
                    cup_is_ready();
                }
                else
                {
                    water.FillRectangle(Brush_milk_foam, new Rectangle(12, y, 127, y2));
                    y -= 10;
                    y2 += 10;
                }

            }
            else
            {
                draw_coffee();
            }
        }

        bool is_cup_ready = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (y < 100)
            {
                timer1.Enabled = false;
                cup_is_ready();
            }
            else
            {
                formGraphics.FillRectangle(Brush, new Rectangle(12, y, 127, y2));
                y -= 10;
                y2 += 10;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (y <= 90)
            {
                if (y < 20)
                {
                    timer2.Enabled = false;
                    cup_is_ready();
                }
                else
                {
                    water.FillRectangle(Brush_water, new Rectangle(12, y, 127, y2));
                    y -= 10;
                    y2 += 10;
                }
                
            }
            else
            {
                draw_coffee();
            }
        }
    }
}
