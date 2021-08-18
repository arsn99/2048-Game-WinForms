using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_Game_WinForms
{
    public partial class Form1 : Form
    {
        private const int size = 4;
        private int[,] map = new int[size, size];
        private PictureBox[,] boxes = new PictureBox[size, size];
        private Label[,] scoreBoxes = new Label[size, size];
        private int score = 0;
        Random random = new Random();

        Font font = new Font(new FontFamily("Microsoft Sans Serif"), 20);
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyBoardEvent);

            CreateBoxes();
            CreateMap();
            
        }
        private PictureBox CreateBox()
        {
            int x = random.Next(0,4);
            int y = random.Next(0,4);
            while (map[y,x]!=0)
            {
                x = random.Next(0, 4);
                y = random.Next(0, 4);
            }
            PictureBox picture = new PictureBox()
            {
                Location = new Point(12+70*x, 73+70*y),
                BackColor = Color.DarkOrange,
                Size = new Size(64, 64),
            };
            CreateLabels(y, x);
            picture.Controls.Add(scoreBoxes[y,x]);
            map[y, x] = 1;
            boxes[y, x] = picture;
            this.Controls.Add(picture);
            picture.BringToFront();
            
            return picture;
        }
        private void CreateLabels(int i, int j)
        {
            scoreBoxes[i,j] = new Label();
            scoreBoxes[i, j].Font = font;
            scoreBoxes[i, j].TextAlign = ContentAlignment.MiddleCenter;
            scoreBoxes[i, j].Text = "2";
            scoreBoxes[i, j].Size = new Size(64, 64);
            scoreBoxes[i, j].ForeColor = Color.Bisque;
        }
        private void CreateBoxes()
        {
            CreateBox();
        }
        private void CreateMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox box = new PictureBox()
                    {
                        Location = new Point(12+70*j, 73+70*i),
                        BackColor = Color.Gray,
                        Size = new Size(64, 64)
                    };
                    this.Controls.Add(box);
                }
            }
        }
     
        private void KeyBoardEvent(object sender, KeyEventArgs key)
        {
            
            switch (key.KeyCode.ToString())
            {
                case "Right":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 2; l >= 0; l--)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l + 1; j < 4; j++)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        boxes[k, j] = boxes[k, j - 1];
                                        boxes[k, j - 1] = null;
                                        scoreBoxes[k, j] = scoreBoxes[k, j - 1];
                                        scoreBoxes[k, j - 1] = null;
                                        boxes[k, j].Location = new Point(boxes[k, j].Location.X + 70, boxes[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(scoreBoxes[k, j].Text);
                                        int b = int.Parse(scoreBoxes[k, j-1].Text);
                                        if (a==b)
                                        {
                                            score += a + b;
                                            scoreBoxes[k, j].Text = (a + b).ToString();
                                            map[k, j] = 1;
                                            map[k, j-1] = 0;
                                            this.Controls.Remove(boxes[k,j-1]);
                                            boxes[k, j - 1] = null;
                                            scoreBoxes[k, j - 1] = null;
                                            scoreBoxes[k, j].BackColor = GetColor(a + b);

                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                    break;
                case "Left":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l - 1; j >= 0; j--)
                                {
                                    if (map[k, j] == 0)
                                    {

                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        boxes[k, j] = boxes[k, j + 1];
                                        scoreBoxes[k, j] = scoreBoxes[k, j + 1];
                                        scoreBoxes[k, j + 1] = null;
                                        boxes[k, j + 1] = null;
                                        boxes[k, j].Location = new Point(boxes[k, j].Location.X - 70, boxes[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(scoreBoxes[k, j].Text);
                                        int b = int.Parse(scoreBoxes[k, j + 1].Text);
                                        if (a == b)
                                        {
                                            score += a + b;
                                            scoreBoxes[k, j].Text = (a + b).ToString();
                                            map[k, j] = 1;
                                            map[k, j + 1] = 0;
                                            this.Controls.Remove(boxes[k, j + 1]);
                                            boxes[k, j + 1] = null;
                                            scoreBoxes[k, j + 1] = null;
                                            scoreBoxes[k, j].BackColor = GetColor(a + b);
                                            
                                        }
                                    }

                                }
                            }
                        }
                    }
                    break;
                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 4; j++)
                                {
                                    if (map[j, l] == 0)
                                    {

                                        map[j-1, l] = 0;
                                        map[j, l] = 1;
                                        boxes[j, l] = boxes[j - 1, l];
                                        scoreBoxes[j, l] = scoreBoxes[j - 1, l];
                                        scoreBoxes[j - 1, l] = null;
                                        boxes[j - 1, l] = null;
                                        boxes[j, l].Location = new Point(boxes[j, l].Location.X, boxes[j, l].Location.Y + 70);
                                    }
                                    else
                                    {
                                        int a = int.Parse(scoreBoxes[j, l].Text);
                                        int b = int.Parse(scoreBoxes[j-1, l].Text);
                                        if (a == b)
                                        {
                                            score += a + b;
                                            scoreBoxes[j, l].Text = (a + b).ToString();
                                            map[j, l] = 1;
                                            map[j-1, l] = 0;
                                            this.Controls.Remove(boxes[j-1, l]);
                                            boxes[j-1, l] = null;
                                            scoreBoxes[j-1, l] = null;
                                            scoreBoxes[j, l].BackColor = GetColor(a + b);
                                        }
                                    }

                                }
                            }
                        }
                    }
                    break;

                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >=0 ; j--)
                                {
                                    if (map[j, l] == 0)
                                    {

                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        boxes[j, l] = boxes[j + 1, l];
                                        scoreBoxes[j, l] = scoreBoxes[j + 1, l];
                                        scoreBoxes[j + 1, l] = null;
                                        boxes[j + 1, l] = null;
                                        boxes[j, l].Location = new Point(boxes[j, l].Location.X, boxes[j, l].Location.Y - 70);
                                    }
                                    else
                                    {
                                        int a = int.Parse(scoreBoxes[j, l].Text);
                                        int b = int.Parse(scoreBoxes[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            score += a + b;
                                            scoreBoxes[j, l].Text = (a + b).ToString();
                                            map[j, l] = 1;
                                            map[j + 1, l] = 0;
                                            this.Controls.Remove(boxes[j + 1, l]);
                                            boxes[j + 1, l] = null;
                                            scoreBoxes[j + 1, l] = null;
                                            scoreBoxes[j, l].BackColor = GetColor(a + b);
                                        }
                                    }

                                }
                            }
                        }
                    }
                    break;


            }
            scoreLabel.Text = $"Score: {score}";
            CreateBox();
        }
        private Color GetColor(int sum)
        {
            
            if (sum % 1024 == 0) return Color.Pink;
            else if (sum % 512 == 0) return Color.Red;
            else if (sum % 256 == 0) return Color.DarkViolet;
            else if (sum % 128 == 0) return Color.Blue;
            else if (sum % 64 == 0) return Color.Brown;
            else if (sum % 32 == 0) return Color.Coral;
            else if (sum % 16 == 0) return Color.Cyan;
            else if (sum % 8 == 0) return Color.Maroon;
            else return Color.Green;

        }
    }
}
