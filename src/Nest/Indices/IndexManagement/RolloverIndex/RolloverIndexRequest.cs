// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.rollover.json")]
	[ReadAs(typeof(RolloverIndexRequest))]
	public partial interface IRolloverIndexRequest : IIndexState
	{
		[DataMember(Name ="conditions")]
		IRolloverConditions Conditions { get; set; }
	}

	public partial class RolloverIndexRequest
	{
		public IAliases Aliases { get; set; }
		public IRolloverConditions Conditions { get; set; }

		public ITypeMapping Mappings { get; set; }

		public IIndexSettings Settings { get; set; }
	}

	public partial class RolloverIndexDescriptor
	{
		IAliases IIndexState.Aliases { get; set; }
		IRolloverConditions IRolloverIndexRequest.Conditions { get; set; }
		ITypeMapping IIndexState.Mappings { get; set; }
		IIndexSettings IIndexState.Settings { get; set; }

		public RolloverIndexDescriptor Conditions(Func<RolloverConditionsDescriptor, IRolloverConditions> selector) =>
			Assign(selector, (a, v) => a.Conditions = v?.Invoke(new RolloverConditionsDescriptor()));

		public RolloverIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public RolloverIndexDescriptor Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<T>()));

		public RolloverIndexDescriptor Map(Func<TypeMappingDescriptor<object>, ITypeMapping> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new TypeMappingDescriptor<object>()));

		[Obsolete("Mappings is no longer a dictionary in 7.x, please use the simplified Map() method on this descriptor instead")]
		public RolloverIndexDescriptor Mappings(Func<MappingsDescriptor, ITypeMapping> selector) =>
			Assign(selector, (a, v) => a.Mappings = v?.Invoke(new MappingsDescriptor()));

		public RolloverIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
