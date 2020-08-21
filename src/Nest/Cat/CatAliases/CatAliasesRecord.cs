// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatAliasesRecord : ICatRecord
	{
		[DataMember(Name ="alias")]
		public string Alias { get; set; }

		[DataMember(Name ="filter")]
		public string Filter { get; set; }

		[DataMember(Name ="index")]
		public string Index { get; set; }

		[DataMember(Name ="indexRouting")]
		public string IndexRouting { get; set; }

		[DataMember(Name ="searchRouting")]
		public string SearchRouting { get; set; }
	}
}
