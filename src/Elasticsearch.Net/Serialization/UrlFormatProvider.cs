using System;
using System.Collections.Generic;
using System.Linq;

namespace Elasticsearch.Net
{
	class UrlFormatProvider : IFormatProvider, ICustomFormatter
	{
		private readonly IConnectionConfigurationValues _settings;

		public UrlFormatProvider(IConnectionConfigurationValues settings)
		{
			_settings = settings;
		}

		public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg == null)
				throw new ArgumentNullException();
			if (format == "r") return arg.ToString();
			var value = this.GetStringValue(arg);
			return value.IsNullOrEmpty() ? string.Empty : Uri.EscapeDataString(value);
		}

		public string GetStringValue(object valueType)
		{
			if (valueType is string s) return s;

			if (valueType is string[] ss) return string.Join(",", ss);

			if (valueType is IEnumerable<object> pns)
				return string.Join(",", pns.Select(AttemptTheRightToString));

			if (valueType is Enum e) return e.GetStringValue();

			if (valueType is bool b) return b ? "true" : "false";

			if (valueType is DateTimeOffset offset) return offset.ToString("o");

			return AttemptTheRightToString(valueType);
		}

		public string AttemptTheRightToString(object value)
		{
			if (value is IUrlParameter urlParam) return this.QueryStringValueType(urlParam);
			return value.ToString();
		}

		public string QueryStringValueType(IUrlParameter value) => value?.GetString(this._settings);
	}
}
