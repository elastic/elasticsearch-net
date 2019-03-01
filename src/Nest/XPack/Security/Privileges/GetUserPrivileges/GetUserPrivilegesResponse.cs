using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetUserPrivilegesResponse : IResponse
	{
		[JsonProperty("cluster")]
		IReadOnlyCollection<string> Cluster { get; }

		[JsonProperty("global")]
		IReadOnlyCollection<GlobalPrivileges> Global { get; }

		[JsonProperty("indices")]
		IReadOnlyCollection<UserIndicesPrivileges> Indices { get; }

		[JsonProperty("applications")]
		IReadOnlyCollection<ApplicationResourcePrivileges> Applications { get; }

		[JsonProperty("run_as")]
		IReadOnlyCollection<string> RunAs { get; }
	}

	public class ManageUserPrivileges
	{
		[JsonProperty("applications")]
		public IReadOnlyCollection<string> Applications { get; internal set; }
	}

	public class ApplicationGlobalUserPrivileges
	{
		[JsonProperty("manage")]
		public ManageUserPrivileges Manage { get; internal set; }
	}

	public class GlobalPrivileges
	{
		[JsonProperty("application")]
		public ApplicationGlobalUserPrivileges Application { get; internal set; }
	}

	public class FieldSecuritySettings
	{
		[JsonProperty("grant")]
		public IReadOnlyCollection<string> Grant { get; internal set; }

		[JsonProperty("except")]
		public IReadOnlyCollection<string> Except { get; internal set; }
	}

	public class TermUserPrivileges
	{
		[JsonProperty("apps")]
		public bool Apps { get; internal set; }
	}

	public class QueryUserPrivileges
	{
		[JsonProperty("term")]
		public TermUserPrivileges Term { get; internal set; }
	}

	public class UserIndicesPrivileges
	{
		[JsonProperty("names")]
		public IReadOnlyCollection<string> Names { get; internal set; }

		[JsonProperty("privileges")]
		public IReadOnlyCollection<string> Privileges { get; internal set; }

		[JsonProperty("field_security")]
		public FieldSecuritySettings FieldSecurity { get; internal set; }

		[JsonProperty("query")]
		public QueryUserPrivileges Query { get; internal set; }
	}

	public class ApplicationResourcePrivileges
	{
		[JsonProperty("application")]
		public string Application { get; internal set; }

		[JsonProperty("privileges")]
		public IReadOnlyCollection<string> Privileges { get; internal set; }

		[JsonProperty("resources")]
		public IReadOnlyCollection<string> Resources { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetUserPrivilegesResponse : ResponseBase, IGetUserPrivilegesResponse
	{
		[JsonProperty("cluster")]
		public IReadOnlyCollection<string> Cluster { get; internal set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("global")]
		public IReadOnlyCollection<GlobalPrivileges> Global { get; internal set; } = EmptyReadOnly<GlobalPrivileges>.Collection;

		[JsonProperty("indices")]
		public IReadOnlyCollection<UserIndicesPrivileges> Indices { get; internal set; } = EmptyReadOnly<UserIndicesPrivileges>.Collection;

		[JsonProperty("applications")]
		public IReadOnlyCollection<ApplicationResourcePrivileges> Applications { get; internal set; } = EmptyReadOnly<ApplicationResourcePrivileges>.Collection;

		[JsonProperty("run_as")]
		public IReadOnlyCollection<string> RunAs { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
