using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Lab5
{

    public partial class Form1 : Form
    {
        float dx = 0, dy = 0;
        float sx = 0.004f, sy = 0.0033f;
        float directX = -1;
        float directY = -1;
        public Form1()
        {

            InitializeComponent();

            glControl1 = new GLControl();
            //создаются обработчики событий для glControl
            glControl1.Resize += GLControl_Resize; // события Resize 
            glControl1.Load += GLControl_Load;
            glControl1.Paint += GLControl_Paint;
            glControl1.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(glControl1);
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
        }
        private void GLControl_Load(object sender, EventArgs e)
        {
        }
        private Color currentColor = Color.FromArgb(178, 252, 177);
        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            // очистка буферов цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // настройка ортогональной проекции 
            float ratio = (float)(glControl1.Width / (float)glControl1.Height);
            if (ratio > 1)
                GL.Ortho(0.0, 30.0 * ratio, 0.0, 30.0, -10, 10);
            else
                GL.Ortho(0.0, 30.0 / ratio, 0.0, 30.0, -10, 10);

            

            // граница
            GL.Color3(0f, 0f, 1.0f);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(1, 1); GL.Vertex2(1, 29);
            GL.Vertex2(49, 29); GL.Vertex2(49, 1);
            GL.End();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity(); // Единичная матрица
            GL.Translate(dx, dy, 0);

            GL.Begin(PrimitiveType.Quads);

            // Часть первой буквы "D"
            GL.Color3(currentColor); GL.Vertex2(19, 20);
            GL.Color3(currentColor); GL.Vertex2(20, 20);
            GL.Color3(currentColor); GL.Vertex2(20, 10);
            GL.Color3(currentColor); GL.Vertex2(19, 10);

            // Буква "V"
            GL.Color3(currentColor); GL.Vertex2(26, 20);
            GL.Color3(currentColor); GL.Vertex2(27, 20);
            GL.Color3(currentColor); GL.Vertex2(29, 10);
            GL.Color3(currentColor); GL.Vertex2(28, 10);

            GL.Color3(currentColor); GL.Vertex2(28, 10);
            GL.Color3(currentColor); GL.Vertex2(29, 10);
            GL.Color3(currentColor); GL.Vertex2(32, 20);
            GL.Color3(currentColor); GL.Vertex2(31, 20);

            // Часть второй буквы "D"
            GL.Color3(currentColor); GL.Vertex2(34, 20);
            GL.Color3(currentColor); GL.Vertex2(35, 20);
            GL.Color3(currentColor); GL.Vertex2(35, 10);
            GL.Color3(currentColor); GL.Vertex2(34, 10);

            GL.End();

            // Часть первой буквы "D" (полуокружность)
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
            double xc1 = 20, yc1 = 15, rad1 = 5;
            double x1, y1;
            for (int i = 0; i <= 15; i++)
            {
                GL.Color3(currentColor);
                x1 = xc1 + rad1 * Math.Sin(i * Math.PI / 15);
                y1 = yc1 + rad1 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x1, y1);
            }
            GL.End();

            // Часть первой буквы "D" (полость в полуокружности)
            GL.Begin(PrimitiveType.TriangleFan);
            double xc3 = 20, yc3 = 15, rad3 = 4;
            double x3, y3;
            for (int i = 0; i <= 15; i++)
            {
                GL.Color3(Color.Black);
                x3 = xc3 + rad3 * Math.Sin(i * Math.PI / 15);
                y3 = yc3 + rad3 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x3, y3);
            }
            GL.End();

            // Часть второй буквы "D" (полуокружность)
            GL.Begin(PrimitiveType.TriangleFan);
            double xc2 = 35, yc2 = 15, rad2 = 5;
            double x2, y2;
            for (int i = 0; i <= 15; i++)
            {
                GL.Color3(currentColor);
                x2 = xc2 + rad2 * Math.Sin(i * Math.PI / 15);
                y2 = yc2 + rad2 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x2, y2);
            }
            GL.End();

            // Часть второй буквы "D" (полость в полуокружности)
            GL.Begin(PrimitiveType.TriangleFan);
            double xc4 = 35, yc4 = 15, rad4 = 4;
            double x4, y4;
            for (int i = 0; i <= 15; i++)
            {
                GL.Color3(Color.Black);
                x4 = xc4 + rad4 * Math.Sin(i * Math.PI / 15);
                y4 = yc4 + rad4 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x4, y4);
            }
            GL.End();

            // Часть изображения оптического диска (эллипс)
            GL.Begin(PrimitiveType.TriangleFan);
            double xc5 = 29, yc5 = 5, rad5 = 3;
            double x5, y5;
            for (int i = 0; i <= 30; i++)
            {
                GL.Color3(currentColor);
                x5 = xc5 + 3.5 * rad5 * Math.Sin(i * Math.PI / 15);
                y5 = yc5 + rad5 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x5, y5);
            }
            GL.End();

            // Часть в изображении оптического диска (полость в эллипсе)
            GL.Begin(PrimitiveType.TriangleFan);
            double xc6 = 29, yc6 = 5, rad6 = 1;
            double x6, y6;
            for (int i = 0; i <= 30; i++)
            {
                GL.Color3(Color.Black);
                x6 = xc6 + 3.5 * rad6 * Math.Sin(i * Math.PI / 15);
                y6 = yc6 + rad6 * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x6, y6);
            }
            GL.End();

            // Границы отскока картинки
            if (dx <= -18)
            {
                directX = 1;
                currentColor = Color.FromArgb(255, 0, 0);
            }
            if (dx > 9)
            {
                directX = -1;
                currentColor = Color.FromArgb(0, 255, 0);
            }
            dx += directX * sx;
            if (dy <= -1)
            {
                directY = 1;
                currentColor = Color.FromArgb(0, 0, 255);
            }
            if (dy > 9)
            {
                directY = -1;
                currentColor = Color.FromArgb(255, 255, 0);
            }
            dy += directY * sy;

            GL.PopMatrix();
            glControl1.SwapBuffers();
            glControl1.Invalidate();
        }
    }
}

