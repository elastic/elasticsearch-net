// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// 
	/// </summary>
	public class ElasticsearchVersionInfo
	{
		[DataMember(Name ="lucene_version")]
		public string LuceneVersion { get; set; }

		[DataMember(Name ="number")]
		public string Number { get; set; }
		
		[DataMember(Name ="build_flavor")]
		public string BuildFlavor { get; set; }
		
		[DataMember(Name ="build_type")]
		public string BuildType { get; set; }
		
		[DataMember(Name ="build_hash")]
		public string BuildHash { get; set; }
		
		[DataMember(Name ="build_date")]
		public DateTimeOffset BuildDate { get; set; }
		
		[DataMember(Name ="build_snapshot")]
		public bool BuildSnapshot { get; set; }
		
		[DataMember(Name ="minimum_wire_compatibility_version")]
		public string MinimumWireCompatibilityVersion { get; set; }
		
		[DataMember(Name ="minimum_index_compatibility_version")]
		public string MinimumIndexCompatibilityVersion { get; set; }
	}
	
}
