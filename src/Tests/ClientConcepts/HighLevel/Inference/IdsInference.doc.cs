using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class IdsInference
	{
		/**[[ids-inference]]
		 *== Ids Inference
		 *
		 * === Implicit Conversions
		 *
		 * Several places in the Elasticsearch API expect an `Id` object to be passed.
		 * This is a special box type that you can implicitly convert to from the following types
		 *
		 * - `Int32`
		 * - `Int64`
		 * - `String`
		 * - `Guid`
		 *
		 * Methods that take an `Id` can be passed any of these types and it will be implicitly converted to an `Id`
		*/
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

		/** === Inferring from a Type
		*
		* Sometimes a method takes an object and we need an Id from that object to build up a path.
		* There is no implicit conversion from any object to Id but we can call `Id.From`.
		*
		* Imagine your codebase has the following type that we want to index into Elasticsearch
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
			* and create a cached fast func delegate based on the properties getter
			*/
			var dto = new MyDTO
			{
				Id = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
				Name = "x",
				OtherName = "y"
			};

			Expect("d70bd3cf-4e38-46f3-91ca-fcbef29b148e").WhenInferringIdOn(dto);

			/** Using the connection settings you can specify a different property that NEST should use to infer the document Id.
			* Here we instruct NEST to infer the Id for `MyDTO` based on its `Name` property
			*/
			WithConnectionSettings(x => x
				.InferMappingFor<MyDTO>(m => m
					.IdProperty(p => p.Name)
				)
			).Expect("x").WhenInferringIdOn(dto);

			/** IMPORTANT: Inference rules are cached __per__ `ConnectionSettings` instance.
			*
			* Because the cache is per `ConnectionSettings` instance, we can create another `ConnectionSettings` instance
			* with different inference rules
			*/
			WithConnectionSettings(x => x
				.InferMappingFor<MyDTO>(m => m
					.IdProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringIdOn(dto);
		}

		/** === Using the ElasticsearchType attribute
		*
		* Another way is to mark the type with an `ElasticsearchType` attribute, setting `IdProperty`
		* to the name of the property that should be used for the document id
		*/
		[ElasticsearchType(IdProperty = nameof(Name))]
		class MyOtherDTO
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		[U] public void CanGetIdFromAttribute()
		{
			/** Now when we infer the id we expect it to be the value of the `Name` property without doing any configuration on the `ConnectionSettings` */
			var dto = new MyOtherDTO
			{
				Id = new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
				Name = "x",
				OtherName = "y"
			};

			Expect("x").WhenInferringIdOn(dto);

			/** === Using Mapping inference on ConnectionSettings
			*
			* This attribute *is* cached statically/globally, however an inference rule on the `ConnectionSettings` for the type will
			* still win over the attribute. Here we demonstrate this by creating a different `ConnectionSettings` instance
			* that will infer the document id from the property `OtherName`:
			*/
			WithConnectionSettings(x => x
				.InferMappingFor<MyOtherDTO>(m => m
					.IdProperty(p => p.OtherName)
				)
			).Expect("y").WhenInferringIdOn(dto);
		}
	}
}
