using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A formatter that can utilize <see cref="IConnectionConfigurationValues" /> to resolve <see cref="IUrlParameter" />'s passed
	/// as format arguments. It also handles known string representations for e.g bool/Enums/IEnumerable<object>.
	/// </summary>
	public class ElasticsearchUrlFormatter : IFormatProvider, ICustomFormatter
	{
		private readonly IConnectionConfigurationValues _settings;

		public ElasticsearchUrlFormatter(IConnectionConfigurationValues settings) => _settings = settings;

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg == null) throw new ArgumentNullException();

			if (format == "r") return arg.ToString();

			var value = CreateString(arg, _settings);
			return value.IsNullOrEmpty() ? string.Empty : Uri.EscapeDataString(value);
		}

		public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

		public string CreateEscapedString(object value)
		{
			var r = CreateString(value, _settings);
			return r.IsNullOrEmpty() ? string.Empty : Uri.EscapeDataString(r);
		}

		public string CreateString(object value) => CreateString(value, _settings);

		public static string CreateString(object value, IConnectionConfigurationValues settings)
		{
			switch (value)
			{
				case null: return null;
				case string s: return s;
				case string[] ss: return string.Join(",", ss);
				case Enum e: return e.GetStringValue();
				case bool b: return b ? "true" : "false";
				case DateTimeOffset offset: return offset.ToString("o");
				case IEnumerable<object> pns:
					return string.Join(",", pns.Select(o => ResolveUrlParameterOrDefault(o, settings)));
				case TimeSpan timeSpan: return timeSpan.ToTimeUnit();
				default:
					return ResolveUrlParameterOrDefault(value, settings);
			}
		}

		private static string ResolveUrlParameterOrDefault(object value, IConnectionConfigurationValues settings) =>
			value is IUrlParameter urlParam ? urlParam.GetString(settings) : value.ToString();
	}
}
