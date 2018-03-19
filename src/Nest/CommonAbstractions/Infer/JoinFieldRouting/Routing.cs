using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(RoutingJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Routing : IEquatable<Routing>, IUrlParameter
	{
		internal string StringValue { get; }
		internal long? LongValue { get; }
		internal string StringOrLongValue => this.StringValue ?? this.LongValue?.ToString(CultureInfo.InvariantCulture);

		internal object Document { get; }
		internal Func<object> DocumentGetter { get; }

		internal int Tag { get; }

		internal Routing(Func<object> documentGetter)
		{
			Tag = 0;
			DocumentGetter = documentGetter;
		}

		public Routing(string routing)
		{
			Tag = 1;
			StringValue = routing;
		}

		public Routing(long routing)
		{
			Tag = 2;
			LongValue = routing;
		}

		public Routing(object document)
		{
			Tag = 4;
			Document = document;
		}

		public static implicit operator Routing(string routing) => routing.IsNullOrEmptyCommaSeparatedList(out _) ? null : new Routing(routing);
		public static implicit operator Routing(string[] routing) => routing.IsEmpty() ? null : new Routing(string.Join(",", routing));
		public static implicit operator Routing(long routing) => new Routing(routing);
		public static implicit operator Routing(Guid routing) => new Routing(routing.ToString("D"));

		/// <summary> Use the inferred routing from <paramref name="document"/> </summary>
		public static Routing From<T>(T document) where T : class => new Routing(document);

		private string DebugDisplay => this.StringOrLongValue ?? "Routing from instance typeof: " + Document?.GetType().Name;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		private string GetString(IConnectionSettingsValues nestSettings)
		{
			string value = null;
			if (this.DocumentGetter != null)
			{
				var doc = this.DocumentGetter();
				value = nestSettings.Inferrer.Routing(doc);
			}
			else if (this.Document != null)
				value = nestSettings.Inferrer.Routing(this.Document);

			return value ?? this.StringOrLongValue;
		}

		public static bool operator ==(Routing left, Routing right) => Equals(left, right);

		public static bool operator !=(Routing left, Routing right) => !Equals(left, right);

		public bool Equals(Routing other)
		{
			if (this.Tag == other.Tag)
			{
				switch (this.Tag)
				{
					case 0:
						var t = this.DocumentGetter();
						var o = other.DocumentGetter();
						return t?.Equals(o) ?? false;
					case 4: return this.Document?.Equals(other.Document) ?? false;
					default:
						return StringEquals(this.StringOrLongValue, other.StringOrLongValue);
				}
			}
			else if (this.Tag + other.Tag == 3)
				return StringEquals(this.StringOrLongValue, other.StringOrLongValue);
			else
				return false;
		}

		private static readonly char[] Separator = {','};

		private static bool StringEquals(string left, string right)
		{
			if (left == null && right == null) return true;
			else if (left == null || right == null)
				return false;
			if (!left.Contains(",") || !right.Contains(",")) return left == right;

			var l1 = left.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(v=>v.Trim()).ToList();
			var l2 = right.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(v=>v.Trim()).ToList();
			if (l1.Count != l2.Count) return false;
			return l1.Count == l2.Count && !l1.Except(l2).Any();

		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case Routing r: return Equals(r);
				case string s: return Equals(s);
				case int l: return Equals(l);
				case long l: return Equals(l);
				case Guid g: return Equals(g);
			}

			return Equals(new Routing(obj));
		}

		private static int TypeHashCode { get; } = typeof(Routing).GetHashCode();
		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (this.StringValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (this.LongValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (this.DocumentGetter?.GetHashCode() ?? 0);
				result = (result * 397) ^ (this.Document?.GetHashCode() ?? 0);
				return result;
			}
		}
	}
}
