using OpenTK.Graphics.OpenGL;

using System.Drawing;
using System.IO;


namespace Aydogan_Mert_3131A
{
    internal class Triunghi : Punct
    {
        private Punct A, B, C;

        //public void TriunghiManual()
        //{
        //    IsDrawable = true;

        //    A = new Punct(5, 2, 0, Color.Red);
        //    B = new Punct(8, 8, 0, Color.Green);
        //    C = new Punct(1, 1, 0, Color.Blue);
        //}

        public Triunghi()
        {
        }

        public Triunghi(Punct a, Punct b, Punct c)
        {
            A = new Punct(a.getX(), a.getY(), a.getZ());
            B = new Punct(b.getX(), b.getY(), b.getZ());
            C = new Punct(c.getX(), c.getY(), c.getZ());
        }

        public void DrawMe()
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(A.getColor());
            GL.Vertex3(A.getX(), A.getY(), A.getZ());
            GL.Color3(B.getColor());
            GL.Vertex3(B.getX(), B.getY(), B.getZ());
            GL.Color3(C.getColor());
            GL.Vertex3(C.getX(), C.getY(), C.getZ());

            GL.End();
        }

        public void DrawMe(int red, int green, int blue)
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.FromArgb(red, 0, 0));
            GL.Vertex3(A.getX(), A.getY(), A.getZ());
            GL.Color3(Color.FromArgb(0, green, 0));
            GL.Vertex3(B.getX(), B.getY(), B.getZ());
            GL.Color3(Color.FromArgb(0, 0, blue));
            GL.Vertex3(C.getX(), C.getY(), C.getZ());

            GL.End();
        }

        public static Triunghi ReadCoordonates(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName);
            string[] result;
            int[] coordonate = new int[3];
            Punct[] vertex = new Punct[3];

            int j = 0;
            foreach (string line in lines)
            {
                int i = 0;
                result = line.Split(' ');
                foreach (string var in result)
                {
                    coordonate[i] = int.Parse(var);
                    i++;
                }
                vertex[j] = new Punct(coordonate[0], coordonate[1], coordonate[2]);
                j++;
            }
            Triunghi T = new Triunghi(vertex[0], vertex[1], vertex[2]);
            return T;
        }
    }
}
