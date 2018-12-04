using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IGetUserResponse : IResponse
	{
		IReadOnlyDictionary<string, XPackUser> Users { get; }
	}

	[DataContract]
	[JsonFormatter(typeof(DictionaryResponseFormatter<GetUserResponse, string, XPackUser>))]
	public class GetUserResponse : DictionaryResponseBase<string, XPackUser>, IGetUserResponse
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, XPackUser> Users => Self.BackingDictionary;
	}

	public class XPackUser
	{
		[DataMember(Name ="email")]
		public string Email { get; internal set; }

		[DataMember(Name ="full_name")]
		public string FullName { get; internal set; }

		[DataMember(Name ="metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name ="roles")]
		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name ="username")]
		public string Username { get; internal set; }
	}
}
