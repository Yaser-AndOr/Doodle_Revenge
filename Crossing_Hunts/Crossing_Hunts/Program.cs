using System;
using Tao.FreeGlut;
using OpenGL;
using System.Numerics;

namespace Crossing_Hunts
{
    class Program
    {
        private static String ccr = "0.3", ccg = "0.3", ccb = "0.8";

        private static ShaderProgram program;

        public static VBO<Vector3> triangulo, square;
        private static VBO<uint> Trielementos, Cuadrilementos;
        private static int width = 1200, heigth = 680;
        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, heigth);
            Glut.glutCreateWindow("Crossing Hunts");

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);

            Glut.glutKeyboardFunc(KeyGame);
            Glut.glutMouseFunc(MouseGame);

            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / heigth, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, new Vector3(0, 1, 0)));

            triangulo = new VBO<Vector3>(new Vector3[] { new Vector3(0, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0) });
            Trielementos = new VBO<uint>(new uint[] { 0, 1, 2 }, BufferTarget.ElementArrayBuffer);

            square = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) });
            Cuadrilementos = new VBO<uint>(new uint[] { 0, 1, 2, 3 }, BufferTarget.ElementArrayBuffer);

            Glut.glutMainLoop();
        }
        private static void OnDisplay()
        {

        }


        private static void OnRenderFrame()
        {
            float cr = float.Parse(ccr);
            float cg = float.Parse(ccg);
            float cb = float.Parse(ccb);

            Gl.Viewport(0, 0, width, heigth);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Gl.UseProgram(program);
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(-1.5f, 0, 0)));

            uint vertexPositionIndex = (uint)Gl.GetAttribLocation(program.ProgramID, "vertexPosition");
            Gl.EnableVertexAttribArray(vertexPositionIndex);
            Gl.BindBuffer(triangulo);
            Gl.VertexAttribPointer(vertexPositionIndex, triangulo.Size, triangulo.PointerType, true, 12, IntPtr.Zero);
            Gl.BindBuffer(Trielementos);

            Gl.DrawElements(BeginMode.Triangles, Trielementos.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            program["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(1.5f, 0, 0)));
            
            Gl.BindBufferToShaderAttribute(square, program, "vertexPosition");
            Gl.BindBuffer(Cuadrilementos);
            Gl.DrawElements(BeginMode.Quads, Cuadrilementos.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            
            Gl.ClearColor(cr, cg, cb, 1);
            Glut.glutSwapBuffers();

        }
        private static void MouseGame(int click, int stado, int x, int y)
        {
            if (click == Glut.GLUT_LEFT_BUTTON && stado == Glut.GLUT_UP)
            {
                square = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, -2), new Vector3(-1, -1, 2) });
                ccr = "0.8";
                ccg = "0.3";
                ccb = "0.3";
            }
            if (click == Glut.GLUT_RIGHT_BUTTON && stado == Glut.GLUT_UP)
            {
                square = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) });
                ccr = "0.8";
                ccg = "0.8";
                ccb = "0.3";
            }
            if (click == Glut.GLUT_MIDDLE_BUTTON && stado == Glut.GLUT_UP)
            {
                ccr = "0.8";
                ccg = "0.3";
                ccb = "0.8";
            }
            if (x > 50 && x < 100 && y > 50 && y < 100)
            {
                ccr = "0.8";
                ccg = "0.8";
                ccb = "0.8";
            }
        }
        private static void KeyGame(byte key, int a, int b)
        {
            if (key == 'r')
            {
                ccr = "0.8";
                ccg = "0.3";
                ccb = "0.3";
            }
            else if (key == 'g')
            {
                ccr = "0.3";
                ccg = "0.8";
                ccb = "0.3";
            }
            else if (key == 'b')
            {
                ccr = "0.3";
                ccg = "0.3";
                ccb = "0.8";
            }
        }

        public static string VertexShader = @"

#version 130

in vec3 vertexPosition;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main (void) 
{
    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}

";
        public static string FragmentShader = @"

#version 130

out vec4 fragment;
void main (void)

{
    fragment = vec4(1, 1, 1, 1);
}

";
    }
}