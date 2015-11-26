using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BinaryAttribute : ElasticsearchPropertyAttribute, IBinaryProperty
	{
		public BinaryAttribute() : base("binary") { }
	}	
}
