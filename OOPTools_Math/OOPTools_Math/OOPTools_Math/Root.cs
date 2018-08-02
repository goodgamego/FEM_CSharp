using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OOPTools_Math
{
    public class Root
    {
        public delegate double Function(double x);
        public double FindRoot(Function Fun,  double x_o, double ErrorX, double ErrorF, int MaxIterations, out double Dx, out double F, out int Iterations)
        {
            Derivative.Function F1 = new Derivative.Function(Fun);
            Derivative DFunDx = new Derivative();

            double x = x_o;
            F = Fun(x);
            double Dx1, Dx2;
            Dx2 = 0.0D;
            Dx = 0.0D;
            Iterations = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                double DFDx = DFunDx.DfDx(F1,x,10*ErrorX,(ErrorF/ErrorX)/1.0E-10);
                if (DFDx < 1.0E-100)
                {
                    Dx = -F / (ErrorF / ErrorX);
                }
                else
                {
                    Dx = -F / DFDx;
                }
                x += Dx;
                Dx1 = Dx2;
                Dx2 = Dx;
                F = Fun(x);
                Iterations = i + 1;
                if (i > 2)
                {
                    if (Math.Abs(F) < ErrorF)
                    {
                        if ((Math.Abs(Dx1) < ErrorX) && (Math.Abs(Dx2) < ErrorX))
                        {
                            Iterations = i + 1;
                            return x;
                        }
                    }
                }

            }
            return x;  

        }
        public double FindRoot(Function Fun, Function DFunDx, double x_o, double ErrorX, double ErrorF, int MaxIterations, out double Dx, out double F, out int Iterations)
        {
            double x = x_o;
            F = Fun(x);
            double Dx1,Dx2;
            Dx2 = 0.0d;
            Dx = 0.0D;
            Iterations = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                double DFDx = DFunDx(x);
                if (DFDx < 1.0E-100)
                {
                    Dx = -F / (ErrorF/ErrorX);
                }
                else
                {
                    Dx = -F / DFDx;
                }
                x += Dx;
                Dx1=Dx2;
                Dx2=Dx;
                F = Fun(x);
                Iterations = i + 1;
                if (i > 2)
                {
                    if (Math.Abs(F) < ErrorF)
                    {
                        if ((Math.Abs(Dx1) < ErrorX) && (Math.Abs(Dx2) < ErrorX))
                        {
                            Iterations = i + 1;
                            return x;
                        }
                    }
                }

            }
            return x;  
        }
        public double FindRoot(Function Fun, Function DFunDx, double x_o)
        {
            double Dx, F;
            int Iterations;
            return FindRoot(Fun, DFunDx, x_o, 1.0E-20, 1.0E-20, 20, out Dx, out F, out Iterations);
        }
        public double FindRoot(Function Fun,  double x_o)
        {
            double Dx, F;
            int Iterations;
            return FindRoot(Fun,  x_o, 1.0E-20, 1.0E-20, 20, out Dx, out F, out Iterations);
        }
    }
}
