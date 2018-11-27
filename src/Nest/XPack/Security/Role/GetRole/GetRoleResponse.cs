using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetRoleResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackRole> Roles { get; }
	}

	[DataContract]
	[JsonConverter(typeof(DictionaryResponseJsonConverter<GetRoleResponse, string, XPackRole>))]
	public class GetRoleResponse : DictionaryResponseBase<string, XPackRole>, IGetRoleResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackRole> Roles => Self.BackingDictionary;
	}
}
