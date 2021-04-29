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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Metadata about a hit matching a query
	/// </summary>
	/// <typeparam name="TEvent">The type of the source document</typeparam>
	[InterfaceDataContract]
	[ReadAs(typeof(Event<>))]
	public interface IEvent<out TEvent> where TEvent : class
	{
		/// <summary>
		/// The individual fields requested for a event.
		/// </summary>
		[DataMember(Name = "fields")]
		FieldValues Fields { get; }

		/// <summary>
		/// The id of the hit
		/// </summary>
		[DataMember(Name = "_id")]
		string Id { get; }

		/// <summary>
		/// The index in which the hit resides
		/// </summary>
		[DataMember(Name = "_index")]
		string Index { get; }
		
		/// <summary>
		/// The source document for the hit
		/// </summary>
		[DataMember(Name = "_source")]
		[JsonFormatter(typeof(SourceFormatter<>))]
		TEvent Source { get; }
	}

	public class Event<TEvent> : IEvent<TEvent>
		where TEvent : class
	{
		public FieldValues Fields { get; internal set; }
		public string Id { get; internal set; }
		public string Index { get; internal set; }
		public TEvent Source { get; internal set; }
	}
}
