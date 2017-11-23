using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework.Benchmarks;

namespace Tests.ClientConcepts.LowLevel
{
	[BenchmarkConfig(1000)]
	public class PostDataBenchmarks
	{
		private readonly PostData _postDataOfBytes;


		private readonly PostData _postDataOfBytesDisableDirectStreaming;
		private readonly PostData _postDataOfCollectionOfComplexObjects;
		private readonly PostData _postDataOfCollectionOfComplexObjectsDisableDirectStreaming;
		private readonly PostData _postDataOfCollectionOfSimpleObjects;
		private readonly PostData _postDataOfCollectionOfSimpleObjectsDisableDirectStreaming;
		private readonly PostData _postDataOfCollectionOfStrings;
		private readonly PostData _postDataOfCollectionOfStringsDisableDirectStreaming;
		private readonly PostData _postDataOfComplexObject;
		private readonly PostData _postDataOfComplexObjectDisableDirectStreaming;
		private readonly PostData _postDataOfSimpleObject;
		private readonly PostData _postDataOfSimpleObjectDisableDirectStreaming;
		private readonly PostData _postDataOfString;
		private readonly PostData _postDataOfStringDisableDirectStreaming;
		private readonly byte[] bytes = Encoding.UTF8.GetBytes("{my_property=\"value\"}");
		private readonly List<object> collectionOfComplexObjects;
		private readonly List<object> collectionOfSimpleObjects;
		private readonly List<string> collectionOfStrings = Enumerable.Range(0, 5).Select(i => i.ToString()).ToList();
		private readonly object complexObject;
		private readonly ConnectionSettings connectionSettings = new ConnectionSettings();

		private readonly object simpleObject;
		private readonly string @string = "{my_property=\"value\"}";

