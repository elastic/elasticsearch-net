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
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	[DataContract]
	public class BulkIndexByScrollFailure
	{
		[DataMember(Name = "cause")]
		public Error Cause { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "index")]
		public string Index { get; set; }

		[DataMember(Name = "status")]
		public int Status { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}
}
