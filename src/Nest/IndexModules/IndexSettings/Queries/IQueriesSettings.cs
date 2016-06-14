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

		/// <inheritdoc/>
		public QueriesSettingsDescriptor Cache(Func<QueriesCacheSettingsDescriptor, IQueriesCacheSettings> selector) =>
			Assign(a => a.Cache = selector.Invoke(new QueriesCacheSettingsDescriptor()));
	}
}
