// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class Retries
	{
		[DataMember(Name ="bulk")]
		public long Bulk { get; internal set; }

		[DataMember(Name ="search")]
		public long Search { get; internal set; }
	}
}
