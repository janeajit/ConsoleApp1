using System;

using HelloWorldConsoleApp1;

namespace ConsoleApp1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
                //constructorExample c = new constructorExample();
                //constructorExample c1 = new constructorExample("ajit");
                //Console.WriteLine("c1" + c.Example1);
                //Console.WriteLine("c1" + c.ajit);

                //Console.WriteLine("c2" + c1.Example2);
                var response = await new Controller().Mountebank();
                
        }
    }
}
