using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Common;
using Nest;
using Tests.Framework;
using System.Reflection;

namespace Tests.CodeStandards
{
	public class ElasticClientStandards
	{
		/*
		* Fluent methods on IElasticClient (Func<Descriptor, Interface>) should be named `selector`.
		*/
		[U] public void ConsistentFluentParameterNames()
		{
			var fluentParametersNotNamedSelector =
				from m in typeof (IElasticClient).GetMethods()
				from p in m.GetParameters()
				where p.ParameterType.BaseType() == typeof (MulticastDelegate)
				where !p.Name.Equals("selector")
				select $"method '{nameof(IElasticClient)}.{m.Name}' should have parameter name of 'selector' but has a name of '{p.Name}'";

			fluentParametersNotNamedSelector.Should().BeEmpty();
		}

		/*
		* Similarly, OIS methods on IElasticClient (IRequest) should be named `request`.
		*/
		[U] public void ConsistentInitializerParameterNames()
		{
			var requestParametersNotNamedRequest =
				from m in typeof(IElasticClient).GetMethods()
				from p in m.GetParameters()
				where typeof(IRequest).IsAssignableFrom(p.ParameterType)
				where !p.Name.Equals("request")
				select $"method '{nameof(IElasticClient)}.{m.Name}' should have parameter name of 'request' but has a name of '{p.Name}'";

			requestParametersNotNamedRequest.Should().BeEmpty();
		}

		/*
		* Request objects on OIS methods are always required, and so they shouldn't be nullable
		*/
		[U] public void InitializerRequestsAreNotOptional()
		{
			var requestParameters =
				(from m in typeof(IElasticClient).GetMethods()
				 from p in m.GetParameters()
				 where typeof(IRequest).IsAssignableFrom(p.ParameterType)
				 select p).ToList();

			foreach (var requestParameter in requestParameters)
				requestParameter.HasDefaultValue.Should().BeFalse();
		}

        //TODO ensure xml docs on all IElasticClient methods

        /*
        * The parameter names to ElasticClient methods and whether they are optional should match
        * the interface method definitions
        */
        [U] public void ConcreteClientOptionalParametersMatchInterfaceClient()
	    {
	        var concreteMethodParametersDoNotMatchInterface = new List<string>();
            var interfaceMap = typeof(ElasticClient).GetInterfaceMap(typeof(IElasticClient));

            foreach (var interfaceMethodInfo in typeof(IElasticClient).GetMethods())
	        {
	            var indexOfInterfaceMethod = Array.IndexOf(interfaceMap.InterfaceMethods, interfaceMethodInfo);
	            var concreteMethod = interfaceMap.TargetMethods[indexOfInterfaceMethod];

	            var concreteParameters = concreteMethod.GetParameters();
                var interfaceParameters = interfaceMethodInfo.GetParameters();

	            for (int i = 0; i < concreteParameters.Length; i++)
	            {
	                var parameterInfo = concreteParameters[i];
	                var interfaceParameter = interfaceParameters[i];

                    parameterInfo.Name.Should().Be(interfaceParameter.Name);

                    if (parameterInfo.HasDefaultValue != interfaceParameter.HasDefaultValue)
                        concreteMethodParametersDoNotMatchInterface.Add(
                            $"'{interfaceParameter.Name}' parameter on concrete implementation of '{nameof(ElasticClient)}.{interfaceMethodInfo.Name}' to {(interfaceParameter.HasDefaultValue ? string.Empty : "NOT")} be optional");
                }
	        }

	        concreteMethodParametersDoNotMatchInterface.Should().BeEmpty();
	    }

        /*
        * Synchronous and Asynchronous methods should be consistent 
        * with regards to optional parameters
        */
        [U]
        public void ConsistentOptionalParametersForSyncAndAsyncMethods()
        {
            var methodGroups =
                from methodInfo in typeof(IElasticClient).GetMethods()
                where
                    typeof(IResponse).IsAssignableFrom(methodInfo.ReturnType) ||
                    (methodInfo.ReturnType.IsGeneric()
                     && typeof(Task<>) == methodInfo.ReturnType.GetGenericTypeDefinition()
                     && typeof(IResponse).IsAssignableFrom(methodInfo.ReturnType.GetGenericArguments()[0]))
                let method = new MethodWithRequestParameter(methodInfo)
                group method by method.Name into methodGroup
                select methodGroup;

            foreach (var methodGroup in methodGroups)
            {
                foreach (var asyncMethod in methodGroup.Where(g => g.IsAsync))
                {
                    var parameters = asyncMethod.MethodInfo.GetParameters();

                    var syncMethod = methodGroup.First(g =>
                        !g.IsAsync
                        && g.MethodType == asyncMethod.MethodType
                        && g.MethodInfo.GetParameters().Length == parameters.Length
                        && (!asyncMethod.MethodInfo.IsGenericMethod ||
                            g.MethodInfo.GetGenericArguments().Length == asyncMethod.MethodInfo.GetGenericArguments().Length));

                    asyncMethod.Parameter.HasDefaultValue.Should().Be(syncMethod.Parameter.HasDefaultValue,
                        $"sync and async versions of {asyncMethod.MethodType} '{nameof(ElasticClient)}{methodGroup.Key}' should match");
                }
            }
        }

        private enum ClientMethodType
        {
            Fluent,
            Initializer
        }

        private class MethodWithRequestParameter
	    {
            public string Name { get; }

	        public MethodInfo MethodInfo { get; }

            public bool IsAsync { get; }

            public ClientMethodType MethodType { get; }

	        public ParameterInfo Parameter { get; }

	        public MethodWithRequestParameter(MethodInfo methodInfo)
	        {
	            Name = methodInfo.Name.EndsWith("Async")
	                ? methodInfo.Name.Substring(0, methodInfo.Name.Length - "Async".Length)
	                : methodInfo.Name;

	            IsAsync = methodInfo.ReturnType.IsGeneric() &&
	                      methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);

                MethodInfo = methodInfo;

	            var parameterInfo = methodInfo.GetParameters()
                    .FirstOrDefault(p => typeof(IRequest).IsAssignableFrom(p.ParameterType));

	            if (parameterInfo != null)
	            {
                    Parameter = parameterInfo;
                    MethodType = ClientMethodType.Initializer;
                }
	            else
	            {
	                Parameter = methodInfo.GetParameters()
	                    .First(p => p.ParameterType.BaseType() == typeof(MulticastDelegate));
                    MethodType = ClientMethodType.Fluent;
	            }
	        }
	    }
	}
}
