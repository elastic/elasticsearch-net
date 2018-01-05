using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net
{
	internal class UrlFormatProvider : IFormatProvider, ICustomFormatter
	{
		private readonly IConnectionConfigurationValues _settings;

		public UrlFormatProvider(IConnectionConfigurationValues settings)
		{
			_settings = settings;
		}

		public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg == null) throw new ArgumentNullException();
			if (format == "r") return arg.ToString();
			var value = GetUnescapedStringRepresentation(arg, this._settings);
			return value.IsNullOrEmpty() ? string.Empty : Uri.EscapeDataString(value);
		}

		public static string GetUnescapedStringRepresentation(object value, IConnectionConfigurationValues settings)
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
					return string.Join(",", pns.Select(o=> ResolveUrlParameterOrDefault(o, settings)));
				default:
					return ResolveUrlParameterOrDefault(value, settings);
			}
		}

		private static string ResolveUrlParameterOrDefault(object value, IConnectionConfigurationValues settings) =>
			value is IUrlParameter urlParam ? urlParam?.GetString(settings) : value.ToString();
	}
}
