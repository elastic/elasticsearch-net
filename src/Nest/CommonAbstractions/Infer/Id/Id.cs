using System;
using System.Diagnostics;
using System.Globalization;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IdJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Id : IEquatable<Id>, IUrlParameter
	{
		internal string StringValue { get; }
		internal long? LongValue { get; }
		internal string StringOrLongValue => this.StringValue ?? this.LongValue?.ToString(CultureInfo.InvariantCulture);
		internal object Document { get; }
		internal int Tag { get; }

		public Id(string id) { Tag = 0; StringValue = id; }
		public Id(long id) { Tag = 1; LongValue = id; }
		public Id(object document) { Tag = 2; Document = document; }

		public static implicit operator Id(string id) => id.IsNullOrEmpty() ? null : new Id(id);
		public static implicit operator Id(long id) => new Id(id);
		public static implicit operator Id(Guid id) => new Id(id.ToString("D"));

		public static Id From<T>(T document) where T : class => new Id(document);

		private string DebugDisplay => StringOrLongValue ?? "Id from instance typeof: " + Document?.GetType().Name;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		private string GetString(IConnectionSettingsValues nestSettings) =>
			this.Document != null ? nestSettings.Inferrer.Id(this.Document) : this.StringOrLongValue;

		public bool Equals(Id other)
		{
			if (this.Tag + other.Tag == 1)
				return this.StringOrLongValue == other.StringOrLongValue;
			else if (this.Tag != other.Tag) return false;
			switch (this.Tag)
			{
				case 0:
				case 1:
					return this.StringOrLongValue == other.StringOrLongValue;
				default:
					return this.Document?.Equals(other.Document) ?? false;
			}
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case Id r: return Equals(r);
				case string s: return Equals(s);
				case int l: return Equals(l);
				case long l: return Equals(l);
				case Guid g: return Equals(g);
			}
			return Equals(new Id(obj));
		}

		private static int TypeHashCode { get; } = typeof(Id).GetHashCode();
		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (this.StringValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (this.LongValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (this.Document?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(Id left, Id right) => Equals(left, right);

		public static bool operator !=(Id left, Id right) => !Equals(left, right);
	}
}
