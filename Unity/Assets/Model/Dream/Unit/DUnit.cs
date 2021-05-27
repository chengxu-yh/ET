using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace ET
{
    // Level中的实例化对象：位置、朝向基础信息
    [BsonIgnoreExtraElements]
    public sealed class DUnit: Entity
    {
        private Vector3 position; // 坐标

        public Vector3 Position
        {
            get => this.position;
            set
            {
                this.position = value;
                Game.EventSystem.Publish(new AppEventType.ChangePosition() { Unit = this }).Coroutine();
            }
        }

        private Quaternion rotation; // 朝向

        public Quaternion Rotation
        {
            get => this.rotation;
            set
            {
                this.rotation = value;
                Game.EventSystem.Publish(new AppEventType.ChangeRotation() {Unit = this}).Coroutine();
            }
        }

        [BsonIgnore]
        public Vector3 Forward
        {
            get => this.Rotation * Vector3.forward;
            set => this.Rotation = Quaternion.LookRotation(value, Vector3.up);
        }
    }
}