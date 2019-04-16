using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IAuthenticateResponse : IResponse
	{
		[DataMember(Name ="email")]
		string Email { get; }

		[DataMember(Name ="full_name")]
		string FullName { get; }

		[DataMember(Name ="metadata")]
		IReadOnlyDictionary<string, object> Metadata { get; }

		[DataMember(Name ="roles")]
		IReadOnlyCollection<string> Roles { get; }

		[DataMember(Name ="username")]
		string Username { get; }
	}

	public class AuthenticateResponse : ResponseBase, IAuthenticateResponse
	{
		public string Email { get; internal set; }

		public string FullName { get; internal set; }

		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		public IReadOnlyCollection<string> Roles { get; internal set; } = EmptyReadOnly<string>.Collection;
		public string Username { get; internal set; }
	}
}
