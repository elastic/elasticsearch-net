using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<IndicesFilterDescriptor<object>>))]
	public interface IIndicesFilter : IFilter
	{
		[JsonProperty("indices")]
		IEnumerable<string> Indices { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterDescriptor<object>>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty("no_match_filter")]
		[JsonConverter(typeof(NoMatchFilterConverter))]
		IFilterContainer NoMatchFilter { get; set; }

	}

	public class NoMatchFilterConverter : CompositeJsonConverter<ReadAsTypeConverter<FilterDescriptor<object>>, CustomJsonConverter>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var en = serializer.Deserialize<NoMatchShortcut>(reader);
				return new NoMatchFilterContainer {Shortcut = en};
			}

			return base.ReadJson(reader, objectType, existingValue, serializer);
		}
	}

	public class NoMatchFilterContainer : FilterContainer, ICustomJson
	{
		public NoMatchShortcut? Shortcut { get; set; }

		object ICustomJson.GetCustomJson()
		{
			if (this.Shortcut.HasValue) return this.Shortcut;
			var f = ((IFilterContainer)this);
			if (f.RawFilter.IsNullOrEmpty()) return f;
			return new RawJson(f.RawFilter);
		}
	}

	public class IndicesFilter : PlainFilter, IIndicesFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Indices = this;
		}

		bool IFilter.IsConditionless { get { return false; } }
		public NestedScore? Score { get; set; }
		public IFilterContainer Filter { get; set; }
		public IFilterContainer NoMatchFilter { get; set; }
		public IEnumerable<string> Indices { get; set; }
	}

	public class IndicesFilterDescriptor<T> : FilterBase, IIndicesFilter where T : class
	{
		IFilterContainer IIndicesFilter.Filter { get; set; }

		IFilterContainer IIndicesFilter.NoMatchFilter { get; set; }

		IEnumerable<string> IIndicesFilter.Indices { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return ((IIndicesFilter)this).NoMatchFilter == null && ((IIndicesFilter)this).Filter == null;
			}
		}

		public IndicesFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var qd = new FilterDescriptor<T>();
			var q = filterSelector(qd);
			if (q.IsConditionless)
				return this;


			((IIndicesFilter)this).Filter = q;
			return this;
		}

		public IndicesFilterDescriptor<T> Filter<K>(Func<FilterDescriptor<K>, FilterContainer> filterSelector) where K : class
		{
			var qd = new FilterDescriptor<K>();
			var q = filterSelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesFilter)this).Filter = q;
			return this;
		}

		public IndicesFilterDescriptor<T> NoMatchFilter(NoMatchShortcut shortcut)
		{
			((IIndicesFilter)this).NoMatchFilter = new NoMatchFilterContainer { Shortcut = shortcut };
			return this;
		}
		
		public IndicesFilterDescriptor<T> NoMatchFilter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var qd = new FilterDescriptor<T>();
			var q = filterSelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesFilter)this).NoMatchFilter = q;
			return this;
		}
		public IndicesFilterDescriptor<T> NoMatchFilter<K>(Func<FilterDescriptor<K>, IFilterContainer> filterSelector) where K : class
		{
			var qd = new FilterDescriptor<K>();
			var q = filterSelector(qd);
			if (q.IsConditionless)
				return this;

			((IIndicesFilter)this).NoMatchFilter = q;
			return this;
		}
		public IndicesFilterDescriptor<T> Indices(IEnumerable<string> indices)
		{
			((IIndicesFilter)this).Indices = indices;
			return this;
		}
	}
}
