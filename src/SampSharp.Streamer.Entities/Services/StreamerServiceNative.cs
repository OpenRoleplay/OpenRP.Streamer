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

using SampSharp.Core.Natives.NativeObjects;

#pragma warning disable 1591

namespace SampSharp.Streamer.Entities
{
    public class StreamerServiceNative
    {
        #region Settings
        [NativeMethod]
        public virtual int Streamer_GetMaxItems(int type)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int Streamer_SetMaxItems(int type, int items)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int Streamer_GetVisibleItems(int type, int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int Streamer_SetVisibleItems(int type, int items, int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int Streamer_SetRadiusMultiplier(int type, float multiplier, int playerid)
        {
            throw new NativeNotImplementedException();
        }
        #endregion

        #region Updates

        [NativeMethod]
        public virtual void Streamer_ProcessActiveItems()
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_ToggleIdleUpdate(int playerid, bool toggle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_IsToggleIdleUpdate(int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_ToggleCameraUpdate(int playerid, bool toggle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_IsToggleCameraUpdate(int playerid)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_ToggleItemUpdate(int playerid, int type, bool toggle)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_IsToggleItemUpdate(int playerid, int type)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual void Streamer_GetLastUpdateTime(out float time)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_Update(int playerid, int type)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual bool Streamer_UpdateEx(int playerid, float x, float y, float z, int worldid = -1, int interiorid = -1, int type = -1, int compensatedtime = -1, int freezeplayer = 1)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Objects

        [NativeMethod]
        public virtual int CreateDynamicObject(int modelid, float x, float y, float z, float rx, float ry, float rz,
            int worldid, int interiorid, int playerid, float streamdistance, float drawdistance, int areaid,
            int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Pickups

        [NativeMethod]
        public virtual int CreateDynamicPickup(int modelid, int type, float x, float y, float z, int worldid,
            int interiorid, int playerid, float streamdistance, int areaid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Checkpoints

        [NativeMethod]
        public virtual int CreateDynamicCP(float x, float y, float z, float size, int worldid, int interiorid,
            int playerid, float streamdistance, int areaid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Race Checkpoints

        [NativeMethod]
        public virtual int CreateDynamicRaceCP(int type, float x, float y, float z, float nextx, float nexty,
            float nextz, float size, int worldid, int interiorid, int playerid, float streamdistance, int areaid,
            int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Map Icon

        [NativeMethod]
        public virtual int CreateDynamicMapIcon(float x, float y, float z, int type, int color, int worldid,
            int interiorid, int playerid, float streamdistance, int style, int areaid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Text Labels

        [NativeMethod]
        public virtual int CreateDynamic3DTextLabel(string text, int color, float x, float y, float z,
            float drawdistance, int attachedplayer, int attachedvehicle, bool testlos, int worldid, int interiorid,
            int playerid, float streamdistance, int areaid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion

        #region Area

        [NativeMethod]
        public virtual int CreateDynamicCircle(float x, float y, float size, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicCylinder(float x, float y, float minz, float maxz, float size, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicSphere(float x, float y, float z, float size, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicRectangle(float minx, float miny, float maxx, float maxy, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicCuboid(float minx, float miny, float minz, float maxx, float maxy, float maxz, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicCube(float minx, float miny, float minz, float maxx, float maxy, float maxz, int worldid, int interiorid,
            int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        [NativeMethod]
        public virtual int CreateDynamicPolygon(float[] points, float minz, float maxz, int maxpoints, int worldid,
            int interiorid, int playerid, int priority)
        {
            throw new NativeNotImplementedException();
        }

        #endregion
    }
}
