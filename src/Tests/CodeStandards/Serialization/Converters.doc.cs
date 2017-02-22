using System;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net_5_2_0;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;

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
