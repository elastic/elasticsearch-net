// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(RoutingFormatter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Routing : IEquatable<Routing>, IUrlParameter
	{
		private static readonly char[] Separator = { ',' };

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

		internal object Document { get; }
		internal Func<object> DocumentGetter { get; }
		internal long? LongValue { get; }
		internal string StringOrLongValue => StringValue ?? LongValue?.ToString(CultureInfo.InvariantCulture);
		internal string StringValue { get; }

		internal int Tag { get; }

		private string DebugDisplay => StringOrLongValue ?? "Routing from instance typeof: " + Document?.GetType().Name;

		public override string ToString() => DebugDisplay;

		private static int TypeHashCode { get; } = typeof(Routing).GetHashCode();

		public bool Equals(Routing other)
		{
			if (other == null) return false;
			if (Tag == other.Tag)
			{
				switch (Tag)
				{
					case 0:
						var t = DocumentGetter();
						var o = other.DocumentGetter();
						return t?.Equals(o) ?? false;
					case 4: return Document?.Equals(other.Document) ?? false;
					default:
						return StringEquals(StringOrLongValue, other.StringOrLongValue);
				}
			}
			else if (Tag + other.Tag == 3)
				return StringEquals(StringOrLongValue, other.StringOrLongValue);
			else
				return false;
		}

		string IUrlParameter.GetString(ITransportConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			return GetString(nestSettings);
		}

		public static implicit operator Routing(string routing) => routing.IsNullOrEmptyCommaSeparatedList(out _) ? null : new Routing(routing);

		public static implicit operator Routing(string[] routing) => routing.IsEmpty() ? null : new Routing(string.Join(",", routing));

		public static implicit operator Routing(long routing) => new Routing(routing);

		public static implicit operator Routing(Guid routing) => new Routing(routing.ToString("D"));

		/// <summary> Use the inferred routing from <paramref name="document" /> </summary>
		public static Routing From<T>(T document) where T : class => new Routing(document);

		private string GetString(IConnectionSettingsValues nestSettings)
		{
			string value = null;
			if (DocumentGetter != null)
			{
				var doc = DocumentGetter();
				value = nestSettings.Inferrer.Routing(doc);
			}
			else if (Document != null)
				value = nestSettings.Inferrer.Routing(Document);

			return value ?? StringOrLongValue;
		}

		public static bool operator ==(Routing left, Routing right) => Equals(left, right);

		public static bool operator !=(Routing left, Routing right) => !Equals(left, right);

		private static bool StringEquals(string left, string right)
		{
			if (left == null && right == null) return true;
			else if (left == null || right == null)
				return false;

			if (!left.Contains(",") || !right.Contains(",")) return left == right;

			var l1 = left.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
			var l2 = right.Split(Separator, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
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

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (StringValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (LongValue?.GetHashCode() ?? 0);
				result = (result * 397) ^ (DocumentGetter?.GetHashCode() ?? 0);
				result = (result * 397) ^ (Document?.GetHashCode() ?? 0);
				return result;
			}
		}

		internal bool ShouldSerialize(IJsonFormatterResolver formatterResolver)
		{
			var inferrer = formatterResolver.GetConnectionSettings().Inferrer;
			var resolved = inferrer.Resolve(this);
			return !resolved.IsNullOrEmpty();
		}
	}
}
