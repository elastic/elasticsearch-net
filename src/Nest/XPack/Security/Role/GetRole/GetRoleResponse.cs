using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IGetRoleResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackRole> Roles { get; }
	}

	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetRoleResponse, string, XPackRole>))]
	public class GetRoleResponse : DictionaryResponseBase<string, XPackRole>, IGetRoleResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackRole> Roles => Self.BackingDictionary;
	}
}
