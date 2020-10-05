// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteResponse : ResponseBase
	{
		[DataMember(Name ="explanations")]
		public IReadOnlyCollection<ClusterRerouteExplanation> Explanations { get; internal set; } =
			EmptyReadOnly<ClusterRerouteExplanation>.Collection;

		[DataMember(Name ="state")]
		public DynamicDictionary State { get; internal set; }
	}
}
