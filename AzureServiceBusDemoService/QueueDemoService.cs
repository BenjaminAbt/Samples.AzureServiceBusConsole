using System;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace AzureServiceBusDemoService
{
    /// <summary>
    /// Demo Service
    /// </summary>
    public class QueueDemoService : IDisposable
    {

        private QueueClient _queueClient;

        /// <summary>
        /// Creates an instance of the demo service
        /// </summary>
        /// <param name="connectionString">Your personal connection string from app.config</param>
        /// <param name="queueName">Name of your Queue</param>
        public QueueDemoService( string connectionString, string queueName )
        {
            // Create client
            _queueClient = QueueClient.CreateFromConnectionString( connectionString, queueName );

            _queueClient.OnMessage( HandleOnQueueMessage );
        }

        /// <summary>
        /// Sends a message to the queue
        /// </summary>
        /// <param name="payload">Message to send. Will be converted to json</param>
        /// <returns>True if message was send, false if queue is closed or diposed.</returns>
        public bool Send( string payload )
        {
            if( _queueClient != null )
            {
                _queueClient.Send( new BrokeredMessage( JsonConvert.SerializeObject( payload ) ) );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Subscripte here to get the messages from queue
        /// </summary>
        public EventHandler<string> OnNewMessage;

        /// <summary>
        /// Handles queue message receive
        /// </summary>
        /// <param name="brokeredMessage"></param>
        private void HandleOnQueueMessage( BrokeredMessage brokeredMessage )
        {
            // Get the origin json payload
            string jsonPayload = brokeredMessage.GetBody<string>();

            // convert to string
            string message = JsonConvert.DeserializeObject<string>( jsonPayload );

            OnNewMessage?.Invoke( this, message );
        }

        /// <summary>
        /// Just a frame to provide using
        /// </summary>
        public void Dispose()
        {
            if( _queueClient != null )
            {
                _queueClient.Close();
                _queueClient = null;
            }
        }
    }
}
