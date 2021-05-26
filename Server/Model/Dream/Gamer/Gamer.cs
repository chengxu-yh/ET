using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    /// <summary>
	/// Gamer是生存在Map服上的玩家实例
	/// </summary>
    [BsonIgnoreExtraElements]
    public sealed class Gamer: Entity
    {
        public string Namer { get; set; }


    }

}