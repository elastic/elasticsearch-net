using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;
using Xunit;

namespace Tests.CodeStandards
{
	public class ElasticClientStandards
	{
		/*
		* Fluent methods on IElasticClient (Func<Descriptor, Interface>) should be named `selector`.
		*/
		[U]
		public void ConsistentFluentParameterNames()
		{
			var fluentParameters =
				from m in typeof (IElasticClient).GetMethods()
				from p in m.GetParameters()
				where p.ParameterType.BaseType == typeof (MulticastDelegate)
				where !p.Name.Equals("selector")
				select $"method '{nameof(IElasticClient)}.{m.Name}' should have parameter name of 'selector' but has a name of '{p.Name}'";

			fluentParameters.Should().BeEmpty();
		}

		/*
		* Similarly, OIS methods on IElasticClient (IRequest) should be named `request`.
		*/
		//[U]
		public void ConsistentInitializerParameterNames()
		{
			var requestParameters =
				from m in typeof(IElasticClient).GetMethods()
				from p in m.GetParameters()
				where typeof(IRequest).IsAssignableFrom(p.ParameterType)
				select p;

			foreach(var requestParameter in requestParameters)
				requestParameter.Name.Should().Be("request");
		}

		/*
		* Request objects on OIS methods are always required, and so they shouldn't be nullable
		*/
		[U]
		public void InitializerRequestsAreNotOptional()
		{
			var requestParameters =
				(from m in typeof(IElasticClient).GetMethods()
				 from p in m.GetParameters()
				 where typeof(IRequest).IsAssignableFrom(p.ParameterType)
				 select p).ToList();

			foreach (var requestParameter in requestParameters)
				requestParameter.IsOptional.Should().BeFalse();
		}

		//TODO ensure xml docs on all IElasticClient methods

		//TODO ensure sync methods have exact same arguments (nullable/not nullable) as their async counterparts
	}
}
