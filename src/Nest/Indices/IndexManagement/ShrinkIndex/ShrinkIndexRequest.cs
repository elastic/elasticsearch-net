// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("indices.shrink.json")]
	[ReadAs(typeof(ShrinkIndexRequest))]
	public partial interface IShrinkIndexRequest
	{
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexRequest
	{
		public IAliases Aliases { get; set; }

		public IIndexSettings Settings { get; set; }
	}

	public partial class ShrinkIndexDescriptor
	{
		IAliases IShrinkIndexRequest.Aliases { get; set; }
		IIndexSettings IShrinkIndexRequest.Settings { get; set; }

		public ShrinkIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		public ShrinkIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
