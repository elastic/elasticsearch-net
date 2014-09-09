using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using Nest.Tests.MockData.Domain;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Nest.Tests.Unit.Template
{
	[TestFixture]
	public class TemplateSpikeTests : BaseJsonTests
	{
		[Test]
		public void Print()
		{

			var search = this._client.Search<ElasticsearchProject>(s => s
					.Strict()
					.Query(q =>
						TemplateBuilder.Section("start",
							q.Range(r => r
								.OnField("fieldx")
								.Greater(TemplateBuilder.Variable("start"))
								.Lower(TemplateBuilder.Variable("end", "20"))
							)
						)
						&& TemplateBuilder.Section("other_var", new TermQuery()
						{
							Field = TemplateBuilder.Variable("myfield"),
							Value = TemplateBuilder.Variable("myval")
						})
						&& q.Terms("field", TemplateBuilder.Array("array"))
					)
					.Sort(TemplateBuilder.Section<ElasticsearchProject>("sort", sort => sort.Ascending().OnField("{{sort}}")))
				);

			Assert.Fail(search.ConnectionStatus.ToString());
		}

		[Test]
		public void SearchTemplateByQuery()
		{
			var search = this._client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(t => t
					.Query<object>(q =>
						TemplateBuilder.Section("start", q.Match(m => m
								.OnField(TemplateBuilder.Variable("my_field"))
								.Query(TemplateBuilder.Variable("my_value"))
							)
						)
					)
				)
				.Params(new Dictionary<string, object>
					{
						{ "my_field", "foo" },
						{ "my_value", "bar" }
					}
				)
			);
		}

		[Test]
		public void SearchTemplateByFile()
		{
			var search = this._client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(t => t
					.File("myTemplate.mustache")
				)
				.Params(new Dictionary<string, object>
				{
					{ "my_field", "foo" },
					{ "my_value", "bar" }
				})
			);
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

		[Test]
		public void SearchTemplateById()
		{
			var search = this._client.SearchTemplate<ElasticsearchProject>(s => s
				.Template(t => t
					.Id("myTemplateName")
				)
				.Params(new Dictionary<string, object>
				{
					{ "my_field", "foo" },
					{ "my_value", "bar" }
				})
			);
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
