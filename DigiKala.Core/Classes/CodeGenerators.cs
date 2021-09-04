using System;
using System.Collections.Generic;
using System.Text;

namespace DigiKala.Core.Classes
{
    public class CodeGenerators
    {
        public static string ActivationCode()
        {
            Random random = new Random();
            return random.Next(100000,999999).ToString();
        }
        public static string FactorCode()
        {
            Random random = new Random();
            return random.Next(10000000,99999999).ToString();
        }
        public static string FileName() => Guid.NewGuid().ToString();

    }
}
