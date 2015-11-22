using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using FluentAssertions;

namespace Tests.CodeStandards
{
	public class ElasticClient
	{
		/*
		* Fluent methods on IElasticClient (Func<Descriptor, Interface>) should be named `selector`.
		*/
		//[U]
		public void ConsistentFluentParameterNames()
		{
			var descriptorParameters =
				from m in typeof(IElasticClient).GetMethods()
				from p in m.GetParameters()
				where p.ParameterType.BaseType == typeof(MulticastDelegate)
				select p;

			foreach (var descriptorParameter in descriptorParameters)
				descriptorParameter.Name.Should().Be("selector");
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
