using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BinaryAttribute : ElasticsearchPropertyAttribute, IBinaryProperty
	{
		public BinaryAttribute() : base("binary") { }
	}	
}
