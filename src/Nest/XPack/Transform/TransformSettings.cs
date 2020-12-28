// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Transform settings
	/// <para />
	/// Valid in Elasticsearch 7.8.0+
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformSettings))]
	public interface ITransformSettings
	{
		[DataMember(Name = "docs_per_second")]
		public float? DocsPerSecond { get; set; }

		[DataMember(Name = "max_page_search_size")]
		public int? MaxPageSearchSize { get; set; }

		[DataMember(Name = "dates_as_epoch_millis")]
		public bool? DatesAsEpochMilliseconds { get; set; }
	}

	/// <inheritdoc />
	public class TransformSettings : ITransformSettings
	{
		/// <inheritdoc />
		public float? DocsPerSecond { get; set; }

		/// <inheritdoc />
		public int? MaxPageSearchSize { get; set; }

		/// <inheritdoc />
		public bool? DatesAsEpochMilliseconds { get; set; }
	}

	public class TransformSettingsDescriptor : DescriptorBase<TransformSettingsDescriptor, ITransformSettings>, ITransformSettings
	{
		float? ITransformSettings.DocsPerSecond { get; set; }
		int? ITransformSettings.MaxPageSearchSize { get; set; }
		bool? ITransformSettings.DatesAsEpochMilliseconds { get; set; }

		/// <inheritdoc cref="ITransformSettings.DocsPerSecond"/>
		public TransformSettingsDescriptor DocsPerSecond(float? docsPerSecond) =>
			Assign(docsPerSecond, (a, v) => a.DocsPerSecond = v);

		/// <inheritdoc cref="ITransformSettings.MaxPageSearchSize"/>
		public TransformSettingsDescriptor MaxPageSearchSize(int? maxPageSearchSize) =>
			Assign(maxPageSearchSize, (a, v) => a.MaxPageSearchSize = v);

		/// <inheritdoc cref="ITransformSettings.DatesAsEpochMilliseconds"/>
		public TransformSettingsDescriptor DatesAsEpochMilliseconds(bool? datesAsEpochMillis = true) =>
			Assign(datesAsEpochMillis, (a, v) => a.DatesAsEpochMilliseconds = v);
	}
}
