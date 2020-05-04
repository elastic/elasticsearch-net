// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IQueriesCacheSettings
	{
		/// <summary>
		/// Whether the query cache is enabled. <c>True</c> by default.
		/// </summary>
		bool? Enabled { get; set; }
	}

	public class QueriesCacheSettings : IQueriesCacheSettings
	{
		public bool? Enabled { get; set; }
	}

	public class QueriesCacheSettingsDescriptor : DescriptorBase<QueriesCacheSettingsDescriptor, IQueriesCacheSettings>, IQueriesCacheSettings
	{
		bool? IQueriesCacheSettings.Enabled { get; set; }

		/// <inheritdoc />
		public QueriesCacheSettingsDescriptor Enabled(bool? enabled = true) =>
			Assign(enabled, (a, v) => a.Enabled = v);
	}
}
