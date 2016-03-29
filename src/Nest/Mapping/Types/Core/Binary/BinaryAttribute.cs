using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BinaryAttribute : ElasticsearchPropertyAttributeBase, IBinaryProperty
	{
		public BinaryAttribute() : base("binary") { }
	}	
}
