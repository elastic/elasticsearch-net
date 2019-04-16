using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class BinaryAttribute : ElasticsearchDocValuesPropertyAttributeBase, IBinaryProperty
	{
		public BinaryAttribute() : base(FieldType.Binary) { }
	}
}
