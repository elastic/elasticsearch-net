// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MultiGetOperationDescriptor<object>))]
	public interface IMultiGetOperation
	{
		bool CanBeFlattened { get; }

		Type ClrType { get; }

		[DataMember(Name = "_id")]
		Id Id { get; set; }

		[DataMember(Name = "_index")]
		IndexName Index { get; set; }

		[DataMember(Name = "routing")]
		string Routing { get; set; }

		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name = "stored_fields")]
		Fields StoredFields { get; set; }

		[DataMember(Name = "version")]
		long? Version { get; set; }

		[DataMember(Name = "version_type")]
		VersionType? VersionType { get; set; }
	}
}
