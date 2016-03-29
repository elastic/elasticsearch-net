using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inferrence
{
	public class IdsInference
	{
		/** # Ids
		 * 
		 * Several places in the elasticsearch API expect an Id object to be passed. This is a special box type that you can implicitly convert to and from many value types.
		 */

		/** Methods that take an Id can be passed longs, ints, strings & Guids and they will implicitly converted to Ids */
		[U] public void CanImplicitlyConvertToId()
		{
			Id idFromInt = 1;
			Id idFromLong = 2L;
			Id idFromString = "hello-world";
			Id idFromGuid = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E");

			Expect(1).WhenSerializing(idFromInt);
			Expect(2).WhenSerializing(idFromLong);
			Expect("hello-world").WhenSerializing(idFromString);
			Expect("d70bd3cf-4e38-46f3-91ca-fcbef29b148e").WhenSerializing(idFromGuid);
		}

		/** Sometimes a method takes an object and we need an Id from that object to build up a path.
		* There is no implicit conversion from any object to Id but we can call Id.From. 
		*
		* Imagine your codebase has the following type that we want to index into elasticsearch
		*/
		class MyDTO
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		[U] public void CanGetIdFromDocument()
		{
			/** By default NEST will try to find a property called `Id` on the class using reflection
			* and create a cached fast func delegate based on the properties getter*/
			var dto = new MyDTO { Id =new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),  Name = "x", OtherName = "y" };
			Expect("d70bd3cf-4e38-46f3-91ca-fcbef29b148e").WhenInferringIdOn(dto);
			
			/** Using the connection settings you can specify a different property NEST should look for ids.
			* Here we instruct NEST to infer the Id for MyDTO based on its Name property */
			WithConnectionSettings(x => x
				.InferMappingFor<MyDTO>(m => m
					.IdProperty(p => p.Name)
				)
			).Expect("x").WhenInferringIdOn(dto);

			/** Even though we have a cache at play the cache is per connection settings, so we can create a different config */
			WithConnectionSettings(x => x
				.InferMappingFor<MyDTO>(m => m
					.IdProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringIdOn(dto);
		}

		/** Another way is to mark the type with an ElasticType attribute, using a string IdProperty */
		[ElasticsearchType(IdProperty = nameof(Name))]
		class MyOtherDTO
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		[U] public void CanGetIdFromAttribute()
		{
			/** Now when we infer the id we expect it to be the Name property without doing any configuration on the ConnectionSettings */
			var dto = new MyOtherDTO { Id =new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),  Name = "x", OtherName = "y" };
			Expect("x").WhenInferringIdOn(dto);
			/** This attribute IS cached statically/globally, however connectionsettings with a config for the type will 
			* still win over this static configuration*/
			/** Eventhough we have a cache at play the cache its per connection settings, so we can create a different config */
			WithConnectionSettings(x => x
				.InferMappingFor<MyOtherDTO>(m => m
					.IdProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringIdOn(dto);
		}
	}
}
