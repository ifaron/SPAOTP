﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssembyLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemby = Assembly.Load("OTP.WebAPI");
        }
    }
}
