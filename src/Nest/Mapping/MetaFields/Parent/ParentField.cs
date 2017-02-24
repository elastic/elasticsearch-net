using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ParentField>))]
	public interface IParentField : IFieldMapping
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

    public class ParentField : IParentField
    {
		public TypeName Type { get; set; }
    }
}
