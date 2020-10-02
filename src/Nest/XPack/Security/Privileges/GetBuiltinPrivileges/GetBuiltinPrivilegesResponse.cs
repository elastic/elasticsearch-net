// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class GetBuiltinPrivilegesResponse : ResponseBase
	{
		[DataMember(Name = "cluster")]
		public IReadOnlyCollection<string> Cluster { get; internal set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name = "index")]
		public IReadOnlyCollection<string> Index { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
