using System;
using System.Configuration;
using AzureServiceBusDemoService;

namespace AzureServiceBusSampleReceiver
{
    class Program
    {
        static void Main( string[ ] args )
        {
            string connectionString = ConfigurationManager.AppSettings[ "Microsoft.ServiceBus.ConnectionString" ];
            string queueName = "schwabencodedemo";

            using( QueueDemoService demoService = new QueueDemoService( connectionString, queueName ) )
            {
                // On new message write that to the command line
                demoService.OnNewMessage += delegate ( object sender, string message )
                {
                    Console.WriteLine( "[RECEIVE] " + message );
                };

                Console.WriteLine( "Listening to queue... press key to exit." );
                Console.ReadKey();
            }
            Console.WriteLine( "You have pressed a key. Queue closed. Enter key to close demo application." );
            Console.ReadKey();

        }
    }


}
