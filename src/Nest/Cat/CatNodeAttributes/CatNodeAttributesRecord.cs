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

namespace Nest
{
	[DataContract]
	public class CatNodeAttributesRecord : ICatRecord
	{
		[DataMember(Name ="attr")]
		public string Attribute { get; set; }

		[DataMember(Name ="host")]
		public string Host { get; set; }

		// duration indices successful_shards failed_shards total_shards
		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		[DataMember(Name ="node")]
		public string Node { get; set; }

		[DataMember(Name ="port")]
		public long Port { get; set; }

		[DataMember(Name ="pid")]
		public long ProcessId { get; set; }

		[DataMember(Name ="value")]
		public string Value { get; set; }
	}
}
