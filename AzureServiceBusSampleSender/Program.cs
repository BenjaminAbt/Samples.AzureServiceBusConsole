using System;
using System.Configuration;
using AzureServiceBusDemoService;

namespace AzureServiceBusSampleSender
{
    class Program
    {
        static void Main( string[ ] args )
        {

            string connectionString = ConfigurationManager.AppSettings[ "Microsoft.ServiceBus.ConnectionString" ];
            string queueName = "schwabencodedemo";

            using( QueueDemoService demoService = new QueueDemoService( connectionString, queueName ) )
            {
                // Send text here
                string send;
                do
                {
                    Console.Write( "Please enter text to send: " );
                    send = Console.ReadLine();

            

                    Console.Write( "Sending.." );
                    demoService.Send( send );
                    Console.WriteLine( "..done" );

                } while( !String.IsNullOrEmpty( send ) );

                Console.WriteLine( "Closing queue.." );
            }
            Console.WriteLine( ".. done" );


            Console.WriteLine( "Enter key to close demo application." );
            Console.ReadKey();

        }
    }


}
