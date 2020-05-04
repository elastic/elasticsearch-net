// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum DFRAfterEffect
	{
		/// <summary>
		/// Implementation used when there is no aftereffect.
		/// </summary>
		[EnumMember(Value = "no")]
		No,

		/// <summary>
		/// Model of the information gain based on the ratio of two Bernoulli processes.
		/// </summary>
		[EnumMember(Value = "b")]
		B,

		/// <summary>
		/// Model of the information gain based on Laplace's law of succession.
		/// </summary>
		[EnumMember(Value = "l")]
		L,
	}
}
