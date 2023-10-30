using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;

/// <summary>
/// Aydogan Mert
/// Grupa 3131-A
/// Calculatoare anul 3 
/// </summary>

namespace Aydogan_Mert_3131A
{
    internal class SimpleWindow3D : GameWindow
    {
        private const int XYZ_SIZE = 75;
        private const float rotation_speed = 180.0f;
        private float angle;
        private bool showCube = true;
        private bool moveLeft, moveRight, moveUp, moveDown;
        private bool moveMouseLeft, moveMouseRight;
        private int colorRed = 0, colorGreen = 0, colorBlue = 0;
        private int minColor = 0, maxColor = 255;
        private int[] WiewPort = new int[3];

        private const string FileName = ".\\CoordonateTriunghi.txt";

        //functie pentru a verifica daca culoarea este la minim sau maxim

        public bool CheckIfInRangeColor(int color)
        {
            if (color >= minColor && color < maxColor)
                return true;
            return false;
        }

        private KeyboardState lastKeyPress;

        // Constructor.
        public SimpleWindow3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            WiewPort[0] = WiewPort[1] = WiewPort[2] = 10;
        }

        /**Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
           fundal a ferestrei 3D.
           Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D. */

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.LightSeaGreen);
            //GL.Enable(EnableCap.DepthTest);
        }

        /**Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
           ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD()!
           Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi
           diferite). */

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        /** Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
            automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc. */

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = OpenTK.Input.Keyboard.GetState();
            MouseState mouse = OpenTK.Input.Mouse.GetState();
            moveLeft = false;
            moveRight = false;
            moveUp = false;
            moveDown = false;

            ///Laborator 3 - punctul 9 si 8 cu modificarea culori

            #region Laborator 3 - punctul 9- Modificare culoare RGB

            if (keyboard[OpenTK.Input.Key.G])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(colorGreen))
                    {
                        colorGreen++;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(colorGreen - 1))
                    {
                        colorGreen--;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.R])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(colorRed))
                    {
                        colorRed++;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(colorRed - 1))
                    {
                        colorRed--;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
            }

            if (keyboard[OpenTK.Input.Key.B])
            {
                if (keyboard[OpenTK.Input.Key.Up])
                {
                    if (CheckIfInRangeColor(colorBlue))
                    {
                        colorBlue++;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
                else if (keyboard[OpenTK.Input.Key.Down])
                {
                    if (CheckIfInRangeColor(colorBlue - 1))
                    {
                        colorBlue--;
                        Console.WriteLine("Red: " + colorRed + " Green: " + colorGreen + " Blue: " + colorBlue);
                    }
                }
            }

            #endregion Laborator 3 - punctul 9- Modificare culoare RGB

            //Verificare daca o tasta este apasata

            #region Verificare si modificare stare tasta

            if (keyboard[OpenTK.Input.Key.Escape])
            {
                Exit();
                return;
            }
            if (keyboard[OpenTK.Input.Key.Left])
            {
                moveLeft = true;
            }

            if (keyboard[OpenTK.Input.Key.Right])
            {
                moveRight = true;
            }
            if (keyboard[OpenTK.Input.Key.Up])
            {
                moveUp = true;
            }
            if (keyboard[OpenTK.Input.Key.Down])
            {
                moveDown = true;
            }
            else if (keyboard[OpenTK.Input.Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Ascundere comandată, prin apăsarea unei taste - cu verificare de remanență! Timpul de reacție uman << calculator.
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            lastKeyPress = keyboard;

            #endregion Verificare si modificare stare tasta

            //Laborator 3 Miscare cu mouse

            #region Laborator 3 Miscare mouse

            moveMouseLeft = false;
            moveMouseRight = false;

            if (mouse.X < -50)
            {
                moveMouseLeft = true;
                if (WiewPort[0] > -10)
                    WiewPort[0]--;
            }
            else if (mouse.X > 100)
            {
                moveMouseRight = true;
                if (WiewPort[0] < 20)
                    WiewPort[0]++;
            }
            if (mouse.Y < -50)
            {
                moveMouseLeft = true;
                if (WiewPort[1] > -10)
                    WiewPort[1]--;
            }
            else if (mouse.Y > 100)
            {
                moveMouseRight = true;
                if (WiewPort[1] < 20)
                    WiewPort[1]++;
            }

            #endregion Laborator 3 Miscare mouse

            //if (mouse[OpenTK.Input.MouseButton.Left])
            //{
            //    // Ascundere comandată, prin clic de mouse - fără testare remanență.
            //    if (showCube == true)
            //    {
            //        showCube = false;
            //    }
            //    else
            //    {
            //        showCube = true;
            //    }
            //}
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //  Matrix4 lookat = Matrix4.LookAt(6, 6, 10, 0, 0, 0, 0, 1, 0);
            Matrix4 lookat = Matrix4.LookAt(WiewPort[0], WiewPort[1], WiewPort[2], 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            DrawAxes();

            Triunghi T = Triunghi.ReadCoordonates(FileName);
            T.DrawMe(colorRed, colorGreen, colorBlue);

            //Laborator 2 - Miscarea cubului cu ajutorul tastelor

            #region Laborator 2 - Miscarea cubului cu ajutorul tastelor

            //if (moveLeft == true)
            //{
            //    angle += rotation_speed * (float)e.Time;
            //    GL.Rotate(angle, 0.0f, -1.0f, 0.0f);
            //}

            //if (moveRight == true)
            //{
            //    angle += rotation_speed * (float)e.Time;
            //    GL.Rotate(angle, 0.0f, 1.0f, 0.0f);
            //}
            //if (moveUp == true)
            //{
            //    angle += rotation_speed * (float)e.Time;
            //    GL.Rotate(angle, 0.0f, 1.0f, 1.0f);
            //}
            //if (moveDown == true)
            //{
            //    angle += rotation_speed * (float)e.Time;
            //    GL.Rotate(angle, 0.0f, 0.0f, -1.0f);
            //}

            //if (keyboard[OpenTK.Input.Key.Right])
            //{
            //    angle += rotation_speed * (float)e.Time;
            //    GL.Rotate(angle, 0.0f, 0.0f, 1.0f);
            //}
            //angle += rotation_speed * (float)e.Time;
            //GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            #endregion Laborator 2 - Miscarea cubului cu ajutorul tastelor

            // Exportăm controlul randării obiectelor către o metodă externă (modularizare).
            if (showCube == true)
            {
                DrawCube();
            }
            SwapBuffers();
            //Thread.Sleep(1);
        }

        //Laboratorul 3- punctul 1 - Desenarea axelor

        #region Laborator 3 - Punctul 1

        private void DrawAxes()
        {
            GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }

        #endregion Laborator 3 - Punctul 1

        // Desenare cub

        #region DrawCube

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        #endregion DrawCube

        [STAThread]
        private static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
