using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.CodeStandards.Serialization
{
	public class Formatters
	{
		[U]
		public void CustomFormattersShouldBeInternal()
		{
			// TODO: Make internals visible to IL generated modules
			var formatters = typeof(IElasticClient).Assembly.GetTypes()
				.Concat(typeof(IElasticLowLevelClient).Assembly.GetTypes())
				.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IJsonFormatter<>)))
				.ToList();
			var visible = new List<string>();
			foreach (var formatter in formatters)
			{
				if (formatter.IsVisible())
					visible.Add(formatter.Name);
			}
			visible.Should().BeEmpty();
		}
	}
}
