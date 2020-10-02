// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class GetUserPrivilegesResponse : ResponseBase
	{
		[DataMember(Name = "applications")]
		public IReadOnlyCollection<ApplicationResourcePrivileges> Applications { get; internal set; } =
			EmptyReadOnly<ApplicationResourcePrivileges>.Collection;

		[DataMember(Name = "cluster")]
		public IReadOnlyCollection<string> Cluster { get; internal set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name = "global")]
		public IReadOnlyCollection<GlobalPrivileges> Global { get; internal set; } = EmptyReadOnly<GlobalPrivileges>.Collection;

		[DataMember(Name = "indices")]
		public IReadOnlyCollection<UserIndicesPrivileges> Indices { get; internal set; } = EmptyReadOnly<UserIndicesPrivileges>.Collection;

		[DataMember(Name = "run_as")]
		public IReadOnlyCollection<string> RunAs { get; internal set; } = EmptyReadOnly<string>.Collection;
	}

	public class ManageUserPrivileges
	{
		[DataMember(Name = "applications")]
		public IReadOnlyCollection<string> Applications { get; internal set; }
	}

	public class ApplicationGlobalUserPrivileges
	{
		[DataMember(Name = "manage")]
		public ManageUserPrivileges Manage { get; internal set; }
	}

	public class GlobalPrivileges
	{
		[DataMember(Name = "application")]
		public ApplicationGlobalUserPrivileges Application { get; internal set; }
	}

	public class FieldSecuritySettings
	{
		[DataMember(Name = "except")]
		public IReadOnlyCollection<string> Except { get; internal set; }

		[DataMember(Name = "grant")]
		public IReadOnlyCollection<string> Grant { get; internal set; }
	}

	public class TermUserPrivileges
	{
		[DataMember(Name = "apps")]
		public bool Apps { get; internal set; }
	}

	public class QueryUserPrivileges
	{
		[DataMember(Name = "term")]
		public TermUserPrivileges Term { get; internal set; }
	}

	public class UserIndicesPrivileges
	{
		[DataMember(Name = "field_security")]
		public FieldSecuritySettings FieldSecurity { get; internal set; }

		[DataMember(Name = "names")]
		public IReadOnlyCollection<string> Names { get; internal set; }

		[DataMember(Name = "privileges")]
		public IReadOnlyCollection<string> Privileges { get; internal set; }

		[DataMember(Name = "query")]
		public QueryUserPrivileges Query { get; internal set; }
	}

	public class ApplicationResourcePrivileges
	{
		[DataMember(Name = "application")]
		public string Application { get; internal set; }

		[DataMember(Name = "privileges")]
		public IReadOnlyCollection<string> Privileges { get; internal set; }

		[DataMember(Name = "resources")]
		public IReadOnlyCollection<string> Resources { get; internal set; }
	}

}
