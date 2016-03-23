using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards
{
	/** == Naming Conventions
	* 
	* NEST uses the following naming conventions (with _some_ exceptions).
	*/
	public class NamingConventions
	{
		/** === Class Names
		*
		* Abstract class names should end with a `Base` suffix
		*/
		[U] public void AbstractClassNamesEndWithBase()
		{
			var exceptions = new[]
			{
				typeof(DateMath)
			};

			var abstractClasses = typeof(IRequest).Assembly().GetTypes()
				.Where(t => t.IsClass() && t.IsAbstract() && !t.IsSealed() && !exceptions.Contains(t))
				.Where(t => !t.Name.Split('`')[0].EndsWith("Base"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			abstractClasses.Should().BeEmpty();
		}

		/**
		* Class names that end with `Base` suffix are abstract
		*/
		[U] public void ClassNameContainsBaseShouldBeAbstract()
		{
			var exceptions = new[] { typeof(DateMath) };

			var baseClassesNotAbstract = typeof(IRequest).Assembly().GetTypes()
				.Where(t => t.IsClass() && !exceptions.Contains(t))
				.Where(t => t.Name.Split('`')[0].EndsWith("Base"))
				.Where(t => !t.IsAbstractClass())
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			baseClassesNotAbstract.Should().BeEmpty();
		}

		/** === Requests and Responses
		*
		* Request class names should end with `Request`
		*/
		[U]
		public void RequestClassNamesEndWithRequest()
		{
			var types = typeof(IRequest).Assembly().GetTypes();
			var requests = types
				.Where(t => typeof(IRequest).IsAssignableFrom(t) && !t.IsAbstract())
				.Where(t => !typeof(IDescriptor).IsAssignableFrom(t))
				.Where(t => !t.Name.Split('`')[0].EndsWith("Request"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			requests.Should().BeEmpty();
		}

		/**
		* Response class names should end with `Response`
		**/
		[U]
		public void ResponseClassNamesEndWithResponse()
		{
			var types = typeof(IRequest).Assembly().GetTypes();
			var responses = types
				.Where(t => typeof(IResponse).IsAssignableFrom(t) && !t.IsAbstract())
				.Where(t => !t.Name.Split('`')[0].EndsWith("Response"))
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			responses.Should().BeEmpty();
		}

		/**
		* Request and Response class names should be one to one in *most* cases.
		* e.g. `ValidateRequest` => `ValidateResponse`, and not `ValidateQueryRequest` => `ValidateResponse`
		* There are a few exceptions to this rule, most notably the `Cat` prefixed requests and
		* the `Exists` requests.
		*/
		[U]
		public void ParityBetweenRequestsAndResponses()
		{
			var exceptions = new[] // <1> _Exceptions to the rule_
			{
				typeof(DocumentExistsRequest),
				typeof(DocumentExistsRequest<>),
				typeof(AliasExistsRequest),
				typeof(IndexExistsRequest),
				typeof(TypeExistsRequest),
				typeof(IndexTemplateExistsRequest),
				typeof(SearchExistsRequest),
				typeof(SearchExistsRequest<>),
				typeof(SearchTemplateRequest),
				typeof(SearchTemplateRequest<>),
				typeof(ScrollRequest),
				typeof(SourceRequest),
				typeof(SourceRequest<>),
				typeof(ValidateQueryRequest<>),
				typeof(GetAliasRequest),
#pragma warning disable 612
				typeof(CatNodeattrsRequest),
#pragma warning restore 612
				typeof(IndicesShardStoresRequest),
				typeof(RenderSearchTemplateRequest)
			};

			var types = typeof(IRequest).Assembly().GetTypes();

			var requests = new HashSet<string>(types
				.Where(t =>
					t.IsClass() &&
					!t.IsAbstract() &&
					typeof(IRequest).IsAssignableFrom(t) &&
					!typeof(IDescriptor).IsAssignableFrom(t)
					&& !t.Name.StartsWith("Cat")
					&& !exceptions.Contains(t))
				.Select(t => t.Name.Split('`')[0].Replace("Request", ""))
			);

			var responses = types
				.Where(t => t.IsClass() && !t.IsAbstract() && typeof(IResponse).IsAssignableFrom(t))
				.Select(t => t.Name.Split('`')[0].Replace("Response", ""));

			requests.Except(responses).Should().BeEmpty();
		}
	}
}
