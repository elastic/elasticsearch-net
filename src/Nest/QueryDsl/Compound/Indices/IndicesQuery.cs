using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<IndicesQueryDescriptor<object>>))]
	public interface IIndicesQuery : IQuery
	{
		[JsonProperty("indices")]
		IEnumerable<string> Indices { get; set; }

		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("no_match_query")]
		[JsonConverter(typeof(NoMatchQueryConverter))]
		IQueryContainer NoMatchQuery { get; set; }
	}

	public class NoMatchQueryConverter : CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var en = serializer.Deserialize<NoMatchShortcut>(reader);
				return new NoMatchQueryContainer {Shortcut = en};
			}

			return base.ReadJson(reader, objectType, existingValue, serializer);
		}
	}

	public class NoMatchQueryContainer : QueryContainer, ICustomJson
	{
		public NoMatchShortcut? Shortcut { get; set; }

		object ICustomJson.GetCustomJson()
		{
			if (this.Shortcut.HasValue) return this.Shortcut;
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f;
			return new RawJson(f.RawQuery);
		}
	}

	public class IndicesQuery : PlainQuery, IIndicesQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Indices = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public string Name { get; set; }
		public NestedScore? Score { get; set; }
		public IQueryContainer Query { get; set; }
		public IQueryContainer NoMatchQuery { get; set; }
		public IEnumerable<string> Indices { get; set; }
	}

	public class IndicesQueryDescriptor<T> : IIndicesQuery where T : class
	{
		private IIndicesQuery Self { get { return this; }}

		NestedScore? IIndicesQuery.Score { get; set; }

		IQueryContainer IIndicesQuery.Query { get; set; }

		IQueryContainer IIndicesQuery.NoMatchQuery { get; set; }

		IEnumerable<string> IIndicesQuery.Indices { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.NoMatchQuery == null && Self.Query == null;
			}
		}

		string IQuery.Name { get; set; }

		public IndicesQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public IndicesQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			Self.Query = q;
			return this;
		}

		public IndicesQueryDescriptor<T> Query<K>(Func<QueryDescriptor<K>, QueryContainer> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			Self.Query = q;
			return this;
		}
		
		public IndicesQueryDescriptor<T> NoMatchQuery(NoMatchShortcut shortcut)
		{
			Self.NoMatchQuery = new NoMatchQueryContainer { Shortcut = shortcut };
			return this;
		}

		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var qd = new QueryDescriptor<T>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			Self.NoMatchQuery = q;
			return this;
		}
		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryDescriptor<K>, IQueryContainer> querySelector) where K : class
		{
			var qd = new QueryDescriptor<K>();
			var q = querySelector(qd);
			if (q.IsConditionless)
				return this;

			Self.NoMatchQuery = q;
			return this;
		}
		public IndicesQueryDescriptor<T> Indices(IEnumerable<string> indices)
		{
			Self.Indices = indices;
			return this;
		}
	}
}
