﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem_v2
{
      public class Program
     {
        static void Main(string[] args)
        {
            CourseRepository repository = new CourseRepository();
            repository.InitializeDatabase();
        }
     }
}
