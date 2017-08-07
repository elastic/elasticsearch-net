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
			var s = valueType as string;
			if (s != null)
				return s;

			var ss = valueType as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pns = valueType as IEnumerable<object>;
			if (pns != null)
				return string.Join(",", pns.Select(AttemptTheRightToString));

			var e = valueType as Enum;
			if (e != null) return e.GetStringValue();

			if (valueType is bool)
				return ((bool)valueType) ? "true" : "false";

			if (valueType is DateTimeOffset)
				return ((DateTimeOffset)valueType).ToString("o");

			return AttemptTheRightToString(valueType);
		}

		public string AttemptTheRightToString(object value)
		{
			var explicitImplementation = this.QueryStringValueType(value as IUrlParameter);
			if (explicitImplementation != null) return explicitImplementation;


			return value.ToString();
		}

		public string QueryStringValueType(IUrlParameter value)
		{
			if (value == null) return null;
			return value.GetString(this._settings);
		}


	}
}
