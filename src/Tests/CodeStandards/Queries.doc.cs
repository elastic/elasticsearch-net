using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Tests.CodeStandards
{
	public class QueriesStandards
	{
		protected static PropertyInfo[] QueryProperties = typeof(IQueryContainer).GetProperties();
		protected static PropertyInfo[] QueryPlaceHolderProperties = typeof(IQueryContainer).GetProperties()
			.Where(a=>!a.GetCustomAttributes<JsonIgnoreAttribute>().Any()).ToArray();

		/*
		* All properties must be either marked with JsonIgnore or JsonProperty
		*/
		[U] public void InterfacePropertiesMustBeMarkedExplicitly()
		{
			var properties = from p in QueryProperties
							 let a = p.GetCustomAttributes<JsonIgnoreAttribute>().Concat<Attribute>(p.GetCustomAttributes<JsonPropertyAttribute>())
							 where a.Count() != 1
							 select p;
			properties.Should().BeEmpty();
		}

		[U] public void StaticQueryExposesAll()
		{
			var staticProperties = from p in typeof(Query<>).GetMethods()
								   let name = p.Name.StartsWith("GeoShape") ? "GeoShape" : p.Name 
								   select name;

			var placeHolders = QueryPlaceHolderProperties.Select(p => p.Name.StartsWith("GeoShape") ? "GeoShape" : p.Name);
			staticProperties.Distinct().Should().Contain(placeHolders.Distinct());
		}
		[U] public void FluentDescriptorExposesAll()
		{
			var fluentMethods = from p in typeof(QueryContainerDescriptor<>).GetMethods()
								   let name = p.Name.StartsWith("GeoShape") ? "GeoShape" : p.Name 
								   select name;

			var placeHolders = QueryPlaceHolderProperties.Select(p => p.Name.StartsWith("GeoShape") ? "GeoShape" : p.Name);
			fluentMethods.Distinct().Should().Contain(placeHolders.Distinct());
							
		}

	}
}
