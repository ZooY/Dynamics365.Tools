using System.Activities;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Xrm.Workflow;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;


namespace PZone.RabbitMqTools.Workflow
{
    /// <summary>
    /// Отправка сообщения в RabbitMQ.
    /// </summary>
    public class Send : WorkflowBase
    {
        [Input("RabbitMQ Server Host")]
        [RequiredArgument]
        public InArgument<string> MqHost { get; set; }


        [Input("RabbitMQ Server Port")]
        public InArgument<int> MqPort { get; set; }


        [Input("RabbitMQ User Name")]
        public InArgument<string> MqUserName { get; set; }


        [Input("RabbitMQ User Password")]
        public InArgument<string> MqUserPassword { get; set; }


        [Input("RabbitMQ Exchange Name")]
        [RequiredArgument]
        public InArgument<string> MqExchangeName { get; set; }


        [Input("RabbitMQ Routing Key")]
        public InArgument<string> MqRoutingKey { get; set; }


        [Input("RabbitMQ Header Name")]
        public InArgument<string> MqHeaderName { get; set; }


        [Input("RabbitMQ Header Value")]
        public InArgument<string> MqHeaderValue { get; set; }

        [Input("Message")]
        public InArgument<string> Message { get; set; }

        [Input("Correlation ID")]
        public InArgument<string> CorrelationId { get; set; }


        protected override void Execute(Context context)
        {
            var mqHost = MqHost.Get(context);
            var mqPort = MqPort.Get(context);
            var mqUserName = MqUserName.Get(context);
            var mqUserPassword = MqUserPassword.Get(context);
            var mqExchangeName = MqExchangeName.Get(context);
            var mqRoutingKey = MqRoutingKey.Get(context);
            var mqHeaderName = MqHeaderName.Get(context);
            var mqHeaderValue = MqHeaderValue.Get(context);


            var factory = new ConnectionFactory { HostName = mqHost };
            if (mqPort > 0)
                factory.Port = mqPort;
            if (!string.IsNullOrWhiteSpace(mqUserName))
            {
                factory.UserName = mqUserName;
                factory.Password = mqUserPassword;
            }
            using (var con = factory.CreateConnection())
            using (var channel = con.CreateModel())
            {
                var properties = new BasicProperties { DeliveryMode = 2 };
                if (!string.IsNullOrWhiteSpace(mqHeaderName))
                    properties.Headers = new Dictionary<string, object> { { mqHeaderName, mqHeaderValue } };
                var correlationId = CorrelationId.Get(context);
                if (!string.IsNullOrWhiteSpace(correlationId))
                    properties.CorrelationId = correlationId;
                channel.BasicPublish(
                    exchange: mqExchangeName,
                    routingKey: mqRoutingKey,
                    body: Encoding.UTF8.GetBytes(Message.Get(context)),
                    basicProperties: properties
                );
            }
        }
    }
}