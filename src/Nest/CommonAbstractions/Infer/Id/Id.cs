using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IdJsonConverter))]
	public class Id : IUrlParameter
	{
		internal object Value { get; set; }
		internal object Document { get; set; }

		public Id(string id) { Value = id; }
		public Id(long id) { Value = id; }
		public Id(object document) { Document = document; }

		public static implicit operator Id(string id) => new Id(id);
		public static implicit operator Id(long id) => new Id(id);
		public static implicit operator Id(Guid id) => new Id(id.ToString("D"));

		public static Id From<T>(T document) where T : class => new Id(document);

		public string GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		internal string GetString(IConnectionSettingsValues nestSettings)
		{
			if (this.Document != null)
			{
				Value = nestSettings.Inferrer.Id(this.Document);
			}

			var s = Value as string;
			return s ?? this.Value?.ToString();
		}
	}
}
