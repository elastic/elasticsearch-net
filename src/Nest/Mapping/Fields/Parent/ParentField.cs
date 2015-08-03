using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IParentField : ISpecialField
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

    public class ParentField : IParentField
    {
		public TypeName Type { get; set; }
    }
}
