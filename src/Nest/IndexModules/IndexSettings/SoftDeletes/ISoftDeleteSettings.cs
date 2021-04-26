/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;

namespace Nest
{
	public interface ISoftDeleteSettings
	{
		[Obsolete("Creating indices with soft-deletes disabled is deprecated and will be removed in future Elasticsearch versions. Do not set a value of 'false'")]
		/// <summary> Enables soft deletes on the index</summary>
		bool? Enabled { get; set; }
		/// <summary> Configure the retention of soft deletes on the index</summary>
		ISoftDeleteRetentionSettings Retention { get; set; }
	}

	public class SoftDeleteSettings : ISoftDeleteSettings
	{
		/// <inheritdoc see cref="ISoftDeleteSettings.Retention"/>
		public ISoftDeleteRetentionSettings Retention { get; set; }

		[Obsolete("Creating indices with soft-deletes disabled is deprecated and will be removed in future Elasticsearch versions. Do not set a value of 'false'")]
		/// <inheritdoc see cref="ISoftDeleteSettings.Enabled"/>
		public bool? Enabled { get; set; }
	}

	public class SoftDeleteSettingsDescriptor : DescriptorBase<SoftDeleteSettingsDescriptor, ISoftDeleteSettings>, ISoftDeleteSettings
	{
		bool? ISoftDeleteSettings.Enabled { get; set; }
		ISoftDeleteRetentionSettings ISoftDeleteSettings.Retention { get; set; }

		/// <inheritdoc see cref="ISoftDeleteSettings.Retention"/>
		public SoftDeleteSettingsDescriptor Retention(Func<SoftDeleteRetentionSettingsDescriptor, ISoftDeleteRetentionSettings> selector) =>
			Assign(selector.Invoke(new SoftDeleteRetentionSettingsDescriptor()), (a, v) => a.Retention = v);

		[Obsolete("Creating indices with soft-deletes disabled is deprecated and will be removed in future Elasticsearch versions. Do not set a value of 'false'")]
		/// <inheritdoc see cref="ISoftDeleteSettings.Enabled"/>
		public SoftDeleteSettingsDescriptor Enabled(bool? enabled = true) => Assign(enabled, (a, v) => a.Enabled = v);
	}
}
