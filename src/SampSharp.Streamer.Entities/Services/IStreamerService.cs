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

namespace SampSharp.Streamer.Entities
{
    /// <summary>
    ///     Provides functionality for adding entities to and controlling the Streamer.
    /// </summary>
    public interface IStreamerService
    {
        #region General
        /// <summary>
		/// Gets the current maximum item amount (number of items that can be created with the plugin natives) for specified item type.
        /// </summary>
		/// <param name="type">The item type.</param>
		/// <returns>
		///		<see cref="int"/>
		/// </returns>
        int GetMaxItems(StreamerType type);

        /// <summary>
        /// Sets the current maximum item amount (number of items that can be created with the plugin natives) for the specified item type.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="items">The maximum item amount (-1 for no imposed limit).</param>
        /// <returns>
        ///		<see cref="int"/>
        /// </returns>
        int SetMaxItems(StreamerType type, int items);

        /// <summary>
        /// Gets the current visible item amount (number of items that can be streamed) for the specified item type and player.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="playerid">The player ID.</param>
        /// <returns>
        ///		<see cref="int"/>
        /// </returns>
        int GetVisibleItems(StreamerType type, EntityId playerid);

        /// <summary>
        /// Gets the current visible item amount (number of items that can be streamed) for the specified item type and player.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="items">The maximum item amount (-1 for no imposed limit).</param>
        /// <param name="playerid">The player ID.</param>
        /// <returns>
        ///		<see cref="int"/>
        /// </returns>
        int SetVisibleItems(StreamerType type, int items, EntityId playerid);

        /// <summary>
        /// Sets the current streaming radius multiplier for the specified item type and player.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="multiplier">The float multiplier.</param>
        /// <param name="playerid">The player ID.</param>
        /// <returns>
        ///		<see cref="int"/>
        /// </returns>
        int SetRadiusMultiplier(StreamerType type, float multiplier, EntityId playerid);
        #endregion

        #region Updates

        /// <summary>
        ///		Updates any item that is currently active (that is, moving or attached), including:
        ///		<list type="bullet">
        ///		<item>
        ///		<description>
        ///		Moving and attached objects.
        ///		</description>
        ///		</item>
        ///		<item>
        ///		<description>
        ///		Attached areas and 3D text labels.
        ///		</description>
        ///		</item>
        ///		</list>
        /// </summary>
        void ProcessActiveItems();

        /// <summary>
		///		Toggles whether updates are issued while the player is idle.
		///		By default, this is turned off.
        /// </summary>
		/// <param name="player">The player.</param>
		/// <param name="toggle"><c>false</c> to turn off, <c>true</c> to turn on.</param>
		/// <returns>
		///		<c>false</c> on failure, <c>true</c> on success.
		///		<see cref="bool"/>
		/// </returns>
		bool ToggleIdleUpdate(EntityId player, bool toggle);

        /// <summary>
		/// Returns whether <c>Streamer_ToggleIdleUpdate</c> is enabled for the player.
        /// </summary>
		/// <param name="player">The player.</param>
		/// <returns>
		///		<see cref="bool"/>
		/// </returns>
		bool IsToggleIdleUpdate(EntityId player);

        /// <summary>
		/// Toggles whether updates are based on the player's current camera position
		/// rather than the player's current world position.
		/// <para>
		/// By default, this is turned off.
		/// </para>
        /// </summary>
		/// <param name="player">The player.</param>
		/// <param name="toggle"><c>false</c> to turn off, <c>true</c> to turn on.</param>
		/// <returns>
		///		<c>false</c> on failure, <c>true</c> on success.
		///		<see cref="bool"/>
		/// </returns>
		bool ToggleCameraUpdate(EntityId player, bool toggle);

        /// <summary>
		/// Returns whether <c>Streamer_ToggleCameraUpdate</c> is enabled for the player.
        /// </summary>
		/// <param name="player">The player.</param>
		/// <returns>
		///		<see cref="bool"/>
		/// </returns>
		bool IsToggleCameraUpdate(EntityId player);

        /// <summary>
		/// Toggles whether updates are issued for the specified player and item type.
        /// </summary>
		/// <param name="player">The player.</param>
		/// <param name="type">The item type.</param>
		/// <param name="toggle"><c>false</c> to turn off, <c>true</c> to turn on.</param>
		/// <returns>
		///		<c>false</c> on failure, <c>true</c> on success.
		///		<see cref="bool"/>
		/// </returns>
		bool ToggleItemUpdate(EntityId player, StreamerType type, bool toggle);

        /// <summary>
		/// Returns whether <c>Streamer_ToggleItemUpdate</c>
		/// is enabled for the specified player and item type.
		/// <para>
		/// By default, every item type is turned on, except for NPCs.
		/// </para>
        /// </summary>
		/// <param name="player">The player.</param>
		/// <param name="type">The item type.</param>
		/// <returns>
		///		<see cref="bool"/>
		/// </returns>
		bool IsToggleItemUpdate(EntityId player, StreamerType type);

        /// <summary>
        ///     Issues an update for the player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="type">The item type.</param>
        /// <returns>
		///		<c>false</c> on failure, <c>true</c> on success.
        ///     <see cref="bool"/>
        /// </returns>
        bool Update(EntityId player, StreamerType type);

