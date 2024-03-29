﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMIS
{
    public class Student
    {
        public int student_id { get; set; }
        public string? given_name { get; set; }
        public string? family_name { get; set; }
        public string? group_id { get; set; }
        public string? title { get; set; }
        public string? campus { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? category { get; set; }

        public override string ToString()
        {
            return $"given_name: {given_name}, family_name: {family_name}";
        }
    }
}
