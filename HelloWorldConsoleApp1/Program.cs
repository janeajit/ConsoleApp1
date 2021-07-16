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

            "body": "{"Events":[{"IdempotentId":"30627997-a747-4c4b-a017-cdfe3a844676"," +
                ""Type":"notifications"," +
                ""Version":"1.0.0.0"," +
                ""Date":"2021-07-13T22:53:19.621495Z"," +
                ""Payload":{"SpinSportVisitId":"e6e7bae1-6f91-45b6-bdef-87449e655b8e"," +
                ""NotificationType":"slotSessionStatus"," +
                ""LossLimitPercentageRemaining\":10,\"TotalWagers\":45656.0,\"TotalPayouts\":1544.0,\"TotalNetWin\":233.0,\"GameSessionLimitValidation\":21,\"NotificationStartTime:"2021-07-13T11:53:18.000Z"}}],"CorrelationId":"df71519e-9fed-4d80-96ec-5a47fd751ad9"}"
        }
    }
}
