using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class UTrapConfigCategory : ProtoObject
    {
        public static UTrapConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, UTrapConfig> dict = new Dictionary<int, UTrapConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<UTrapConfig> list = new List<UTrapConfig>();
		
        public UTrapConfigCategory()
        {
            Instance = this;
        }
		
		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            foreach (UTrapConfig config in list)
            {
                this.dict.Add(config.Id, config);
            }
            list.Clear();
            this.EndInit();
        }
		
        public UTrapConfig Get(int id)
        {
            this.dict.TryGetValue(id, out UTrapConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (UTrapConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, UTrapConfig> GetAll()
        {
            return this.dict;
        }

        public UTrapConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class UTrapConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string Desc { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public string Icon { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public string Model { get; set; }
		[ProtoMember(6, IsRequired  = true)]
		public string AI { get; set; }
		[ProtoMember(7, IsRequired  = true)]
		public string Skills { get; set; }
		[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
		[ProtoMember(8, IsRequired  = true)]
		public float AttackSpeed { get; set; }
		[ProtoMember(9, IsRequired  = true)]
		public int HPDamage { get; set; }
		[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
		[ProtoMember(10, IsRequired  = true)]
		public float LifeTime { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
