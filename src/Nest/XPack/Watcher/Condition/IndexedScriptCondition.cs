// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IIndexedScriptCondition : IScriptCondition
	{
		[DataMember(Name = "id")]
		string Id { get; set; }
	}

	public class IndexedScriptCondition : ScriptConditionBase, IIndexedScriptCondition
	{
		public IndexedScriptCondition(string id) => Id = id;

		public string Id { get; set; }
	}

	public class IndexedScriptConditionDescriptor
		: ScriptConditionDescriptorBase<IndexedScriptConditionDescriptor, IIndexedScriptCondition>, IIndexedScriptCondition
	{
		public IndexedScriptConditionDescriptor(string id) => Self.Id = id;

		string IIndexedScriptCondition.Id { get; set; }
	}
}
