// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public interface IQueriesSettings
	{
		IQueriesCacheSettings Cache { get; set; }
	}

	public class QueriesSettings : IQueriesSettings
	{
		public IQueriesCacheSettings Cache { get; set; }
	}

	public class QueriesSettingsDescriptor : DescriptorBase<QueriesSettingsDescriptor, IQueriesSettings>, IQueriesSettings
	{
		IQueriesCacheSettings IQueriesSettings.Cache { get; set; }

		/// <inheritdoc />
		public QueriesSettingsDescriptor Cache(Func<QueriesCacheSettingsDescriptor, IQueriesCacheSettings> selector) =>
			Assign(selector.Invoke(new QueriesCacheSettingsDescriptor()), (a, v) => a.Cache = v);
	}
}
