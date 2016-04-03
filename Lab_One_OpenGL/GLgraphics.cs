using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Lab_One_OpenGL
{
    class GLgraphics
    {
        public float rotateAngle;
        public List<int> texturesIDs = new List<int>();
        public int Cheker = 1;

        Vector3 cameraPosition = new Vector3(2, 3, 4);
        Vector3 cameraDirecton = new Vector3(0, 0, 0);
        Vector3 cameraUp = new Vector3(0, 0, 1);

        public float latitude = 47.98f;
        public float longitude = 60.41f;
        public float radius = 5.385f;

        public void Setup(int width, int height)
        {
            GL.ClearColor(Color.DarkGray); //Заливает буфер экрана одним цветом
            GL.ShadeModel(ShadingModel.Smooth); //Устанавливает тип отрисовки полигонов с оттенками
            GL.Enable(EnableCap.DepthTest); //Включает буфер глубины

            Matrix4 perspectiveMat = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                width / (float)height, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiveMat);
            SetupLightning();
        }

        public void Update()
        {
            rotateAngle += 0.1f;

            //Функция отчищает буферы
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Управление камерой
            Matrix4 viewMat = Matrix4.LookAt(cameraPosition * Cheker, cameraDirecton, cameraUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMat);
            Render();

            cameraPosition = new Vector3(
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) * Math.Cos(Math.PI / 180.0f * longitude)),
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) * Math.Sin(Math.PI / 180.0f * longitude)),
                (float)(radius * Math.Sin(Math.PI / 180.0f * latitude)));
        }

        private void drawTestQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();
        }

        public void Render()
        {
            //drawTestQuad();
            //GL.PushMatrix();
            //GL.Translate(1, 1, 1);
            //GL.Rotate(45/*rotateAngle*/, Vector3.UnitZ);
            //GL.Scale(0.5f, 0.5f, 0.5f);
            //drawTestQuad();
            //GL.PopMatrix();
           // drawTexturedQuad();
            
            // drawSphere(1.5f, 20, 20, Color.BlueViolet);

            //drawPoint(); //Точка

            //drawLine(); //Линия

            //drawCircle(1); //Окружность

            //drawTriangle(); //Треугольник

            //drawTriangleStrip(); // Шестиугольник

            //drawTriangleFun(); // Пирамида четырехугольная без основания  

            /*GL.PushMatrix(); // Кубик
            GL.Scale(0.5f, 0.5f, 0.5f);
            DrawCube();
            GL.PopMatrix();*/

            //-----------------
            //Солнечная система
            DrawSolSystem();

            /*drawLine(2, 0, 0);
            drawLine(0, 2, 0);
            drawLine(0, 0, 2);*/

            DrawSqCube();//Фон

        }

        public int LoadTexture(string filePath)
        {
            try
            {
                Bitmap image = new Bitmap(filePath);
                int texID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texID);
                BitmapData data = image.LockBits(
                    new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0,
                    PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                image.UnlockBits(data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                return texID;
            }
            catch (System.IO.FileNotFoundException е)
            {
                return -1;
            }
        }

        private void drawTexturedQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void SetupLightning()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);

            Vector4 lightPosition = new Vector4(1.0f, 1.0f, 4.0f, 0.0f);
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);

            Vector4 ambientColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, ambientColor);

            Vector4 diffuseColor = new Vector4(0.6f, 0.6f, /*0.6f,*/ 1.0f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseColor);

            Vector4 materialSpecular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            float materialShininess = 100;
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);
        }

        private void drawSphere(double r, int nx, int ny, Color ColorPlanet, int NumPlanet)
        {
            

            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                GL.Begin(PrimitiveType.QuadStrip);
                if (NumPlanet != 8)
                    GL.Color3(ColorPlanet);

                for (ix = 0; ix <= nx; ++ix)
                {
                    if (NumPlanet == 8)
                    {
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, texturesIDs[NumPlanet]);
                    }
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                    if (NumPlanet == 8)
                    {
                        if ((ix % 2) == 0) GL.TexCoord2(0.0, 0.0);
                        else GL.TexCoord2(1.0, 1.0);
                    }

                    x = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                    if (NumPlanet == 8)
                    {
                        if ((ix % 2) == 0) GL.TexCoord2(1.0, 0.0);
                        else GL.TexCoord2(0.0, 1.0);
                    }
                }
                GL.End();
                if (NumPlanet == 8)
                    GL.Disable(EnableCap.Texture2D);
            }
        }

        private void drawPoint()
        {
            GL.PointSize(5);

            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Black);
            GL.Vertex3(0, 0, 0);
            GL.End();
        }
        private void drawLine(int x, int y, int z)
        {
            GL.LineWidth(5);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);
            GL.Vertex3(0, 0, 0);
            GL.Color3(Color.Black);
            GL.Vertex3(x, y, z);
            GL.End();
        }

        private void drawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.End();
        }

        private void drawTriangleStrip()
        {
            // Функция рисует треугольники по тройкам в последовательности
         
            GL.Begin(PrimitiveType.TriangleStrip);

            GL.Color3(Color.White);
            GL.Vertex3(-1.7f, -1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.Vertex3(-1.7f, 1.0f, -1.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, -2.0f, -1.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, 2.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(1.7f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(1.7f, 1.0f, -1.0f);           
            GL.End();
        }

        private void drawTriangleFun()
        {
            // Функция рисует треугольтики первая вершина и две последние

            GL.Begin(PrimitiveType.TriangleFan);

            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, 0.0f, 1.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
 
            GL.End();
        }

        private void DrawCube()
        {

            // Первая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Вторая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[1]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Третья Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[2]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Blue);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Blue);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Color3(Color.Blue);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Четвертая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[3]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Yellow);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Yellow);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Yellow);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Yellow);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Пятая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[4]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Шестая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[5]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Orange);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Color3(Color.Orange);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Color3(Color.Orange);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Orange);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void DrawSolSystem()
        {
            //Солнце
            GL.PushMatrix();
            GL.Rotate((rotateAngle * 24.47f / 50), Vector3.UnitZ);
            drawSphere(1.0f, 20, 20, Color.Yellow, 7);
            GL.PopMatrix();

            Color CCorcle = Color.White;

            drawCircle(0.387 * 3, CCorcle, false);
            drawCircle(0.733 * 3, CCorcle, false);
            drawCircle(3, CCorcle, false);
            drawCircle(1.52 * 3, CCorcle, false);
            drawCircle(5.19 * 3, CCorcle, false);
            drawCircle(9.53 * 3, CCorcle, false);

            //Меркурий
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 7.23) * 0.387 * 3,
                Math.Cos(rotateAngle / 7.23) * 0.387 * 3,
                0);
            GL.Rotate(rotateAngle * 88 / 50, Vector3.UnitZ);
            drawSphere(0.111f / 3, 20, 20, Color.Orange, 10);
            GL.PopMatrix();
            
            //Венера
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 18.46) * 0.733 * 3,
                Math.Cos(rotateAngle / 18.46) * 0.733 * 3,
                0);
            GL.Rotate(rotateAngle * 200 / 50, Vector3.UnitZ);
            drawSphere(0.291f / 3, 20, 20, Color.LightBlue, 100);
            GL.PopMatrix();

            //Земля
            GL.PushMatrix();
            GL.Translate(
                //Координаты положения планеты 
                //Math.Sin(rotateAngle / <скорость вращения>) * <радиус вращения> * 3,
                Math.Sin(rotateAngle / 30) * 3,
                Math.Cos(rotateAngle / 30) * 3,
                0);
            GL.Rotate(rotateAngle / 50, Vector3.UnitZ);
            drawSphere(0.3f / 3, 20, 20, Color.DarkBlue, 100);
            GL.PopMatrix();

            //Луна
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 2.3) * 0.05 * 3 + Math.Sin(rotateAngle / 30) * 3,
                Math.Cos(rotateAngle / 2.3) * 0.05 * 3 + Math.Cos(rotateAngle / 30) * 3,
                0);
            drawSphere(0.081f / 3, 20, 20, Color.Gray, 100);
            GL.PopMatrix();

            //Марс
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 56.46) * 1.52 * 3,
                Math.Cos(rotateAngle / 56.46) * 1.52 * 3,
                0);
            GL.Rotate(rotateAngle * 1.025f / 50, Vector3.UnitZ);
            drawSphere(0.456f / 3, 20, 20, Color.Red, 13);
            GL.PopMatrix();

            //Юпитер
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 355.8) * 5.19 * 3,
                Math.Cos(rotateAngle / 355.8) * 5.19 * 3,
                0);
            GL.Rotate(rotateAngle * 9.92f / 50, Vector3.UnitZ);
            drawSphere(3.384f / 3, 20, 20, Color.Orange, 14);
            GL.PopMatrix();

            //Сатурн
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 883.81) * 9.53 * 3,
                Math.Cos(rotateAngle / 883.81) * 9.53 * 3,
                0);
            GL.Rotate(rotateAngle * 10.23f / 50, Vector3.UnitZ);
            drawSphere(2.835f / 3, 20, 20, Color.Peru, 15); 

            //Кольца Сатурна
            double i = 1.1;
            while (i < 1.6)
            {
                drawCircle(i, Color.DarkOrange, true);
                i += 0.05;
            }

            i = 1.7;

            while (i < 2.11)
            {
                drawCircle(i, Color.Orange, true);
                i += 0.05;
            }
            
            GL.PopMatrix();
        }

        private void drawCircle(double Radius, Color CircleColor, bool round)
        {
            //Переменна round нужна для наклона колец Cатурна
            GL.PointSize(1);

            GL.Begin(PrimitiveType.Points);
            double i = 0;
            double alf;

            while ( i < (10) )
            {
                if (round) alf = Math.Sin(i) * Radius;
                else
                    alf = 0;
                GL.Color3(CircleColor);
                GL.Vertex3(Math.Sin(i) * Radius, Math.Cos(i) * Radius, alf);
                i += 0.01;
            }
            GL.End();
        }

        private void DrawSqCube()
        {
            float Size = 32.0f;
            // Первая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-Size, -Size, -Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-Size, Size, -Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Вторая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-Size, -Size, -Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-Size, Size, Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-Size, -Size, Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Третья Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-Size, -Size, -Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-Size, -Size, Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Четвертая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(Size, -Size, -Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(Size, -Size, Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Пятая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(Size, Size, -Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(-Size, Size, -Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-Size, Size, Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);

            // Шестая Грань
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-Size, -Size, Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(Size, -Size, Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(Size, Size, Size);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(-Size, Size, Size);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }
    }
}
