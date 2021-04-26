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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	internal class BulkUpdateBody<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		[DataMember(Name ="doc_as_upsert")]
		public bool? DocAsUpsert { get; set; }

		[DataMember(Name ="doc")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TPartialUpdate PartialUpdate { get; set; }

		[DataMember(Name ="script")]
		internal IScript Script { get; set; }

		[DataMember(Name ="scripted_upsert")]
		internal bool? ScriptedUpsert { get; set; }

		[DataMember(Name ="upsert")]
		[JsonFormatter(typeof(CollapsedSourceFormatter<>))]
		internal TDocument Upsert { get; set; }

		[DataMember(Name = "if_seq_no")]
		internal long? IfSequenceNumber { get; set; }

		[DataMember(Name = "if_primary_term")]
		internal long? IfPrimaryTerm { get; set; }

		[DataMember(Name = "_source")]
		internal Union<bool, ISourceFilter> Source { get; set; }
	}
}
