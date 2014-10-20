using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesStats4
{
	public partial class IndicesStats4YamlTests
	{	
	
		public class IndicesStats413FieldsYamlBase : YamlTestsBase
		{
			public IndicesStats413FieldsYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						bar= new {
							properties= new {
								bar= new {
									type= "string",
									fields= new {
										completion= new {
											type= "completion"
										}
									}
								},
								baz= new {
									type= "string",
									fields= new {
										completion= new {
											type= "completion"
										}
									}
								}
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test1", _body));

				//do index 
				_body = new {
					bar= "bar",
					baz= "baz"
				};
				this.Do(()=> _client.Index("test1", "bar", "1", _body));

				//do index 
				_body = new {
					bar= "bar",
					baz= "baz"
				};
				this.Do(()=> _client.Index("test2", "baz", "1", _body));

				//do indices.refresh 
				this.Do(()=> _client.IndicesRefreshForAll());

				//do search 
				this.Do(()=> _client.SearchGet(nv=>nv
					.AddQueryString("sort", @"bar,baz")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsBlank2Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsBlank2Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll());

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsOne3Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsOne3Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fields", @"bar")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//is_false _response._all.total.completion.fields.bar; 
				this.IsFalse(_response._all.total.completion.fields.bar);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsMulti4Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsMulti4Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fields", @"bar,baz.completion")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"]);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"], 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsStar5Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsStar5Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fields", @"*")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.baz.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.baz.memory_size_in_bytes, 0);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"], 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsPattern6Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsPattern6Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fields", @"bar*")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsAllMetric7Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsAllMetric7Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all", nv=>nv
					.AddQueryString("fields", @"bar*")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsFielddataMetric8Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsFielddataMetric8Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("fielddata", nv=>nv
					.AddQueryString("fields", @"bar*")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion; 
				this.IsFalse(_response._all.total.completion);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsCompletionMetric9Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsCompletionMetric9Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("completion", nv=>nv
					.AddQueryString("fields", @"bar*")
				));

				//is_false _response._all.total.fielddata; 
				this.IsFalse(_response._all.total.fielddata);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FieldsMultiMetric10Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FieldsMultiMetric10Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("completion,fielddata,search", nv=>nv
					.AddQueryString("fields", @"bar*")
				));

				//gt _response._all.total.fielddata.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//gt _response._all.total.completion.size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.completion.size_in_bytes, 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsOne11Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsOne11Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fielddata_fields", @"bar")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsMulti12Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsMulti12Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fielddata_fields", @"bar,baz,baz.completion")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.baz.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.baz.memory_size_in_bytes, 0);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsStar13Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsStar13Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fielddata_fields", @"*")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//gt _response._all.total.fielddata.fields.baz.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.baz.memory_size_in_bytes, 0);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsPattern14Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsPattern14Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("fielddata_fields", @"*r")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsAllMetric15Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsAllMetric15Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all", nv=>nv
					.AddQueryString("fielddata_fields", @"*r")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsOneMetric16Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsOneMetric16Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("fielddata", nv=>nv
					.AddQueryString("fielddata_fields", @"*r")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class FielddataFieldsMultiMetric17Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void FielddataFieldsMultiMetric17Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("fielddata,search", nv=>nv
					.AddQueryString("fielddata_fields", @"*r")
				));

				//gt _response._all.total.fielddata.fields.bar.memory_size_in_bytes: 0; 
				this.IsGreaterThan(_response._all.total.fielddata.fields.bar.memory_size_in_bytes, 0);

				//is_false _response._all.total.fielddata.fields.baz; 
				this.IsFalse(_response._all.total.fielddata.fields.baz);

				//is_false _response._all.total.completion.fields; 
				this.IsFalse(_response._all.total.completion.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionFieldsOne18Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionFieldsOne18Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("completion_fields", @"bar.completion")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionFieldsMulti19Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionFieldsMulti19Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("completion_fields", @"bar.completion,baz,baz.completion")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"], 0);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionFieldsStar20Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionFieldsStar20Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("completion_fields", @"*")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"][@"size_in_bytes"], 0);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionPattern21Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionPattern21Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll(nv=>nv
					.AddQueryString("completion_fields", @"*r*")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionAllMetric22Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionAllMetric22Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("_all", nv=>nv
					.AddQueryString("completion_fields", @"*r*")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionOneMetric23Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionOneMetric23Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("completion", nv=>nv
					.AddQueryString("completion_fields", @"*r*")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class CompletionMultiMetric24Tests : IndicesStats413FieldsYamlBase
		{
			[Test]
			public void CompletionMultiMetric24Test()
			{	

				//do indices.stats 
				this.Do(()=> _client.IndicesStatsForAll("completion,search", nv=>nv
					.AddQueryString("completion_fields", @"*r*")
				));

				//gt _response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"]: 0; 
				this.IsGreaterThan(_response[@"_all"][@"total"][@"completion"][@"fields"][@"bar.completion"][@"size_in_bytes"], 0);

				//is_false _response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]; 
				this.IsFalse(_response[@"_all"][@"total"][@"completion"][@"fields"][@"baz.completion"]);

				//is_false _response._all.total.fielddata.fields; 
				this.IsFalse(_response._all.total.fielddata.fields);

			}
		}
	}
}

