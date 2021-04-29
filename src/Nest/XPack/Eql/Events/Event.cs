
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest.XPack.Eql.Events
{
	/// <summary>
	/// Metadata about a hit matching a query
	/// </summary>
	/// <typeparam name="TEvent">The type of the source document</typeparam>
	[InterfaceDataContract]
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

		/// <summary>
		/// The type of hit
		/// </summary>
		[DataMember(Name = "_type")]
		string Type { get; }

		/// <summary>
		/// The version of the hit
		/// </summary>
		[DataMember(Name = "_version")]
		long Version { get; }
	}

	public class Event
	{
	}
}
