﻿// SampSharp.Streamer
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace OpenRP.Streamer
{
    internal class PlayerEnterDynamicCheckpointMiddleware
    {
        private readonly EventDelegate _next;

        public PlayerEnterDynamicCheckpointMiddleware(EventDelegate next)
        {
            _next = next;
        }

        public object Invoke(EventContext context, IEntityManager entityManager)
        {
            var playerEntity = SampEntities.GetPlayerId((int)context.Arguments[0]);
            var checkpointEntity = StreamerEntities.GetDynamicCheckpointId((int)context.Arguments[1]);

            if (!entityManager.Exists(playerEntity))
                return null;

            if (!entityManager.Exists(checkpointEntity))
                return null;

            context.Arguments[0] = playerEntity;
            context.Arguments[1] = checkpointEntity;

            return _next(context);
        }
    }
}