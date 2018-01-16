using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Для музыки
using System.Media;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Lab_One_OpenGL
{
    public partial class Form1 : Form
    {
        GLgraphics glgraphics = new GLgraphics();

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            glgraphics.Setup(glControl1.Width, glControl1.Height);
            Application.Idle += Application_Idle;
            
            int[] texID = new int[150];

            texID[0] = glgraphics.LoadTexture("Texture/logo_ITMM.png");
            glgraphics.texturesIDs.Add(texID[0]);

            texID[1] = glgraphics.LoadTexture("Texture/Sun.jpg");
            glgraphics.texturesIDs.Add(texID[1]);
            texID[2] = glgraphics.LoadTexture("Texture/Меркурий.png");
            glgraphics.texturesIDs.Add(texID[2]);
            texID[3] = glgraphics.LoadTexture("Texture/Венера.png");
            glgraphics.texturesIDs.Add(texID[3]);
            texID[4] = glgraphics.LoadTexture("Texture/Земля.jpg");
            glgraphics.texturesIDs.Add(texID[4]);
            texID[5] = glgraphics.LoadTexture("Texture/Атмосфера.png");
            glgraphics.texturesIDs.Add(texID[5]);
            texID[6] = glgraphics.LoadTexture("Texture/Луна.png");
            glgraphics.texturesIDs.Add(texID[6]);
            texID[7] = glgraphics.LoadTexture("Texture/Марс.png");
            glgraphics.texturesIDs.Add(texID[7]);
            texID[8] = glgraphics.LoadTexture("Texture/Юпитер.png");
            glgraphics.texturesIDs.Add(texID[8]);
            texID[9] = glgraphics.LoadTexture("Texture/Сатурн.png");
            glgraphics.texturesIDs.Add(texID[9]);

            texID[10] = glgraphics.LoadTexture("Texture/kosmos.jpg");
            glgraphics.texturesIDs.Add(texID[10]);

            texID[11] = glgraphics.LoadTexture("Texture/Звезда.jpg");
            glgraphics.texturesIDs.Add(texID[11]);
            texID[12] = glgraphics.LoadTexture("Texture/Shot.jpg");
            glgraphics.texturesIDs.Add(texID[12]);
            texID[13] = glgraphics.LoadTexture("Texture/Boom.jpg");
            glgraphics.texturesIDs.Add(texID[13]);

            // Комната
            texID[14] = glgraphics.LoadTexture("Texture/Door.png");
            glgraphics.texturesIDs.Add(texID[14]);
            texID[15] = glgraphics.LoadTexture("Texture/Sex.jpg");
            glgraphics.texturesIDs.Add(texID[15]);
            texID[16] = glgraphics.LoadTexture("Texture/flor.jpg");
            glgraphics.texturesIDs.Add(texID[16]);
            texID[17] = glgraphics.LoadTexture("Texture/Wall.jpg");
            glgraphics.texturesIDs.Add(texID[17]);
        }

             
       private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            label7.Text = (glgraphics.music.GetLastIndex() + 1).ToString();

            glgraphics.Update();
            glControl1.SwapBuffers();

            glgraphics.sum = 1;
            for (int i = 0; i < 8; i++)
                glgraphics.sum *= glgraphics.Game[i];
            glgraphics.sum += glgraphics.Game[8];

            /*if ((glgraphics.sum > 10) && (glgraphics.sum < 100))
                {
                    label5.Text = "YOU LOST";
                }
            else if (glgraphics.sum == 1)
                {
                    label5.Text = "YOU WIN";
                }*/
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float widthCoef = (-e.X + glControl1.Width * 0.5f) / (float)glControl1.Width;
            float heightCoef = (-e.Y + glControl1.Height * 0.5f) / (float)glControl1.Height;
            glgraphics.latitude = heightCoef * 180;
            glgraphics.longitude = widthCoef * 360;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
                glControl1.Refresh();
        }

        private void glControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (glgraphics.flag != 10)
            {
                // Выстрел
                glgraphics.rotateB[glgraphics.flag] = 0;
                glgraphics.flag += 1;
                // Звук выстрела
                glgraphics.PlayMusic("Sound/Ship_Shot_01.wav");
            }
            else
                label4.Text = "RECHARGE! Press R";

            String s = (10 - glgraphics.flag).ToString();

            label1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (glgraphics.Cheker > 1) glgraphics.Cheker--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (glgraphics.Cheker < 2) glgraphics.Cheker++;
        }

        // Функция включения/выключения звука
        private void soundChange()
        {
            glgraphics.soundStatus = !glgraphics.soundStatus;
            if (glgraphics.soundStatus)
            {
                Sound_button.BackgroundImage = global::Lab_One_OpenGL.Properties.Resources.Sound_on_25x25;
            }
            else
            {
                Sound_button.BackgroundImage = global::Lab_One_OpenGL.Properties.Resources.Sound_off_25x25;
            }
        }

        // Функция задающая громкость музыки в игре
        private void setVolume(int val)
        {
            if (!glgraphics.soundStatus)
            {
                soundChange();
            }

            ProsentOfSound.Text = (val * 10).ToString() + "%"; // Для счетчика процентов

            int koefVolume = 200;

            for (int i = 0; i <= glgraphics.music.GetLastIndex(); i++)
            {
                glgraphics.music.GetForIndex(i).Volume = (val - 10) * koefVolume;
            }

            if (val == 0)
            {
                soundChange();
            }
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            int num = (int)e.KeyCode;

            switch (num) { 
                case 83: // S - назад
                    glgraphics.Straight(-0.05f);
                    break;
                case 87: // W - вперед
                    glgraphics.Straight(0.05f);
                    break;
                case 82: // R - перезарядка
                    // Звук перезарядки
                    glgraphics.PlayMusic("Sound/Recharge.wav");
                    label4.Text = "";
                    glgraphics.flag = 0;
                    label1.Text = "10";
                    break;
                case 74: // J
                    soundChange();
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Sound_button_Click(object sender, EventArgs e)
        {
            soundChange();
        }

        private void soundBar_Scroll(object sender, EventArgs e)
        {
            setVolume(soundBar.Value);
        }
    }
}
