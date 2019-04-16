using System;
using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("indices.put_settings.json")]
	[JsonFormatter(typeof(UpdateIndexSettingsRequestFormatter))]
	public partial interface IUpdateIndexSettingsRequest
	{
		IDynamicIndexSettings IndexSettings { get; set; }
	}

	public partial class UpdateIndexSettingsRequest
	{
		public IDynamicIndexSettings IndexSettings { get; set; }
	}

	public partial class UpdateIndexSettingsDescriptor
	{
		IDynamicIndexSettings IUpdateIndexSettingsRequest.IndexSettings { get; set; }

		/// <inheritdoc />
		public UpdateIndexSettingsDescriptor IndexSettings(Func<DynamicIndexSettingsDescriptor, IPromise<IDynamicIndexSettings>> settings) =>
			Assign(a => a.IndexSettings = settings?.Invoke(new DynamicIndexSettingsDescriptor())?.Value);
	}

	internal class UpdateIndexSettingsRequestFormatter : IJsonFormatter<IUpdateIndexSettingsRequest>
	{
		private static readonly DynamicIndexSettingsFormatter DynamicIndexSettingsFormatter =
			new DynamicIndexSettingsFormatter();

		public IUpdateIndexSettingsRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var dynamicSettings = DynamicIndexSettingsFormatter.Deserialize(ref reader, formatterResolver);
			return new UpdateIndexSettingsRequest { IndexSettings = dynamicSettings };
		}

		public void Serialize(ref JsonWriter writer, IUpdateIndexSettingsRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			DynamicIndexSettingsFormatter.Serialize(ref writer, value.IndexSettings, formatterResolver);
		}
	}
}
