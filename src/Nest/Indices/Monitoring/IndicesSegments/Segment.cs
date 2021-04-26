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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class Segment
	{
		[DataMember(Name ="attributes")]
		public IReadOnlyDictionary<string, string> Attributes { get; internal set; } =
			EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name ="committed")]
		public bool Committed { get; internal set; }

		[DataMember(Name ="compound")]
		public bool Compound { get; internal set; }

		[DataMember(Name ="deleted_docs")]
		public long DeletedDocuments { get; internal set; }

		[DataMember(Name ="generation")]
		public int Generation { get; internal set; }

		[DataMember(Name ="memory_in_bytes")]
		public double MemoryInBytes { get; internal set; }

		[DataMember(Name ="search")]
		public bool Search { get; internal set; }

		[DataMember(Name ="size_in_bytes")]
		public double SizeInBytes { get; internal set; }

		[DataMember(Name ="num_docs")]
		public long TotalDocuments { get; internal set; }

		[DataMember(Name ="version")]
		public string Version { get; internal set; }
	}
}
