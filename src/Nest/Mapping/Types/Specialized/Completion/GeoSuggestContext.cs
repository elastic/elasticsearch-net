// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGeoSuggestContext : ISuggestContext
	{
		[IgnoreDataMember]
		[Obsolete("No longer valid. Will be removed in next major release")]
		bool? Neighbors { get; set; }

		/// <summary>
		/// The precision of the geohash to encode the query geo point.
		/// Only the first value will be serialized.
		/// </summary>
		[JsonFormatter(typeof(SerializeAsSingleFormatter<string>))]
		[DataMember(Name = "precision")]
		IEnumerable<string> Precision { get; set; }
	}

	[DataContract]
	public class GeoSuggestContext : SuggestContextBase, IGeoSuggestContext
	{
		[Obsolete("No longer valid. Will be removed in next major release")]
		public bool? Neighbors { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Precision { get; set; }
		public override string Type => "geo";
	}

	[DataContract]
	public class GeoSuggestContextDescriptor<T>
		: SuggestContextDescriptorBase<GeoSuggestContextDescriptor<T>, IGeoSuggestContext, T>, IGeoSuggestContext
		where T : class
	{
		protected override string Type => "geo";

		[Obsolete("No longer valid. Will be removed in next major release")]
		bool? IGeoSuggestContext.Neighbors { get; set; }
		IEnumerable<string> IGeoSuggestContext.Precision { get; set; }

		/// <inheritdoc cref="IGeoSuggestContext.Precision" />
		public GeoSuggestContextDescriptor<T> Precision(params string[] precisions) => Assign(precisions, (a, v) => a.Precision = v);

		[Obsolete("No longer valid. Will be removed in next major release")]
		public GeoSuggestContextDescriptor<T> Neighbors(bool? neighbors = true) => Assign(neighbors, (a, v) => a.Neighbors = v);
	}
}
