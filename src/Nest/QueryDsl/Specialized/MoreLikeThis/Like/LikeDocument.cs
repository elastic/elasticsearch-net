// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An indexed or artificial document with which to find likeness
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(LikeDocument<object>))]
	public interface ILikeDocument
	{
		/// <summary>
		/// A document to find other documents like
		/// </summary>
		[DataMember(Name = "doc")]
		[JsonFormatter(typeof(SourceFormatter<object>))]
		object Document { get; set; }

		/// <summary>
		/// The fields to use for likeness
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// The id of an indexed document to find other documents like
		/// </summary>
		[DataMember(Name = "_id")]
		Id Id { get; set; }

		/// <summary>
		/// The index of an indexed document to find other documents like
		/// </summary>
		[DataMember(Name = "_index")]
		IndexName Index { get; set; }

		/// <summary>
		/// A different analyzer than the one defined for the target fields
		/// </summary>
		[DataMember(Name = "per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		/// <summary>
		/// The routing value of an indexed document to find other documents like
		/// </summary>
		[DataMember(Name = "routing")]
		Routing Routing { get; set; }
	}

	/// <inheritdoc />
	public abstract class LikeDocumentBase : ILikeDocument
	{
		/// <inheritdoc />
		public object Document { get; set; }

		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public Id Id { get; set; }

		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		/// <inheritdoc />
		public Routing Routing { get; set; }
	}

	/// <inheritdoc cref="ILikeDocument" />
	public class LikeDocument<TDocument> : LikeDocumentBase
		where TDocument : class
	{
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			Id = id;
			Index = typeof(TDocument);
		}

		public LikeDocument(TDocument document)
		{
			Document = document;
			Index = typeof(TDocument);
		}
	}

	/// <inheritdoc cref="ILikeDocument" />
	public class LikeDocumentDescriptor<TDocument> : DescriptorBase<LikeDocumentDescriptor<TDocument>, ILikeDocument>, ILikeDocument
		where TDocument : class
	{
		public LikeDocumentDescriptor() => Self.Index = typeof(TDocument);

		object ILikeDocument.Document { get; set; }
		Fields ILikeDocument.Fields { get; set; }
		Id ILikeDocument.Id { get; set; }
		IndexName ILikeDocument.Index { get; set; }
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }
		Routing ILikeDocument.Routing { get; set; }

		/// <inheritdoc cref="ILikeDocument.Index" />
		public LikeDocumentDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="ILikeDocument.Id" />
		public LikeDocumentDescriptor<TDocument> Id(Id id) => Assign(id, (a, v) => a.Id = v);

		/// <inheritdoc cref="ILikeDocument.Routing" />
		public LikeDocumentDescriptor<TDocument> Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		/// <inheritdoc cref="ILikeDocument.Fields" />
		public LikeDocumentDescriptor<TDocument> Fields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		/// <inheritdoc cref="ILikeDocument.Fields" />
		public LikeDocumentDescriptor<TDocument> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="ILikeDocument.Document" />
		public LikeDocumentDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.Document = v);

		/// <inheritdoc cref="ILikeDocument.PerFieldAnalyzer" />
		public LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(
			Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector
		) =>
			Assign(analyzerSelector, (a, v) => a.PerFieldAnalyzer = v?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);
	}
}
