using System;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.CodeStandards.Serialization
{
	public class Converters
	{
		[U]
		public void CustomConvertersShouldBeInternal()
		{
			var converters = typeof(IElasticClient).Assembly().GetTypes()
				.Where(t => typeof(JsonConverter).IsAssignableFrom(t))
				.ToList();
			var visible = new List<string>();
			foreach (var converter in converters)
			{
				if (converter.IsVisible())
					visible.Add(converter.Name);
			}
			visible.Should().BeEmpty();
		}
	}
}
