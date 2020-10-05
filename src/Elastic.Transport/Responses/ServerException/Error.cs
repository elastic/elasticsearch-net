// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Elastic.Transport
{
	[DataContract]
	public class Error : ErrorCause
	{
		private static readonly IReadOnlyDictionary<string, string> DefaultHeaders =
			new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(0));

		[DataMember(Name = "headers")]
		[JsonPropertyName("headers")]
		public IReadOnlyDictionary<string, string> Headers { get; set; } = DefaultHeaders;

		[DataMember(Name = "root_cause")]
		[JsonPropertyName("root_cause")]
		public IReadOnlyCollection<ErrorCause> RootCause { get; set; }

		public override string ToString() => CausedBy == null
			? $"Type: {Type} Reason: \"{Reason}\""
			: $"Type: {Type} Reason: \"{Reason}\" CausedBy: \"{CausedBy}\"";
	}
}
