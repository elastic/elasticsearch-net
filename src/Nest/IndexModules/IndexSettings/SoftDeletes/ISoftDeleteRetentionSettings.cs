// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ISoftDeleteRetentionSettings
	{
		/// <summary> How many operations should be retained </summary>
		long? Operations { get; set; }
	}

	public class SoftDeleteRetentionSettings : ISoftDeleteRetentionSettings
	{
		/// <inheritdoc see cref="ISoftDeleteRetentionSettings.Operations"/>
		public long? Operations { get; set; }
	}

	public class SoftDeleteRetentionSettingsDescriptor : DescriptorBase<SoftDeleteRetentionSettingsDescriptor, ISoftDeleteRetentionSettings>, ISoftDeleteRetentionSettings
	{
		long? ISoftDeleteRetentionSettings.Operations { get; set; }

		/// <inheritdoc see cref="ISoftDeleteRetentionSettings.Operations"/>
		public SoftDeleteRetentionSettingsDescriptor Operations(long? operations) => Assign(operations, (a, v) => a.Operations = v);
	}
}
