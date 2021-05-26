using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET
{
	public class GamerComponent: Entity
	{
		[BsonElement]
		[BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
		public readonly Dictionary<long, Gamer> idGamers = new Dictionary<long, Gamer>();
		
		public int Count
		{
			get
			{
				return this.idGamers.Count;
			}
		}
	}
}