        /// <summary>
        ///     Issues an update for the player at a specific position.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="position">The position.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="type">The item type.</param>
        /// <param name="compensatedtime">The compensated time in milliseconds.</param>
        /// <param name="freezeplayer">The freeze player (0 to turn off, 1 to turn on).</param>
        /// <returns>
        ///     <see cref="bool"/>
        /// </returns>
        bool UpdateEx(EntityId player, Vector3 position, int virtualWorld = -1, int interior = -1, StreamerType type = StreamerType.All, int compensatedtime = -1, int freezeplayer = 1);

        #endregion

        #region Objects

        /// <summary>
        ///     Creates a new Dynamic Object in the world.
        /// </summary>
        /// <param name="modelId">The model id.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The attached player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="areaid">The attached area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicObject"/>
        /// </returns>
        DynamicObject CreateDynamicObject(int modelId, Vector3 position, Vector3 rotation,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 200.0f,
            float drawDistance = 0.0f, int areaid = -1, int priority = 0, EntityId parent = default);

        #endregion

        #region Pickups

        /// <summary>
        ///     Creates a new Dynamic Pickup in the world.
        /// </summary>
        /// <param name="modelId">The model id.</param>
        /// <param name="pickupType">The pickup type.</param>
        /// <param name="position">The position.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The attached player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="areaid">The attached area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicPickup"/>
        /// </returns>
        DynamicPickup CreateDynamicPickup(int modelId, PickupType pickupType, Vector3 position, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default);

        #endregion

        #region Checkpoint

        /// <summary>
        ///     Creates a new Dynamic Checkpoint in the world.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The attached player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="areaid">The attached area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicCheckpoint"/>
        /// </returns>
        DynamicCheckpoint CreateDynamicCheckpoint(Vector3 position, float size, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default);

        #endregion

        #region Race Checkpoint

        /// <summary>
        ///     Creates a new Dynamic Race Checkpoint in the world.
        /// </summary>
        /// <param name="type">The check point type.</param>
        /// <param name="position">The position.</param>
        /// <param name="nextPosition">The next position.</param>
        /// <param name="size">The size.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The attached player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="areaid">The attached area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicRaceCheckpoint"/>
        /// </returns>
        DynamicRaceCheckpoint CreateDynamicRaceCheckpoint(CheckpointType type, Vector3 position, Vector3 nextPosition, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0,
            EntityId parent = default);

        #endregion

        #region Map Icon

        /// <summary>
        ///     Creates a new Dynamic Map Icon in the world.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="mapIcon">The map icon type.</param>
        /// <param name="color">The color.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The attached player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="style">The map icon style.</param>
        /// <param name="areaid">The attached area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicMapIcon"/>
        /// </returns>
        DynamicMapIcon CreateDynamicMapIcon(Vector3 position, MapIcon mapIcon, Color color,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 200.0f,
            MapIconType style = MapIconType.Local, int areaid = -1, int priority = 0, EntityId parent = default);

        #endregion

        #region Text Labels

        /// <summary>
        ///     Creates a new Dynamic Text Label in the world. 
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="color">The color.</param>
        /// <param name="position">The position.</param>
        /// <param name="drawDistance">The draw distance.</param>
        /// <param name="attachedPlayer">The attach player.</param>
        /// <param name="attachedVehicle">The attache vehicle.</param>
        /// <param name="testLos">The test los.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player.</param>
        /// <param name="streamDistance">The stream distance.</param>
        /// <param name="areaid">The area id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicTextLabel"/>
        /// </returns>
        DynamicTextLabel CreateDynamicTextLabel(string text, Color color, Vector3 position, float drawDistance,
            Player attachedPlayer = null, Vehicle attachedVehicle = null, bool testLos = false, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default);

        #endregion

        #region Area

        /// <summary>
        ///     Creates a new dynamic circle in the world.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateCircle(Vector2 position, float size, int virtualWorld = -1, int interior = -1,
            Player player = null, int priority = 0, EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic cylinder in the world.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="minz">The minz.</param>
        /// <param name="maxz">The maxz.</param>
        /// <param name="size">The size.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateCylinder(Vector2 position, float minz, float maxz, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic sphere in the world.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateSphere(Vector3 position, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic rectange in the world.
        /// </summary>
        /// <param name="min">The min XY.</param>
        /// <param name="max">The max XY.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateRectangle(Vector2 min, Vector2 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic cuboid in the world.
        /// </summary>
        /// <param name="min">The min XYZ.</param>
        /// <param name="max">The max XYZ.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateCuboid(Vector3 min, Vector3 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic cube in the world.
        /// </summary>
        /// <param name="min">The min XYZ.</param>
        /// <param name="max">The max XYZ.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreateCube(Vector3 min, Vector3 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0,
            EntityId parent = default);

        /// <summary>
        ///     Creates a new dynamic polygon in the world.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="minz">The minz.</param>
        /// <param name="maxz">The maxz.</param>
        /// <param name="virtualWorld">The virtual world.</param>
        /// <param name="interior">The interior.</param>
        /// <param name="player">The player</param>
        /// <param name="priority">The priority.</param>
        /// <param name="parent">The EntityId parent.</param>
        /// <returns>
        ///     <see cref="DynamicArea"/>
        /// </returns>
        DynamicArea CreatePolygon(float[] points, float minz = float.NegativeInfinity, float maxz = float.PositiveInfinity,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default);

        #endregion
    }
}
