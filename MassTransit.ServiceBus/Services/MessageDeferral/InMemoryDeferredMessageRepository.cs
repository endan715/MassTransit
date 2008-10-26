// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.ServiceBus.Services.MessageDeferral
{
    using System;
    using System.Collections.Generic;

    public class InMemoryDeferredMessageRepository :
        IDeferredMessageRepository
    {
        private readonly Dictionary<Guid, DeferredMessage> _messages = new Dictionary<Guid, DeferredMessage>();

        public void Add(DeferredMessage message)
        {
            lock (_messages)
                _messages.Add(message.Id, message);
        }

        public DeferredMessage Get(Guid id)
        {
            lock (_messages)
                if (_messages.ContainsKey(id))
                    return _messages[id];

            return null;
        }

        public bool Contains(Guid id)
        {
            lock (_messages)
                return _messages.ContainsKey(id);
        }

        public void Remove(Guid id)
        {
            lock (_messages)
                _messages.Remove(id);
        }

        public void Dispose()
        {
            _messages.Clear();
        }
    }
}