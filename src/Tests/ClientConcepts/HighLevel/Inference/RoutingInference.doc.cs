using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class RoutingInference
	{
		/**[[routing-inference]]
		 *=== Routing inference
		 *
		 * ==== Implicit conversion
		 *
		 * You can always create a routing explicitly by relying on the implicit conversion from the following types
		 *
		 * - `Int32`
		 * - `Int64`
		 * - `String`
		 * - `Guid`
		 *
		 * Methods and Properties that take an `Routing` can be passed any of these types and it will be implicitly
		 * converted to an instance of `Routing`
		*/
		[U] public void CanImplicitlyConvertToRouting()
		{
			Routing routingFromInt = 1;
			Routing routingFromLong = 2L;
			Routing routingFromString = "hello-world";
			Routing routingFromGuid = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E");

			Expect(1).WhenSerializing(routingFromInt);
			Expect(2).WhenSerializing(routingFromLong);
			Expect("hello-world").WhenSerializing(routingFromString);
			Expect("d70bd3cf-4e38-46f3-91ca-fcbef29b148e").WhenSerializing(routingFromGuid);
		}

		/**
		* ==== Inferring from a type
		*
		* The real power of the `Routing` is in the inference rules (the default inferred routing for an object will be null).
		* Lets look at an example of this given the following POCO:
		*/
		class MyDTO
		{
			public Guid Routing { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		[U] public void CanGetRoutingFromDocument()
		{
			/** By default NEST will try to find a property called `Routing` on the class using reflection
			* and create a cached delegate based on the property getter
			*/
			var dto = new MyDTO
			{
				Routing = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
				Name = "x",
				OtherName = "y"
			};
			Expect(null).WhenInferringRoutingOn(dto);

			/**
			 * Using connection settings, you can specify a property that NEST should use to infer Routing for the document.
			* Here we instruct NEST to infer the Routing for `MyDTO` based on its `Name` property
			*/
			WithConnectionSettings(x => x
				.DefaultMappingFor<MyDTO>(m => m
					.RoutingProperty(p => p.Name)
				)
			).Expect("x").WhenInferringRoutingOn(dto);

			/** IMPORTANT: Inference rules are cached __per__ `ConnectionSettings` instance.
			*
			* Because the cache is per `ConnectionSettings` instance, we can create another `ConnectionSettings` instance
			* with different inference rules
			*/
			WithConnectionSettings(x => x
				.DefaultMappingFor<MyDTO>(m => m
					.RoutingProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringRoutingOn(dto);
		}

		/**
		* ==== JoinField
		*
		* If your class has a property of type JoinField, NEST will automatically infer the parentid as the routing value.
		 *
		* The name of this property can be anything. Be sure the read the <<parent-child-relationships, section on Parent/Child relationships>> to get a complete
		 * walkthrough on using Parent Child joins with NEST.
		*/
		class MyOtherDTO
		{
			public JoinField SomeJoinField { get; set; }
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		[U] public void CanGetRoutingFromJoinField()
		{
			/** here we link this instance of `MyOtherDTO` with its parent id `"8080"` */
			var dto = new MyOtherDTO
			{
				SomeJoinField = JoinField.Link<MyOtherDTO>("8080"),
				Id = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
				Name = "x",
				OtherName = "y"
			};
			Expect("8080").WhenInferringRoutingOn(dto);

			/**
			 * Here we link this instance as the root (parent) of the relation. NEST infers that the default routing for this instance
			 * should be the Id of the document itself.
			 */
			dto = new MyOtherDTO
			{
				SomeJoinField = JoinField.Root<MyOtherDTO>(),
				Id = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
				Name = "x",
				OtherName = "y"
			};
			Expect("d70bd3cf-4e38-46f3-91ca-fcbef29b148e").WhenInferringRoutingOn(dto);

			/**
			* ==== Precedence of ConnectionSettings
			*
			* The routing property configured on `ConnectionSettings` always takes precedence.
			*
			*/
			WithConnectionSettings(x => x
				.DefaultMappingFor<MyOtherDTO>(m => m
					.RoutingProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringRoutingOn(dto);
		}
		class BadDTO
		{
			public JoinField SomeJoinField { get; set; }
			public JoinField AnotherJoinField { get; set; }
			public string ParentName { get; set; }
		}
		[U] public void DuplicateJoinField()
		{
			/**
			 * A class cannot contain more than one property of type JoinField, an exception is thrown in this case
			 */
			var dto = new BadDTO
			{
				SomeJoinField = JoinField.Link<MyOtherDTO>("8080"),
				AnotherJoinField = JoinField.Link<MyOtherDTO>("8081"),
				ParentName = "my-parent"
			};
			Action resolve = () => Expect("8080").WhenInferringRoutingOn(dto);
			resolve.ShouldThrow<ArgumentException>().WithMessage("BadDTO has more than one JoinField property");

			/** unless you configure the ConnectionSettings to use an alternate property: */
			WithConnectionSettings(x => x
				.DefaultMappingFor<BadDTO>(m => m
					.RoutingProperty(p => p.ParentName)
				)
			).Expect("my-parent").WhenInferringRoutingOn(dto);
		}
	}
}
