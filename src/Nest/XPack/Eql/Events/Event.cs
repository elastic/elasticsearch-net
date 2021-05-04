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
	public class Event<TEvent> where TEvent : class
	{
		/// <summary>
		/// The individual fields requested for a event.
		/// </summary>
		[DataMember(Name = "fields")]
		public FieldValues Fields { get; internal set; }

		/// <summary>
		/// The id of the event.
		/// </summary>
		[DataMember(Name = "_id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The index in which the event resides.
		/// </summary>
		[DataMember(Name = "_index")]
		public string Index { get; internal set; }

		/// <summary>
		/// The source document for the event.
		/// </summary>
		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		public TEvent Source { get; internal set; }
	}
}
