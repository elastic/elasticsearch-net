using System;
using Elasticsearch.Net;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(MultiGetOperationDescriptor<object>))]
	public interface IMultiGetOperation
	{
		bool CanBeFlattened { get; }

		Type ClrType { get; }

		[DataMember(Name ="_id")]
		Id Id { get; set; }

		[DataMember(Name ="_index")]
		IndexName Index { get; set; }

		[DataMember(Name ="routing")]
		string Routing { get; set; }

		[DataMember(Name ="_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name ="stored_fields")]
		Fields StoredFields { get; set; }

		[DataMember(Name ="version")]
		long? Version { get; set; }

		[DataMember(Name ="version_type")]
		VersionType? VersionType { get; set; }
	}
}
