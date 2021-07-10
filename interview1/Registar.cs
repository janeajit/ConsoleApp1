using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace interview1
{
    class Registar
    {
        public string name { get; set; }
        public int Id { get; set; }
        public DateTime DateOfBirth{ get; set; }
       
        public static (string ,int ,DateTime ) GetPerson(string name, int Id, DateTime DateofBirth)
        {
            return (name,Id,DateofBirth);
        }
    }
}
