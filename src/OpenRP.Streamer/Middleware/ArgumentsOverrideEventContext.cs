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

using System;
using SampSharp.Entities;

namespace OpenRP.Streamer
{
    internal class ArgumentsOverrideEventContext : EventContext
    {
        public ArgumentsOverrideEventContext(int argumentCount)
        {
            Arguments = new object[argumentCount];
        }

        public EventContext BaseContext { get; set; }

        public override string Name => BaseContext.Name;

        public override object[] Arguments { get; }

        public override IServiceProvider EventServices => BaseContext.EventServices;
    }
}