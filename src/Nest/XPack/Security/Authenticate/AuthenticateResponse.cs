using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class RealmInfo
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}

	public class AuthenticateResponse : ResponseBase
	{
		[DataMember(Name = "email")]
		public string Email { get; internal set; }

		[DataMember(Name = "full_name")]
		public string FullName { get; internal set; }

		[DataMember(Name = "metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; }
			= EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name = "roles")]
		public IReadOnlyCollection<string> Roles { get; internal set; }
			= EmptyReadOnly<string>.Collection;

		[DataMember(Name = "username")]
		public string Username { get; internal set; }

		[DataMember(Name = "authentication_realm")]
		public RealmInfo AuthenticationRealm { get; internal set; }

		[DataMember(Name = "lookup_realm")]
		public RealmInfo LookupRealm { get; internal set; }
	}
}
