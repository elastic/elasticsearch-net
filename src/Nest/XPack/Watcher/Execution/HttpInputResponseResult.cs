// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class HttpInputResponseResult
	{
		[DataMember(Name ="body")]
		public string Body { get; set; }

		[DataMember(Name ="headers")]
		public IDictionary<string, string[]> Headers { get; set; }

		[DataMember(Name ="status")]
		public int StatusCode { get; set; }
	}
}
