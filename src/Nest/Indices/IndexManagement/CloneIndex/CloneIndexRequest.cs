// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A request to the clone index API
	/// </summary>
	[MapsApi("indices.clone")]
	[ReadAs(typeof(CloneIndexRequest))]
	public partial interface ICloneIndexRequest
	{
		/// <summary>
		/// The aliases to apply to the target index
		/// </summary>
		[DataMember(Name ="aliases")]
		IAliases Aliases { get; set; }

		/// <summary>
		/// The settings to apply to the target index
		/// </summary>
		[DataMember(Name ="settings")]
		IIndexSettings Settings { get; set; }
	}

	/// <inheritdoc cref="ICloneIndexRequest" />
	public partial class CloneIndexRequest
	{
		/// <inheritdoc />
		public IAliases Aliases { get; set; }

		/// <inheritdoc />
		public IIndexSettings Settings { get; set; }
	}

	/// <inheritdoc cref="ICloneIndexRequest" />
	public partial class CloneIndexDescriptor
	{
		IAliases ICloneIndexRequest.Aliases { get; set; }
		IIndexSettings ICloneIndexRequest.Settings { get; set; }

		/// <inheritdoc cref="ICloneIndexRequest.Settings"/>
		public CloneIndexDescriptor Settings(Func<IndexSettingsDescriptor, IPromise<IIndexSettings>> selector) =>
			Assign(selector, (a, v) => a.Settings = v?.Invoke(new IndexSettingsDescriptor())?.Value);

		/// <inheritdoc cref="ICloneIndexRequest.Aliases"/>
		public CloneIndexDescriptor Aliases(Func<AliasesDescriptor, IPromise<IAliases>> selector) =>
			Assign(selector, (a, v) => a.Aliases = v?.Invoke(new AliasesDescriptor())?.Value);
	}
}
