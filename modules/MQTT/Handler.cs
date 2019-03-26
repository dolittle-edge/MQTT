/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Edge.Modules;

namespace Dolittle.Edge.Arundo
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

        /// <summary>
        /// Initializes a new instance ofr <see cref="Handler"/>
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> for communication</param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        public Handler(ICommunicationClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
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