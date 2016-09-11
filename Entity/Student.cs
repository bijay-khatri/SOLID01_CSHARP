using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility.Entity
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Height { get; set; }

        public override string ToString()
        {
            return $"[Name = {Name}, Height = {Height}]";
        }
    }
}
