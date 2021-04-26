/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
