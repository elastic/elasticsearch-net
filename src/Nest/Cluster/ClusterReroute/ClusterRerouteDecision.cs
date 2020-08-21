// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterRerouteDecision
	{
		[DataMember(Name ="decider")]
		public string Decider { get; set; }

		[DataMember(Name ="decision")]
		public string Decision { get; set; }

		[DataMember(Name ="explanation")]
		public string Explanation { get; set; }
	}
}
