using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace Nest.Tests.Unit.Template
{
	[TestFixture]
	public class TemplateSpikeTests : BaseJsonTests
	{
		[Test]
		public void Print()
		{

			var search = this._client.Search<object>(s => s
					.Strict()
					.Query(q =>
						Template.If("start",
							q.Range(r => r
								.OnField("fieldx")
								.Greater(Template.Variable("start"))
								.Lower(Template.Variable("end", "20"))
							)
						)
						&& Template.If("other_var", new TermQuery()
						{
							Field = Template.Variable("myfield"),
							Value = Template.Variable("myval")
						})
						&& q.Terms("field", Template.Array("array"))
					)
					.Sort(Template.If<object>("sort", sort => sort.Ascending().OnField("{{sort}}")))
				);
			Assert.Fail(search.ConnectionStatus.ToString());
		}

	}

	interface IIfTemplate
	{
		object Instance { get; }
		string Variable { get; }
	}
	// Define other methods and classes here
	public static class Template
	{
		[JsonConverter(typeof(IfTemplateConverter))]
		private class IfSortFieldDescriptor : SortFieldDescriptor<object>, IIfTemplate
		{
			private readonly IFieldSort _queryContainer;
			private readonly string _variable;
			object IIfTemplate.Instance { get { return _queryContainer; } }
			string IIfTemplate.Variable { get { return _variable; } }
			public IfSortFieldDescriptor(string variable, IFieldSort o)
			{
				_variable = variable;
				_queryContainer = o;
			}
		}


		[JsonConverter(typeof(IfTemplateConverter))]
		private class IfQueryContainer : QueryContainer, IIfTemplate
		{
			private readonly QueryContainer _queryContainer;
			private readonly string _variable;
			object IIfTemplate.Instance { get { return _queryContainer; } }
			string IIfTemplate.Variable { get { return _variable; } }
			public IfQueryContainer(string variable, QueryContainer o)
			{
				_variable = variable;
				_queryContainer = o;
			}
		}

		public static Func<SortFieldDescriptor<T>, IFieldSort> If<T>(string variable, Func<SortFieldDescriptor<T>, IFieldSort> s)
		where T : class
		{
			return new Func<SortFieldDescriptor<T>, IFieldSort>(inner => new IfSortFieldDescriptor(variable, s(inner)));
		}
		public static QueryContainer If(string variable, QueryContainer o)
		{
			return new IfQueryContainer(variable, o);
		}

		public static string Variable(string name, string defaultValue = null)
		{
			if (!string.IsNullOrWhiteSpace(defaultValue))
				return "{{" + name + "}}{{^" + name + "}}" + defaultValue + "{{/" + name + "}}";
			return "{{" + name + "}}";
		}
		public static IEnumerable<string> Array(string name)
		{
			return new[] {
				"{{#"+name+"}",
				"{{.}}",
				"{{/" + name + "}"
			};
		}
	}

	public class IfTemplateConverter : JsonConverter
	{
		public override bool CanWrite { get { return true; } }

		public override bool CanRead { get { return false; } }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IIfTemplate;
			if (v == null) writer.WriteNull();
			var l = writer.Path.Split(new[] { '.' }).Length + 1;
			writer.WriteRaw("{{#" + v.Variable + "}}");
			serializer.Serialize(writer, v.Instance);
			writer.WriteRaw("{{/" + v.Variable + "}}");

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}
