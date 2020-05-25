// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexedScript : IScript
	{
		[DataMember(Name ="id")]
		string Id { get; set; }
	}

	public class IndexedScript : ScriptBase, IIndexedScript
	{
		public IndexedScript(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptDescriptor
		: ScriptDescriptorBase<IndexedScriptDescriptor, IIndexedScript>, IIndexedScript
	{
		public IndexedScriptDescriptor() { }

		public IndexedScriptDescriptor(string id) => Self.Id = id;

		string IIndexedScript.Id { get; set; }

		public IndexedScriptDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);
	}
}
