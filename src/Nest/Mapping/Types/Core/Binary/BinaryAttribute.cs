using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BinaryAttribute : ElasticsearchDocValuesPropertyAttributeBase, IBinaryProperty
	{
		public BinaryAttribute() : base(FieldType.Binary) { }
	}
}
