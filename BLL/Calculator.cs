using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Calculator
    {
        public static dynamic Add(dynamic x, dynamic y)
        {

            #region Super unnecessary business logic for testing
            #region IsString
            if (x is string || y is string) { throw new ArgumentException("You cannot add a string to an int", "x"); }
            #endregion
            #region isNull
            if (x == 0 || y == 0) { return "zeros not allowed"; }
            #endregion
            #region isDouble
            if (x is double || y is double) { return "cannot use doubles for addition"; }
            #endregion
            #endregion

            return x + y;
        }

        public static int Substract(int x, int y)
        {
            return x - y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }

        public static int Divide(int x, int y)
        {
            return x / y;
        }
    }
}
