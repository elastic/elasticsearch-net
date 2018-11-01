using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Tests.Framework;

namespace Tests.CodeStandards.Serialization
{
	public class Converters
	{
		[U]
		public void CustomConvertersShouldBeInternal()
		{
			var converters = typeof(IElasticClient).Assembly()
				.GetTypes()
				.Where(t => typeof(JsonConverter).IsAssignableFrom(t))
				.ToList();
			var visible = new List<string>();
			foreach (var converter in converters)
				if (converter.IsVisible())
					visible.Add(converter.Name);
			visible.Should().BeEmpty();
		}
	}
}
