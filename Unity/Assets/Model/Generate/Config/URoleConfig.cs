using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class URoleConfigCategory : ProtoObject
    {
        public static URoleConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, URoleConfig> dict = new Dictionary<int, URoleConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<URoleConfig> list = new List<URoleConfig>();
		
        public URoleConfigCategory()
        {
            Instance = this;
        }
		
		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            foreach (URoleConfig config in list)
            {
                this.dict.Add(config.Id, config);
            }
            list.Clear();
            this.EndInit();
        }
		
        public URoleConfig Get(int id)
        {
            this.dict.TryGetValue(id, out URoleConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (URoleConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, URoleConfig> GetAll()
        {
            return this.dict;
        }

        public URoleConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class URoleConfig: ProtoObject, IConfig
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
		public float AlertRadius { get; set; }
		[ProtoMember(8, IsRequired  = true)]
		public string Skills { get; set; }
		[ProtoMember(9, IsRequired  = true)]
		public float AttackSpeed { get; set; }
		[ProtoMember(10, IsRequired  = true)]
		public float MoveSpeed { get; set; }
		[ProtoMember(11, IsRequired  = true)]
		public int Defense { get; set; }
		[ProtoMember(12, IsRequired  = true)]
		public int HPDamage { get; set; }
		[ProtoMember(13, IsRequired  = true)]
		public int HPRegain { get; set; }
		[ProtoMember(14, IsRequired  = true)]
		public int HP { get; set; }
		[ProtoMember(15, IsRequired  = true)]
		public int AngerRegain { get; set; }
		[ProtoMember(16, IsRequired  = true)]
		public int Anger { get; set; }


		[ProtoAfterDeserialization]
        public void AfterDeserialization()
        {
            this.EndInit();
        }
	}
}
