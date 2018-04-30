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
			if (format == "r")
				return arg.ToString();
			return Uri.EscapeDataString(this.GetStringValue(arg));
		}

		public string GetStringValue(object valueType)
		{
			switch (valueType)
			{
				case null: return null;
				case string s: return s;
				case string[] ss: return string.Join(",", ss);
				case IEnumerable<object> pns:
					return string.Join(",", pns.Select(AttemptTheRightToString));
				case Enum e: return e.GetStringValue();
				case bool b: return b ? "true" : "false";
				case DateTimeOffset offset: return offset.ToString("o");
				case TimeSpan span: return span.ToTimeUnit();
				default:
					return AttemptTheRightToString(valueType);
			}
		}

		public string AttemptTheRightToString(object value)
		{
			var explicitImplementation = this.QueryStringValueType(value as IUrlParameter);
			if (explicitImplementation != null) return explicitImplementation;
			return value.ToString();
		}

		public string QueryStringValueType(IUrlParameter value) =>
			value?.GetString(this._settings);
	}
}
