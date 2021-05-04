// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
