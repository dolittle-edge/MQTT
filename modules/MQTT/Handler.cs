/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Text;
using System.Threading.Tasks;
using Dolittle.Edge.Modules;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using MQTTnet;
using MQTTnet.Server;

namespace Dolittle.Edge.MQTT
{
    /// <summary>
    /// 
    /// </summary>
    public class Data
    {

    }

    /// <summary>
    /// Represents a <see cref="ICanHandle{T}"/> for storing messages offline
    /// </summary>
    public class Handler : ICanHandle<Data>
    {
        readonly ICommunicationClient _client;
        readonly ILogger _logger;
        readonly IMqttServer _mqttServer;

        /// <summary>
        /// Initializes a new instance ofr <see cref="Handler"/>
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> for communication</param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        /// <param name="mqttServer"></param>
        public Handler(
            ICommunicationClient client,
            ILogger logger,
            IMqttServer mqttServer)
        {
            _client = client;
            _logger = logger;
            _mqttServer = mqttServer;
            _mqttServer.ClientConnected += (s, e) => logger.Information("Client connected");
            _mqttServer.ClientDisconnected += (s, e) => logger.Information("Client disconnected");
            _mqttServer.ApplicationMessageReceived += MessageReceived;
        }

        void MessageReceived(object sender, MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            var json = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
            _logger.Information($"MQTT Message received on '{eventArgs.ApplicationMessage.Topic}' - '{json}'");

            try
            {
                _client.SendRaw(eventArgs.ApplicationMessage.Topic, eventArgs.ApplicationMessage.Payload);
                _logger.Information("Raw message sent");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error forwarding message as raw");
            }
        }

        /// <inheritdoc/>
        public Input Input => "events";

        /// <inheritdoc/>
        public async Task Handle(Data data)
        {
            await Task.CompletedTask;
        }
    }
}