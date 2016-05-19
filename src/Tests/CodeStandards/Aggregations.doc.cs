using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;

namespace Tests.CodeStandards
{
	public class Aggregations
	{
		[U]
		/* Ensures that the aggregation name => aggregate type map in AggregateJsonConverter
		/* accounts for all aggregations that have a response type. */
		public void AllAggregationsAccountedFor()
		{
			var nest = Assembly.GetAssembly(typeof(IElasticClient));

			var aggregateTypes = nest.GetType("Nest.AggregateJsonConverter")
				.GetField("_aggregateTypes", BindingFlags.NonPublic | BindingFlags.Static)
				.GetValue(null) as Dictionary<string, Type>;

			var initializers = nest.GetTypes()
				.Where(t => typeof(AggregationBase).IsAssignableFrom(t) && t != typeof(AggregationBase) && t.IsClass && !t.IsAbstract && t.IsPublic)
				.Select(t => Activator.CreateInstance(t, true))
				.ToList();

			var descriptors = nest.GetTypes()
				.Where(t => typeof(IAggregation).IsAssignableFrom(t) && t != typeof(IAggregation) && t.Name.Contains("Descriptor") && !t.IsAbstract)
				.Select(t => t.IsGeneric() ? Activator.CreateInstance(t.MakeGenericType(typeof(object))) : Activator.CreateInstance(t) )
				.ToList();

			initializers.Count.Should().Be(descriptors.Count);

			// These aggregations do not have a response (aggregate) type
			var exceptions = new List<string> { "bucket_selector", "serial_diff" };

			var initializersNotAccountedFor = new List<string>();
			foreach(var instance in initializers)
			{
				var aggTypeName = (string)instance.GetType().GetProperty("TypeName").GetValue(instance);
				if (!aggregateTypes.ContainsKey(aggTypeName))
					initializersNotAccountedFor.Add(aggTypeName);
			}

			var descriptorsNotAccountedFor = new List<string>();
			foreach(var instance in descriptors)
			{
				var aggTypeName = (string)instance.GetType().GetProperty("TypeName").GetValue(instance);
				if (!aggregateTypes.ContainsKey(aggTypeName))
					descriptorsNotAccountedFor.Add(aggTypeName);
			}

			initializersNotAccountedFor.Should().BeEquivalentTo(exceptions);
			descriptorsNotAccountedFor.Should().BeEquivalentTo(exceptions);
		}
	}
}
