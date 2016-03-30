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
            Matrix4 viewMat = Matrix4.LookAt(cameraPosition, cameraDirecton, cameraUp);
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
            //drawTexturedQuad();
           /* GL.Color3(Color.BlueViolet);
            drawSphere(1.0f, 20, 20);*/
            
            //drawPoint();

            //drawLine();

            //drawTriangle(); //Треугольник

            //drawTriangleStrip(); // Шестиугольник

            //drawTriangleFun(); // Пирамида четырехугольная без основания  

            DrawCube();
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
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
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

        private void drawSphere(double r, int nx, int ny)
        {
            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                GL.Begin(BeginMode.QuadStrip);
                for (ix = 0; ix <= nx; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
            }
        }

        private void drawPoint() //!!!
        {
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Blue);
            GL.End();
        }
        private void drawLine()  //!!!
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Blue);
            GL.Normal3(0, 0, 1);
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
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.End();

            // Вторая Грань
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(0.0f, 1.0f, 1.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(0.0f, 0.0f, 1.0f);
            GL.End();

            // Третья Грань
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(1.0f, 0.0f, 1.0f);
            GL.Color3(Color.Blue);
            GL.Vertex3(0.0f, 0.0f, 1.0f);
            GL.End();

            // Четвертая Грань
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, 0.0f, 0.0f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Yellow);
            GL.Vertex3(1.0f, 0.0f, 1.0f);
            GL.End();

            // Пятая Грань
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(1.0f, 1.0f, 0.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Color3(Color.Orange);
            GL.Vertex3(0.0f, 1.0f, 1.0f);
            GL.End();

            // Шестая Грань
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Purple);
            GL.Vertex3(0.0f, 0.0f, 1.0f);
            GL.Color3(Color.Purple);
            GL.Vertex3(1.0f, 0.0f, 1.0f);
            GL.Color3(Color.Purple);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Color3(Color.Purple);
            GL.Vertex3(0.0f, 1.0f, 1.0f);
            GL.End();
        }
    }
}
