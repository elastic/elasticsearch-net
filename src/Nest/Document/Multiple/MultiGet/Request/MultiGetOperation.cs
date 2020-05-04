// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;

namespace Nest
{
	public class MultiGetOperation<T> : IMultiGetOperation
	{
		public MultiGetOperation(Id id)
		{
			Id = id;
			Index = typeof(T);
		}


		public object Document { get; set; }

		public Id Id { get; set; }

		public IndexName Index { get; set; }

		public string Routing { get; set; }

		public Union<bool, ISourceFilter> Source { get; set; }

		public Fields StoredFields { get; set; }

		public long? Version { get; set; }

		public VersionType? VersionType { get; set; }

		bool IMultiGetOperation.CanBeFlattened =>
			Index == null
			&& Routing == null
			&& Source == null
			&& StoredFields == null;

		Type IMultiGetOperation.ClrType => typeof(T);
	}

	public class MultiGetOperationDescriptor<T> : DescriptorBase<MultiGetOperationDescriptor<T>, IMultiGetOperation>, IMultiGetOperation
		where T : class
	{
		public MultiGetOperationDescriptor() => Self.Index = Self.ClrType;

		/// <summary>
		/// when rest.action.multi.allow_explicit_index is set to false you can use this constructor to generate a multiget operation
		/// with no index and type set
		/// <pre>
		/// See also: https://github.com/elastic/elasticsearch/issues/3636
		/// </pre>
		/// </summary>
		public MultiGetOperationDescriptor(bool allowExplicitIndex)
			: this()
		{
			if (allowExplicitIndex) return;

			Self.Index = null;
		}

		bool IMultiGetOperation.CanBeFlattened =>
			Self.Index == null
			&& Self.Routing == null
			&& Self.Source == null
			&& Self.StoredFields == null;

		Type IMultiGetOperation.ClrType => typeof(T);
		Id IMultiGetOperation.Id { get; set; }
		IndexName IMultiGetOperation.Index { get; set; }
		string IMultiGetOperation.Routing { get; set; }
		Union<bool, ISourceFilter> IMultiGetOperation.Source { get; set; }
		Fields IMultiGetOperation.StoredFields { get; set; }
		long? IMultiGetOperation.Version { get; set; }
		VersionType? IMultiGetOperation.VersionType { get; set; }

		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public MultiGetOperationDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public MultiGetOperationDescriptor<T> Id(Id id) => Assign(id, (a, v) => a.Id = v);

		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(bool? sourceEnabled = true) => Assign(sourceEnabled, (a, v) => a.Source = v);

		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> source) =>
			Assign(new Union<bool, ISourceFilter>(source(new SourceFilterDescriptor<T>())), (a, v) => a.Source = v);

		/// <summary>
		/// Set the routing for the get operation
		/// </summary>
		public MultiGetOperationDescriptor<T> Routing(string routing) => Assign(routing, (a, v) => a.Routing = v);

		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public MultiGetOperationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MultiGetOperationDescriptor<T> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);

		public MultiGetOperationDescriptor<T> Version(long? version) => Assign(version, (a, v) => a.Version = v);

		public MultiGetOperationDescriptor<T> VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a.VersionType = v);
	}
}
