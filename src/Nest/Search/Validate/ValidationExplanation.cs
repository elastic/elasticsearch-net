// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ValidationExplanation
	{
		[DataMember(Name ="error")]
		public string Error { get; internal set; }

		[DataMember(Name ="explanation")]
		public string Explanation { get; internal set; }

		[DataMember(Name ="index")]
		public string Index { get; internal set; }

		[DataMember(Name ="valid")]
		public bool Valid { get; internal set; }
	}
}
