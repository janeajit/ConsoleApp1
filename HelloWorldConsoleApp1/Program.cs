using System;
using System.ComponentModel;
using HelloWorldConsoleApp1;

namespace HelloWorldConsoleApp1
{

    public class constructorExample
    {
        private string ian = "Ian";

        //public string john2;
        public string jane = "jane";
        public string ajit = "Ajit";
        public string Example1;
        public string Example2;
        public constructorExample()
        {
            Example1 = "Jane";

        }
        public constructorExample(string jane)
        {

            Example2 = ian;


        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            constructorExample c = new constructorExample();
            constructorExample c1 = new constructorExample("ajit");
            Console.WriteLine("c1" + c.Example1);
            Console.WriteLine("c1" + c.ajit);
           
            Console.WriteLine("c2" + c1.Example2);
        }
    }
}
