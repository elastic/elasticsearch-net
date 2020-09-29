// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBulkOperation
	{
		Type ClrType { get; }

		[DataMember(Name = "_id")]
		Id Id { get; set; }

		[DataMember(Name = "_index")]
		IndexName Index { get; set; }

		string Operation { get; }

		[DataMember(Name = "retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		[DataMember(Name = "routing")]
		Routing Routing { get; set; }

		[DataMember(Name = "version")]
		long? Version { get; set; }

		[DataMember(Name = "version_type")]
		VersionType? VersionType { get; set; }

		object GetBody();

		Id GetIdForOperation(Inferrer inferrer);

		Routing GetRoutingForOperation(Inferrer settingsInferrer);
	}
}
