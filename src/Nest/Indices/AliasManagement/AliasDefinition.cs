// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class AliasDefinition
	{
		[DataMember(Name ="filter")]
		public IQueryContainer Filter { get; internal set; }

		[DataMember(Name ="index_routing")]
		public string IndexRouting { get; internal set; }

		[DataMember(Name ="is_write_index")]
		public bool? IsWriteIndex { get; internal set; }

		[DataMember(Name ="routing")]
		public string Routing { get; internal set; }

		[DataMember(Name ="search_routing")]
		public string SearchRouting { get; internal set; }
	}
}
