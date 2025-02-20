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
    /// Represents a service for adding entities to and control the Streamer.
    /// </summary>
    public class StreamerService : IStreamerService
    {
        private readonly IEntityManager _entityManager;
        private readonly StreamerServiceNative _native;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamerService" /> class.
        /// </summary>
        public StreamerService(IEntityManager entityManager, INativeProxy<StreamerServiceNative> nativeProxy)
        {
            _entityManager = entityManager;
            _native = nativeProxy.Instance;
        }

        #region Settings
        /// <inheritdoc />
        public int GetMaxItems(StreamerType type)
        {
            return _native.Streamer_GetMaxItems((int)type);
        }

        /// <inheritdoc />
        public int SetMaxItems(StreamerType type, int items)
        {
            return _native.Streamer_SetMaxItems((int)type, items);
        }

        /// <inheritdoc />
        public int GetVisibleItems(StreamerType type, EntityId player)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            return _native.Streamer_GetVisibleItems((int)type, player.Handle);
        }

        /// <inheritdoc />
        public int SetVisibleItems(StreamerType type, int items, EntityId player)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            return _native.Streamer_SetVisibleItems((int)type, items, player.Handle);
        }

        /// <inheritdoc />
        public int SetRadiusMultiplier(StreamerType type, float multiplier, EntityId player)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            return _native.Streamer_SetRadiusMultiplier((int)type, multiplier, player.Handle);
        }
        #endregion

        #region Updates

        /// <inheritdoc />
        public void ProcessActiveItems()
        {
			_native.Streamer_ProcessActiveItems();
        }

        /// <inheritdoc />
        public bool ToggleIdleUpdate(EntityId player, bool toggle)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var success = _native.Streamer_ToggleIdleUpdate(player.Handle, toggle);
            return success;
        }

        /// <inheritdoc />
        public bool IsToggleIdleUpdate(EntityId player)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var isToggled = _native.Streamer_IsToggleIdleUpdate(player.Handle);
            return isToggled;
        }

        /// <inheritdoc />
        public bool ToggleCameraUpdate(EntityId player, bool toggle)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var success = _native.Streamer_ToggleCameraUpdate(player.Handle, toggle);
            return success;
        }

        /// <inheritdoc />
        public bool IsToggleCameraUpdate(EntityId player)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var isToggled = _native.Streamer_IsToggleCameraUpdate(player.Handle);
            return isToggled;
        }

        /// <inheritdoc />
        public bool ToggleItemUpdate(EntityId player, StreamerType type, bool toggle)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var success = _native.Streamer_ToggleItemUpdate(player.Handle, (int)type, toggle);
            return success;
        }

        /// <inheritdoc />
        public bool IsToggleItemUpdate(EntityId player, StreamerType type)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
            {
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);
            }

            var isToggled = _native.Streamer_IsToggleItemUpdate(player.Handle, (int)type);
            return isToggled;
        }

        /// <inheritdoc />
        public bool Update(EntityId player, StreamerType type)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var success = _native.Streamer_Update(player.Handle, (int)type);

            return success;
        }

        /// <inheritdoc />
        public bool UpdateEx(EntityId player, Vector3 position, int virtualWorld = -1, int interior = -1, StreamerType type = StreamerType.All, int compensatedtime = -1, int freezeplayer = 1)
        {
            if (!player.IsOfAnyType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var success = _native.Streamer_UpdateEx(player.Handle, position.X, position.Y, position.Z, virtualWorld, interior, (int)type, compensatedtime, freezeplayer);

            return success;
        }

        #endregion

        #region Objects

        /// <inheritdoc />
        public DynamicObject CreateDynamicObject(int modelId, Vector3 position, Vector3 rotation,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 300.0f,
            float drawDistance = 0.0f, int areaid = -1, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicObject(modelId, position.X, position.Y, position.Z,
                rotation.X, rotation.Y, rotation.Z, virtualWorld, interior, player ? player.Entity.Handle : -1,
                streamDistance, drawDistance, areaid, priority);

            if (id == NativeDynamicObject.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicObjectId(id);
            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicObject>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicObject>(entity);
        }

        #endregion

        #region Pickups

        /// <inheritdoc />
        public DynamicPickup CreateDynamicPickup(int modelId, PickupType pickupType, Vector3 position, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicPickup(modelId, (int)pickupType, position.X, position.Y, position.Z, virtualWorld, interior,
                player ? player.Entity.Handle : -1, streamDistance, areaid, priority);

            if (id == NativeDynamicPickup.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicPickupId(id);
            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicPickup>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicPickup>(entity, position);
        }

        #endregion

        #region Checkpoints

        /// <inheritdoc />
        public DynamicCheckpoint CreateDynamicCheckpoint(Vector3 position, float size, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicCP(position.X, position.Y, position.Z, size, virtualWorld, interior,
                player ? player.Entity.Handle : -1, streamDistance, areaid, priority);

            if (id == NativeDynamicCheckpoint.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicCheckpointId(id);
            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicCheckpoint>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicCheckpoint>(entity, position, size);
        }

        #endregion

        #region Race Checkpoints

        /// <inheritdoc />
        public DynamicRaceCheckpoint CreateDynamicRaceCheckpoint(CheckpointType type, Vector3 position, Vector3 nextPosition, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0,
            EntityId parent = default)
        {
            var id = _native.CreateDynamicRaceCP((int)type, position.X, position.Y, position.Z, nextPosition.X, nextPosition.Y, nextPosition.Z,
                size, virtualWorld, interior, player ? player.Entity.Handle : -1, streamDistance, areaid, priority);

            if (id == NativeDynamicRaceCheckpoint.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicRaceCheckpointId(id);
            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicRaceCheckpoint>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicRaceCheckpoint>(entity, position, nextPosition);
        }

        #endregion

        #region Map Icon

        /// <inheritdoc />
        public DynamicMapIcon CreateDynamicMapIcon(Vector3 position, MapIcon mapIcon, Color color,
            int virtualWorld = -1, int interior = -1, Player player = null, float streamDistance = 200.0f,
            MapIconType style = MapIconType.Local, int areaid = -1, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicMapIcon(position.X, position.Y, position.Z, (int)mapIcon, color,
                virtualWorld, interior, player ? player.Entity.Handle : -1, streamDistance, (int)style, areaid, priority);

            if (id == NativeDynamicMapIcon.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicRaceCheckpointId(id);
            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicMapIcon>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicMapIcon>(entity, position, mapIcon, style, color);
        }

        #endregion

        #region Text Labels

        /// <inheritdoc />
        public DynamicTextLabel CreateDynamicTextLabel(string text, Color color, Vector3 position, float drawDistance,
            Player attachedPlayer = null, Vehicle attachedVehicle = null, bool testLos = false, int virtualWorld = -1, int interior = -1,
            Player player = null, float streamDistance = 200.0f, int areaid = -1, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamic3DTextLabel(text, color, position.X, position.Y, position.Z,
            drawDistance, attachedPlayer ? attachedPlayer.Entity.Handle : 0xFFFF, attachedVehicle ? attachedVehicle.Entity.Handle : 0xFFFF, testLos, virtualWorld, interior,
            player ? player.Entity.Handle : -1, streamDistance, areaid, priority);

            if (id == NativeDynamicTextLabel.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicTextLabelId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicTextLabel>(entity);
            _entityManager.AddComponent<NativeDynamicWorldObject>(entity);

            return _entityManager.AddComponent<DynamicTextLabel>(entity, text, color, position, drawDistance, virtualWorld);
        }

        #endregion

        #region Area

        /// <inheritdoc />
        public DynamicArea CreateCircle(Vector2 position, float size, int virtualWorld = -1, int interior = -1,
            Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicCircle(position.X, position.Y, size, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreateCylinder(Vector2 position, float minz, float maxz, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicCylinder(position.X, position.Y, minz, maxz, size, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreateSphere(Vector3 position, float size,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicSphere(position.X, position.Y, position.Z, size, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreateRectangle(Vector2 min, Vector2 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicRectangle(min.X, min.Y, max.X, max.Y, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreateCuboid(Vector3 min, Vector3 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicCuboid(min.X, min.Y, min.Z, max.X, max.Y, max.Z, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreateCube(Vector3 min, Vector3 max,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicCube(min.X, min.Y, min.Z, max.X, max.Y, max.Z, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        /// <inheritdoc />
        public DynamicArea CreatePolygon(float[] points, float minz = float.NegativeInfinity, float maxz = float.PositiveInfinity,
            int virtualWorld = -1, int interior = -1, Player player = null, int priority = 0, EntityId parent = default)
        {
            var id = _native.CreateDynamicPolygon(points, minz, maxz, points.Length, virtualWorld, interior,
                player ? player.Entity.Handle : -1, priority);

            if (id == NativeDynamicArea.InvalidId)
                throw new EntityCreationException();

            var entity = StreamerEntities.GetDynamicAreaId(id);

            _entityManager.Create(entity, parent);

            _entityManager.AddComponent<NativeDynamicArea>(entity);

            return _entityManager.AddComponent<DynamicArea>(entity);
        }

        #endregion
    }
}
