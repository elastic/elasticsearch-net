using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BinaryAttribute : ElasticsearchDocValuesPropertyAttributeBase, IBinaryProperty
	{
		public BinaryAttribute() : base(FieldType.Binary) { }
	}
}
