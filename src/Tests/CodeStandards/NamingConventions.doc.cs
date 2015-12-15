using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest;

namespace Tests.CodeStandards
{
	public class NamingConventions
	{
		/**
		* Abstract class names should end with a `Base` suffix
		*/
		//[U]
		public void AbstractClassNamesEndWithBase()
		{
			var abstractClasses = Assembly.Load("Nest").GetTypes()
				.Where(t => t.IsClass && t.IsAbstract && !t.IsSealed)
				.Select(t => t.Name.Split('`')[0])
				.ToList();

			foreach (var abstractClass in abstractClasses)
				abstractClass.Should().EndWith("Base");
		}

		/**
		* Request class names should end with "Request"
		*/
		//[U]
		public void RequestClassNamesEndWithRequest()
		{
			var types = Assembly.Load("Nest").GetTypes();
			var requests = types
				.Where(t => typeof(IRequest).IsAssignableFrom(t))
				.Select(t => t.Name.Split('`')[0])
				.ToList();
			foreach (var request in requests)
				request.Should().EndWith("Request");
		}

		/**
		* Response class names should end with "Response"
		**/
		//[U]
		public void ResponseClassNamesEndWithResponse()
		{
			var types = Assembly.Load("Nest").GetTypes();
			var responses = types
				.Where(t => typeof(IResponse).IsAssignableFrom(t))
				.Select(t => t.Name.Split('`')[0])
				.ToList();
			foreach (var response in responses)
				response.Should().EndWith("Response");
		}

		/**
		* Request and Response class names should be one to one in *most* cases.
		* e.g. ValidateRequest => ValidateResponse, and not ValidateQueryRequest => ValidateResponse
		*/
		//[U]
		public void ParityBetweenRequestsAndResponses()
		{
			var types = Assembly.Load("Nest").GetTypes();

			var requests = new HashSet<string>(types
				.Where(t => t.IsClass && !t.IsAbstract && typeof(IRequest).IsAssignableFrom(t) && !(t.Name.EndsWith("Descriptor")))
				.Select(t => t.Name.Split('`')[0].Replace("Request", ""))
			);

			var responses = types
				.Where(t => t.IsClass && !t.IsAbstract && typeof(IResponse).IsAssignableFrom(t))
				.Select(t => t.Name.Split('`')[0].Replace("Response", ""));

			// Add any exceptions to the rule here
			var exceptions = new string[] { "Cat" };

			responses = responses.Where(r => !exceptions.Contains(r)).ToList();

			foreach (var response in responses)
				requests.Should().Contain(response);
		}
	}
}
