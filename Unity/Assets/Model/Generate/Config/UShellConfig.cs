using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class UShellConfigCategory : ProtoObject
    {
        public static UShellConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, UShellConfig> dict = new Dictionary<int, UShellConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<UShellConfig> list = new List<UShellConfig>();
		
        public UShellConfigCategory()
        {
            Instance = this;
        }
		
		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            foreach (UShellConfig config in list)
            {
                this.dict.Add(config.Id, config);
            }
            list.Clear();
            this.EndInit();
        }
		
        public UShellConfig Get(int id)
        {
            this.dict.TryGetValue(id, out UShellConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (UShellConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, UShellConfig> GetAll()
        {
            return this.dict;
        }

        public UShellConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class UShellConfig: ProtoObject, IConfig
	{
		[ProtoMember(1, IsRequired  = true)]
		public int Id { get; set; }
		[ProtoMember(2, IsRequired  = true)]
		public string Name { get; set; }
		[ProtoMember(3, IsRequired  = true)]
		public string Desc { get; set; }
		[ProtoMember(4, IsRequired  = true)]
		public string Model { get; set; }
		[ProtoMember(5, IsRequired  = true)]
		public string AI { get; set; }
		[ProtoMember(6, IsRequired  = true)]
		public string Skills { get; set; }
		[ProtoMember(7, IsRequired  = true)]
		public string TriggerEffect { get; set; }
		[ProtoMember(8, IsRequired  = true)]
		public int TriggerMaxCount { get; set; }
		[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
		[ProtoMember(9, IsRequired  = true)]
		public float MoveSpeed { get; set; }
		[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
		[ProtoMember(10, IsRequired  = true)]
		public float Gravity { get; set; }
		[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
		[ProtoMember(11, IsRequired  = true)]
		public float LifeTime { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