		public PostDataBenchmarks()
		{
			simpleObject = new {my_property = "value"};
			complexObject = new
			{
				input = new
				{
					chain = new
					{
						inputs = new object[]
						{
							new
							{
								simple = new
								{
									simple = new
									{
										str = "val1",
										num = 23,
										obj = new
										{
											str = "val2"
										}
									}
								}
							},
							new
							{
								http = new
								{
									http = new
									{
										request = new
										{
											host = "localhost",
											port = 8080,
											method = "post",
											path = "/path.html",
											proxy = new
											{
												host = "proxy",
												port = 6000
											},
											scheme = "https",
											auth = new
											{
												basic = new
												{
													username = "Username123",
													password = "Password123"
												}
											},
											body =
											"{\"query\" : {\"range\": {\"@timestamp\" : {\"from\": \"{{ctx.trigger.triggered_time}}||-5m\",\"to\": \"{{ctx.trigger.triggered_time}}\"}}}}",
											headers = new
											{
												header1 = "value1"
											},
											@params = new
											{
												lat = "52.374031",
												lon = "4.88969",
												appid = "appid"
											},
											connection_timeout = "3s",
											read_timeout = "500ms"
										},
										response_content_type = "text"
									}
								}
							},
							new
							{
								search = new
								{
									search = new
									{
										request = new
										{
											indices = new[] {"project"},
											body = new
											{
												size = 0,
												aggs = new
												{
													nested_tags = new
													{
														nested = new
														{
															path = "tags"
														},
														aggs = new
														{
															top_project_tags = new
															{
																terms = new
																{
																	field = "tags.name"
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				},
				transform = new
				{
					chain = new object[]
					{
						new
						{
							search = new
							{
								request = new
								{
									indices = new[] {"project"},
									indices_options = new
									{
										expand_wildcards = "open",
										ignore_unavailable = true
									},
									search_type = "dfs_query_then_fetch",
									body = new
									{
										query = new
										{
											match = new
											{
												state = new
												{
													query = "stable"
												}
											}
										}
									}
								},
								timeout = "10s"
							}
						},
						new
						{
							script = new
							{
								inline = "return [ 'time' : ctx.trigger.scheduled_time ]"
							}
						}
					}
				},
				condition = new
				{
					array_compare = new JObject
					{
						{
							"ctx.payload.search.aggregations.top_project_tags.buckets", new JObject
							{
								{"path", "doc_count"},
								{"gte", new JObject {{"value", 1}}}
							}
						}
					}
				},
				trigger = new
				{
					schedule = new
					{
						weekly = new[]
						{
							new {on = new[] {"monday"}, at = new[] {"noon"}},
							new {on = new[] {"friday"}, at = new[] {"17:00"}}
						}
					}
				},
				actions = new
				{
					reminder_email = new
					{
						email = new
						{
							to = new[] {"me@example.com"},
							subject = "Something's strange in the neighbourhood",
							body = new
							{
								text = "Dear {{ctx.payload.name}}, by the time you read these lines, I'll be gone"
							},
							attachments = new
							{
								http_attachment = new
								{
									http = new
									{
										inline = true,
										content_type = "application/json",
										request = new
										{
											url = "http://localhost:8080/http_attachment"
										}
									}
								},
								data_attachment = new
								{
									data = new
									{
										format = "json"
									}
								}
							}
						}
					},
					reminder_index = new
					{
						index = new
						{
							index = "put-watch-test-index",
							doc_type = "reminder",
							execution_time_field = "execution_time"
						}
					},
					reminder_pagerduty = new
					{
						pagerduty = new
						{
							account = "my_pagerduty_account",
							description = "pager duty description",
							attach_payload = true,
							event_type = "trigger",
							incident_key = "incident_key",
							context = new object[]
							{
								new
								{
									type = "image",
									src = "http://example.com/image"
								},
								new
								{
									type = "link",
									href = "http://example.com/link"
								}
							}
						}
					},
					reminder_slack = new
					{
						slack = new
						{
							account = "monitoring",
							message = new
							{
								from = "nest integration test",
								to = new[] {"#nest"},
								text = "slack message",
								attachments = new[]
								{
									new
									{
										title = "Attachment 1",
										author_name = "Russ Cam"
									}
								}
							}
						}
					},
					reminder_hipchat = new
					{
						hipchat = new
						{
							account = "notify-monitoring",
							message = new
							{
								body = "hipchat message",
								color = "purple",
								room = new[] {"nest"},
								notify = true
							}
						}
					}
				}
			};

			collectionOfSimpleObjects = Enumerable.Range(0, 5).Select(i => simpleObject).ToList();
			collectionOfComplexObjects = Enumerable.Range(0, 5).Select(i => complexObject).ToList();

			_postDataOfString = PostData.String(@string);
			_postDataOfBytes = PostData.Bytes(bytes);
			_postDataOfCollectionOfStrings = PostData.MultiJson(collectionOfStrings);
			_postDataOfCollectionOfSimpleObjects = PostData.MultiJson(collectionOfSimpleObjects);
			_postDataOfCollectionOfComplexObjects = PostData.MultiJson(collectionOfComplexObjects);
			_postDataOfSimpleObject = PostData.Serializable(simpleObject);
			_postDataOfComplexObject = PostData.Serializable(complexObject);

			PostData DisableStreaming(PostData data)
			{
				data.DisableDirectStreaming = true;
				return data;
			}
			_postDataOfStringDisableDirectStreaming = DisableStreaming(PostData.String(@string));
			_postDataOfBytesDisableDirectStreaming = DisableStreaming(PostData.Bytes(bytes));
			_postDataOfCollectionOfStringsDisableDirectStreaming = DisableStreaming(PostData.MultiJson(collectionOfStrings));
			_postDataOfCollectionOfSimpleObjectsDisableDirectStreaming = DisableStreaming(PostData.MultiJson(collectionOfSimpleObjects));
			_postDataOfCollectionOfComplexObjectsDisableDirectStreaming = DisableStreaming(PostData.MultiJson(collectionOfComplexObjects));
			_postDataOfSimpleObjectDisableDirectStreaming = DisableStreaming(PostData.Serializable(simpleObject));
			_postDataOfComplexObjectDisableDirectStreaming = DisableStreaming(PostData.Serializable(complexObject));
		}

		[Benchmark]
		public void PostString()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfString.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostBytes()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfBytes.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfStrings()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfStrings.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfSimpleObjects()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfSimpleObjects.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfComplexObjects()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfComplexObjects.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostSimpleObject()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfSimpleObject.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostComplexObject()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfComplexObject.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostStringDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfStringDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostBytesDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfBytesDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfStringsDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfStringsDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfSimpleObjectsDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfSimpleObjectsDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostCollectionOfComplexObjectsDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfCollectionOfComplexObjectsDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostSimpleObjectDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfSimpleObjectDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}

		[Benchmark]
		public void PostComplexObjectDisableDirectStreaming()
		{
			using (var ms = new MemoryStream())
			{
				_postDataOfComplexObjectDisableDirectStreaming.Write(ms, connectionSettings);
			}
		}
	}
}
