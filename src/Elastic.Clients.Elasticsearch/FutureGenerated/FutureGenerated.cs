// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	// TODO - Generate more of these?
	public partial class TermQuery
	{
		public static implicit operator QueryContainer(TermQuery termQuery) => QueryContainer.Term(termQuery);
	}

	public partial class MatchAllQuery
	{
		public static implicit operator QueryContainer(MatchAllQuery matchAllQuery) => QueryContainer.MatchAll(matchAllQuery);
	}

	public partial class QueryContainer
	{
		// TODO - Generate more of these!
		public TermQuery? GetTerm => Variant as TermQuery;
	}
}

namespace Elastic.Clients.Elasticsearch
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	public partial class HttpHeaders : Dictionary<string, Union<string, IReadOnlyCollection<string>>>
	{
	}

	public partial class Metadata : Dictionary<string, object>
	{
	}

	//public partial class RuntimeFields : Dictionary<Field, RuntimeField>
	//{
	//}

	public partial class ApplicationsPrivileges : Dictionary<Name, ResourcePrivileges>
	{
	}

	public partial class Privileges : Dictionary<string, bool>
	{
	}

	public partial class ResourcePrivileges : Dictionary<Name, Privileges>
	{
	}

	// TODO: Implement properly
	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
	}
}
