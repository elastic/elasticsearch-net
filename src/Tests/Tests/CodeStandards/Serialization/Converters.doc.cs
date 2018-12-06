using System;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Utf8Json;

namespace Tests.CodeStandards.Serialization
{
	public class Converters
	{
		[U]
		public void CustomFormattersShouldBeInternal()
		{
			var formatters = typeof(IElasticClient).Assembly().GetTypes()
				.Where(t => typeof(IJsonFormatter<>).IsAssignableFrom(t))
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
