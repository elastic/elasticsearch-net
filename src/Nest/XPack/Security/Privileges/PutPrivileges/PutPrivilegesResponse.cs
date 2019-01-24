using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPutPrivilegesResponse : IResponse
	{
		IReadOnlyDictionary<string, IDictionary<string, PutPrivilegesStatus>> Applications { get; }
	}

	[JsonConverter(typeof(DictionaryResponseJsonConverter<PutPrivilegesResponse, string, IDictionary<string, PutPrivilegesStatus>>))]
	public class PutPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PutPrivilegesStatus>>, IPutPrivilegesResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<string, IDictionary<string, PutPrivilegesStatus>> Applications => Self.BackingDictionary;
	}

	public class PutPrivilegesStatus
	{
		[JsonProperty("created")]
		public bool Created { get; internal set; }
	}
}
