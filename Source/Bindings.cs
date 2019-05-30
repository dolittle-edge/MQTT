/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using MQTTnet;
using MQTTnet.Server;
using Dolittle.DependencyInversion;

namespace Dolittle.TimeSeries.MQTT
{
    /// <summary>
    /// Provides bindings for the module
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var mqttServer = new MqttFactory().CreateMqttServer();
            mqttServer.StartAsync(new MqttServerOptions()).Wait();
            builder.Bind<IMqttServer>().To(mqttServer);
        }
    }
}