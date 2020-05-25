// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexedScriptTransform : IScriptTransform
	{
		[DataMember(Name ="id")]
		string Id { get; set; }
	}

	public class IndexedScriptTransform : ScriptTransformBase, IIndexedScriptTransform
	{
		public IndexedScriptTransform(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptTransformDescriptor
		: ScriptTransformDescriptorBase<IndexedScriptTransformDescriptor, IIndexedScriptTransform>, IIndexedScriptTransform
	{
		public IndexedScriptTransformDescriptor(string id) => Self.Id = id;

		public IndexedScriptTransformDescriptor() { }

		string IIndexedScriptTransform.Id { get; set; }
	}
}
