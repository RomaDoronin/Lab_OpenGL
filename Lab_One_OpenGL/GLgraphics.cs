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
        public float Cheker = 1.0f;

        Vector3 cameraPosition = new Vector3(2, 3, 4); //Позиция камеры
        Vector3 cameraDirecton = new Vector3(0, 0, 0); //Направление камеры
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

        // Функция рисующая Тестовый квадрат
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
            // Тестовый крадрат
            //drawTestQuad();

            // Трансформированный квадрат
            /*GL.PushMatrix();
            GL.Translate(1, 1, 1);
            GL.Rotate(rotateAngle, Vector3.UnitZ);
            GL.Scale(0.5f, 0.5f, 0.5f);
            drawTestQuad();
            GL.PopMatrix();
            drawTexturedQuad();*/
            
            // Сфера
            //drawSphere(1.5f, 10, 10, Color.BlueViolet, 8);

            // Точка
            //drawPoint();

            // Линия
            //drawLine(1, 1, 1);

            // Окружность
            //drawCircle(1, Color.Red, false);

            // Треугольнкик
            //drawTriangle();

            // Шестиугольник
            //drawTriangleStrip();

            // Пирамида четырехугольная без основания
            //drawTriangleFun();  

            // Куб с текстурами
            /*GL.PushMatrix();
            GL.Scale(0.5f, 0.5f, 0.5f);
            DrawCube();
            GL.PopMatrix();*/

            // Солнечная система
            DrawSolSystem();

            // Координатные оси (X,Y,Z)
            /*drawLine(1, 0, 0);
            drawLine(0, 1, 0);
            drawLine(0, 0, 1);*/

            // Фон
            DrawSqCube();

            // Шар - положение источника света
            /*GL.PushMatrix();
            GL.Translate(0.0f, 0.0f, 1.1f);
            drawSphere(0.2f, 20, 20, Color.Yellow, 1);
            GL.PopMatrix();*/

        }

        // Функция загружающая текстуры
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

        // Функция рисующая квадрат с текстурами
        private void drawTexturedQuad()
        {
            // Включение наложения текстур
            GL.Enable(EnableCap.Texture2D);
            // Указание, какую текстуру берем
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[8]);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            // Задание Текстурных координат
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            // Задание Текстурных координат
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            // Задание Текстурных координат
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            // Задание Текстурных координат
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();

            // Выключение наложения текстур
            GL.Disable(EnableCap.Texture2D);
        }

        // Функция настройки Света
        private void SetupLightning()
        {
            // Включение Освещения
            GL.Enable(EnableCap.Lighting);
            // Включение нулевого источника света
            GL.Enable(EnableCap.Light0);
            // Включение освещение цветных вершин
            GL.Enable(EnableCap.ColorMaterial);

            // Установка позиции источника света
            Vector4 lightPosition = new Vector4(0.0f, 0.0f, 1.5f, 0.0f);
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);

            // Установка цвета, который будет иметь объект, не освещенный источником
            Vector4 ambientColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, ambientColor);

            // Установка цвета, который будет иметь объект, освещенный источником
            Vector4 diffuseColor = new Vector4(0.6f, 0.6f, 1.0f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseColor);

            // Установка материалам зеркальной состовляющей
            Vector4 materialSpecular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            float materialShininess = 100;
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);
        }

        // Функция рисующая Сферу
        private void drawSphere(double r, int nx, int ny, Color ColorPlanet, int NumPlanet)
        {
            // r - радиус сферы
            // nx * ny - колличество полигонов(четырехугольников) из которых будет собрана сфера
            // ColorPlanet - цвет сферы
            // NumPlanet - номер текстуры для накладывания на сферу
            
            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                if (NumPlanet == 8)
                {
                    GL.Enable(EnableCap.Texture2D);
                    GL.BindTexture(TextureTarget.Texture2D, texturesIDs[NumPlanet]);
                }
                GL.Begin(PrimitiveType.QuadStrip);
                //if (NumPlanet != 8)
                    GL.Color3(ColorPlanet);

                for (ix = 0; ix <= nx; ++ix)
                {
                    
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

        // Функция рисующая Точку
        private void drawPoint()
        {
            // Задание размера точки
            GL.PointSize(5);

            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Black);
            GL.Vertex3(0, 0, 0);
            GL.End();
        }

        // Функция рисующая Линию
        private void drawLine(int x, int y, int z)
        {
            // Задание ширины/толщины линии
            GL.LineWidth(5);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Black);
            GL.Vertex3(0, 0, 0);
            GL.Color3(Color.Black);
            GL.Vertex3(x, y, z);
            GL.End();
        }

        // Функция рисующая  Треугольник
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

        // Функция рисующая Шестиугольник
        private void drawTriangleStrip()
        {
            // TriangleStrip - рисует треугольники, беря вершины по тройкам в последовательности
         
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(Color.White);
            GL.Vertex3(-1.7f, -1.0f, -1.0f); // 1
            GL.Color3(Color.White);
            GL.Vertex3(-1.7f, 1.0f, -1.0f);  // 1,2
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, -2.0f, -1.0f);  // 1,2,3
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, 2.0f, -1.0f);   // 2,3,4
            GL.Color3(Color.Red);
            GL.Vertex3(1.7f, -1.0f, -1.0f);  // 3,4
            GL.Color3(Color.Red);
            GL.Vertex3(1.7f, 1.0f, -1.0f);   // 4
            GL.End();
        }

        // Функция рисующая Четырехугольную пирамиду
        private void drawTriangleFun()
        {
            // TriangleFan - рисует треугольтики, беря как вершины первую и две последние

            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, 0.0f, 1.0f);    // 1,2,3,4
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, -1.0f);   // 1,4
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, -1.0f, -1.0f);  // 1,2
            GL.Color3(Color.Green);
            GL.Vertex3(-1.0f, -1.0f, -1.0f); // 2,3
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);  // 3,4
            // Вершина повторяется, чтобы замкнуть пирамиду
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.End();
        }

        // Функция рисующая Куб с текстурами
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

        // Функция рисующая Солнечную Систему
        private void DrawSolSystem()
        {
            // Солнце
            GL.PushMatrix();
            GL.Rotate((rotateAngle * 24.47f / 50), Vector3.UnitZ);
            drawSphere(1.0f, 20, 20, Color.Yellow, 8);
            GL.PopMatrix();

            Color CCorcle = Color.White;

            drawCircle(0.387 * 3, CCorcle, false);
            drawCircle(0.733 * 3, CCorcle, false);
            drawCircle(3, CCorcle, false);
            drawCircle(1.52 * 3, CCorcle, false);
            drawCircle(5.19 * 3, CCorcle, false);
            drawCircle(9.53 * 3, CCorcle, false);

            // Меркурий

            // Координаты положения планеты:
            // Math.Sin(rotateAngle / <скорость вращения>) * <радиус вращения> * 3
            // Math.Cos(rotateAngle / <скорость вращения>) * <радиус вращения> * 3
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 7.23) * 0.387 * 3,
                Math.Cos(rotateAngle / 7.23) * 0.387 * 3,
                0);

            // GL.Rotate(rotateAngle * <скорость вращения вокруг своей оси> / 50, Vector3.UnitZ);
            GL.Rotate(rotateAngle * 88 / 50, Vector3.UnitZ);
            drawSphere(0.111f / 3, 20, 20, Color.Orange, 10);
            GL.PopMatrix();
            
            // Венера
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 18.46) * 0.733 * 3,
                Math.Cos(rotateAngle / 18.46) * 0.733 * 3,
                0);
            GL.Rotate(rotateAngle * 200 / 50, Vector3.UnitZ);
            drawSphere(0.291f / 3, 20, 20, Color.LightBlue, 100);
            GL.PopMatrix();

            // Земля
            GL.PushMatrix();
            GL.Translate(
                Math.Sin(rotateAngle / 30) * 3,
                Math.Cos(rotateAngle / 30) * 3,
                0);
            GL.Rotate(rotateAngle / 50, Vector3.UnitZ);
            drawSphere(0.3f / 3, 20, 20, Color.DarkBlue, 1);
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

        // Функция рисующая Окружность
        private void drawCircle(double Radius, Color CircleColor, bool round)
        {
            //Переменна round нужна для наклона колец Cатурна

            GL.PointSize(1);

            GL.Begin(PrimitiveType.Points);
            double i = 0;
            double alf;

            while (i < 10)
            {
                if (round) 
                    alf = Math.Sin(i) * Radius;
                else
                    alf = 0;
                GL.Color3(CircleColor);
                GL.Vertex3(Math.Sin(i) * Radius, Math.Cos(i) * Radius, alf);
                i += 0.01;
            }
            GL.End();
        }

        // Функция рисующая Треугольники в углах фоновой текстуры
        private void DrawAlf(float Size, float SizeTr, int x, int y, int z)
        {
            // Size - размер фоновой текстуры
            // SizeTr - размер угловых треугольников

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[7]);
            GL.Begin(PrimitiveType.Triangles);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(x * SizeTr, y * Size, z * Size);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(x * Size, y * SizeTr, z * Size);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(x * Size, y * Size, z * SizeTr);
            GL.TexCoord2(1.0, 0.0);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        // Функция рисующая фоновую текстуру "КОСМОС"
        private void DrawSqCube()
        {
            float Size = 32.0f, SizeTr = 28.0f;

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

            //Углы фоновой текстуры
            DrawAlf(Size, SizeTr, 1, 1, 1);
            DrawAlf(Size, SizeTr, 1, 1, -1);
            DrawAlf(Size, SizeTr, 1, -1, 1);
            DrawAlf(Size, SizeTr, 1, -1, -1);
            DrawAlf(Size, SizeTr, -1, 1, 1);
            DrawAlf(Size, SizeTr, -1, 1, -1);
            DrawAlf(Size, SizeTr, -1, -1, 1);
            DrawAlf(Size, SizeTr, -1, -1, -1);
        }

    }
}
