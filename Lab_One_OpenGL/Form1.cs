using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            texID[0] = glgraphics.LoadTexture("logo_ITMM.png");
            glgraphics.texturesIDs.Add(texID[0]);
            
            texID[1] = glgraphics.LoadTexture("1.png");
            glgraphics.texturesIDs.Add(texID[1]);
            texID[2] = glgraphics.LoadTexture("2.png");
            glgraphics.texturesIDs.Add(texID[2]);
            texID[3] = glgraphics.LoadTexture("3.png");
            glgraphics.texturesIDs.Add(texID[3]);
            texID[4] = glgraphics.LoadTexture("4.png");
            glgraphics.texturesIDs.Add(texID[4]);
            texID[5] = glgraphics.LoadTexture("5.png");
            glgraphics.texturesIDs.Add(texID[5]);
            texID[6] = glgraphics.LoadTexture("6.png");
            glgraphics.texturesIDs.Add(texID[6]);
            texID[7] = glgraphics.LoadTexture("kosmos.jpg");
            glgraphics.texturesIDs.Add(texID[7]);

            //Текстура Солнца
            texID[8] = glgraphics.LoadTexture("Sun.png");
            glgraphics.texturesIDs.Add(texID[8]);

            // Комната
            texID[9] = glgraphics.LoadTexture("flor.jpg");
            glgraphics.texturesIDs.Add(texID[9]);
            texID[10] = glgraphics.LoadTexture("Wall.jpg");
            glgraphics.texturesIDs.Add(texID[10]);
            texID[11] = glgraphics.LoadTexture("Door.png");
            glgraphics.texturesIDs.Add(texID[11]);
            texID[12] = glgraphics.LoadTexture("Sex.jpg");
            glgraphics.texturesIDs.Add(texID[12]);

            // Система загрузки 100 текстур для Земли
            /*texID[9] = glgraphics.LoadTexture("Меркурий.png"); //Текстура Мекрурия
            glgraphics.texturesIDs.Add(texID[9]);
            texID[10] = glgraphics.LoadTexture("venus.png"); //Текстура Венеры
            glgraphics.texturesIDs.Add(texID[10]);
            texID[12] = glgraphics.LoadTexture("Марс.png"); //Текстура Марса
            glgraphics.texturesIDs.Add(texID[12]);
            texID[13] = glgraphics.LoadTexture("Юпитер.png"); //Текстура Юпитера
            glgraphics.texturesIDs.Add(texID[13]);
            texID[14] = glgraphics.LoadTexture("Сатурн.png"); //Текстура Сатурна
            glgraphics.texturesIDs.Add(texID[14]);
            texID[15] = glgraphics.LoadTexture("Луна.png"); //Текстура Луны
            glgraphics.texturesIDs.Add(texID[15]);*/

            /*for (int i = 1; i <= 100; i++)
            {
                string kernelStr;
                if (i < 10)
                    kernelStr = "Earth_0";
                else kernelStr = "Earth_";
                kernelStr += Convert.ToString(i);
                if ((i != 13) && (i != 1))
                    kernelStr += ".jpg";
                else kernelStr += ".png";
                texID[8 + i] = glgraphics.LoadTexture(kernelStr); //Текстура Земли
                glgraphics.texturesIDs.Add(texID[8 + i]);
            }*/
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glgraphics.Update();
            glControl1.SwapBuffers();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float widthCoef = (e.X - glControl1.Width * 0.5f) / (float)glControl1.Width;
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
            //glgraphics.ZoomZoom();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (glgraphics.Cheker > 1) glgraphics.Cheker--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (glgraphics.Cheker < 2) glgraphics.Cheker++;
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            int num;
            num = (int)e.KeyCode;
            if (num == 83)
            {
                if (glgraphics.Cheker < 2) glgraphics.Cheker += 0.1f;
            }
            if (num == 87)
            {
                if (glgraphics.Cheker > 1) glgraphics.Cheker -= 0.1f;
            }
        }

        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {

        }

    }
}
