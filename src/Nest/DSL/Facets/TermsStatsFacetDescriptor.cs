using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<TermsStatsFacetRequest>))]
	public interface ITermsStatsFacetRequest : IFacetRequest
	{
		[JsonProperty(PropertyName = "key_field")]
		PropertyPathMarker KeyField { get; set; }

		[JsonProperty(PropertyName = "value_field")]
		PropertyPathMarker ValueField { get; set; }

		[JsonProperty(PropertyName = "key_script")]
		string KeyScript { get; set; }

		[JsonProperty(PropertyName = "value_script")]
		string ValueScript { get; set; }

		[JsonProperty(PropertyName = "order")]
		[JsonConverter(typeof (StringEnumConverter))]
		TermsStatsOrder? Order { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Lang { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }
	}

	public class TermsStatsFacetRequest : FacetRequest, ITermsStatsFacetRequest
	{
		public PropertyPathMarker KeyField { get; set; }
		public PropertyPathMarker ValueField { get; set; }
		public string KeyScript { get; set; }
		public string ValueScript { get; set; }
		public TermsStatsOrder? Order { get; set; }
		public string Lang { get; set; }
		public int? Size { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class TermsStatsFacetDescriptor<T> : BaseFacetDescriptor<TermsStatsFacetDescriptor<T>,T>, ITermsStatsFacetRequest where T : class
	{
		public ITermsStatsFacetRequest Self { get { return this; } }

		PropertyPathMarker ITermsStatsFacetRequest.KeyField { get; set; }

		PropertyPathMarker ITermsStatsFacetRequest.ValueField { get; set; }

		string ITermsStatsFacetRequest.KeyScript { get; set; }

		string ITermsStatsFacetRequest.ValueScript { get; set; }

		TermsStatsOrder? ITermsStatsFacetRequest.Order { get; set; }

		string ITermsStatsFacetRequest.Lang { get; set; }

		int? ITermsStatsFacetRequest.Size { get; set; }

		Dictionary<string, object> ITermsStatsFacetRequest.Params { get; set; }

		public TermsStatsFacetDescriptor<T> KeyField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.KeyField = objectPath;
			return this;
		}
		public TermsStatsFacetDescriptor<T> KeyField(string keyField)
		{
			keyField.ThrowIfNull("keyField");
			Self.KeyField = keyField;
			return this;
		}
		public TermsStatsFacetDescriptor<T> KeyScript(string keyScript)
		{
			keyScript.ThrowIfNull("keyScript");
			Self.KeyScript = keyScript;
			return this;
		}
		public TermsStatsFacetDescriptor<T> ValueField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.ValueField = objectPath;
			return this;
		}
		public TermsStatsFacetDescriptor<T> ValueField(string valueField)
		{
			valueField.ThrowIfNull("valueField");
			Self.ValueField = valueField;
			return this;
		}
		public TermsStatsFacetDescriptor<T> ValueScript(string valueScript)
		{
			valueScript.ThrowIfNull("valueScript");
			Self.ValueScript = valueScript;
			return this;
		}
		public TermsStatsFacetDescriptor<T> Language(string language)
		{
			language.ThrowIfNull("language");
			Self.Lang = language;
			return this;
		}
		public TermsStatsFacetDescriptor<T> Size(int size)
		{
			size.ThrowIfNull("size");
			Self.Size = size;
			return this;
		}
		public TermsStatsFacetDescriptor<T> Order(TermsStatsOrder order)
		{
			Self.Order = order;
			return this;
		}

		public TermsStatsFacetDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}
	}
}
