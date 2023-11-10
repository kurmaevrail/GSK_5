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
                GL.Color3(1f, 0, 0); // текущий цвет - красный (от 0 до 1)
                                     // режим рисования закрашенных треугольников 
                GL.Begin(PrimitiveType.Triangles);
                GL.Vertex2(15, 15); GL.Vertex2(13, 13); GL.Vertex2(15, 1);
                GL.Vertex2(13, 13); GL.Vertex2(15, 15); GL.Vertex2(1, 15);
                GL.Vertex2(1, 15); GL.Vertex2(15, 15); GL.Vertex2(13, 17);
                GL.Vertex2(13, 17); GL.Vertex2(15, 15); GL.Vertex2(15, 29);
                GL.Vertex2(15, 29); GL.Vertex2(15, 15); GL.Vertex2(17, 17);
                GL.Vertex2(17, 17); GL.Vertex2(15, 15); GL.Vertex2(29, 15);
                GL.Vertex2(29, 15); GL.Vertex2(17, 13); GL.Vertex2(15, 15);
                GL.Vertex2(15, 15); GL.Vertex2(15, 1); GL.Vertex2(17, 13);
                GL.End();
                GL.LineWidth(4f); // толщина линий

                GL.Color3(0.7f, 0.2f, 0.8f);
                GL.Begin(PrimitiveType.LineLoop); // замкнутая линия
                GL.Vertex2(15, 1);
                GL.Vertex2(12, 12);
                GL.Vertex2(1, 15);
                GL.Vertex2(12, 18);
                GL.Vertex2(15, 29);
                GL.Vertex2(18, 18);
                GL.Vertex2(29, 15);
                GL.Vertex2(18, 12);
                GL.End();
                // граница
                GL.Color3(0f, 0f, 1.0f);
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(1, 1); GL.Vertex2(1, 29);
                GL.Vertex2(29, 29); GL.Vertex2(29, 1);
                GL.End();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity(); // Единичная матрица
                GL.Translate(dx, dy, 0);
                // шарик
                double xc = 15, yc = 15, r = 1;
                double x, y;
                GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
                GL.Color3(0.4f, 0.5f, 1.0f);
                GL.Vertex2(xc + 0.8f, yc + 0.3);
                GL.Vertex2(xc + 1, yc);
                for (int i = 0; i <= 30; i++)
                {
                    GL.Color3(0f, 0f, 1.0f);
                    x = xc + r * Math.Sin(i * Math.PI / 15);
                    y = yc + r * Math.Cos(i * Math.PI / 15);
                    GL.Vertex2(x, y);
                }
                GL.End();
                if (dx <= -13) directX = 1;
    if (dx > 13) directX = -1;
                dx += directX * sx;
                if (dy <= -13) directY = 1;
                if (dy > 13) directY = -1;
                dy += directY * sy;
                GL.PopMatrix();
                glControl1.SwapBuffers();
                glControl1.Invalidate();
            }
        }
    }

