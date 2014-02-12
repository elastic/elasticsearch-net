using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Diagnostics;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce325Tests : IntegrationTests
	{

		/// <summary>
		///	https://github.com/Mpdreamz/NEST/issues/325
		/// </summary>
		[Test]
		public void FluentMappingReturnsResults()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			this._client.CreateIndex(indexName, settings => settings
				.Settings(s => s.Add("search.slowlog.threshold.fetch.warn", "1s"))
				.Analysis(x =>
				{
					var descriptor = x.Analyzers(i => i.Add("autocomplete", new CustomAnalyzer
					{
						Tokenizer = new WhitespaceTokenizer().Type,
						Filter = new[] { new LowercaseTokenFilter().Type, "engram" }
					}));

					descriptor.TokenFilters(i => i.Add("engram", new EdgeNGramTokenFilter
					{
						MinGram = 3,
						MaxGram = 10
					}
					));

					return descriptor;
				})
				.AddMapping<TechnicalProduct>(m => MapTechnicalProduct(m, indexName)));

		}


		private static PutMappingDescriptor<TechnicalProduct> MapTechnicalProduct(PutMappingDescriptor<TechnicalProduct> m, string indexName)
		{
			return m
				.Index(indexName)
				.Type("technicalProducts")
				.DateDetection()
				.NumericDetection()
				.DynamicDateFormats(new[] { "dateOptionalTime", "yyyy/MM/dd HH:mm:ss Z||yyyy/MM/dd Z" })
				.Dynamic(false)
				.IdField(i => i
					.SetIndex("not_analyzed")
					.SetStored()
				)
				.Properties(o => o
					.String(i => i
						.Name(x => x.Name)
						.Index(FieldIndexOption.analyzed)
						.IndexAnalyzer("autocomplete")
						.SearchAnalyzer("standard")
					)
					.String(i => i
						.Name(x => x.Brand)
						.Index(FieldIndexOption.analyzed)
						.IndexAnalyzer("autocomplete")
						.SearchAnalyzer("standard")
					)
				);
		}

		public class TechnicalProduct : Product
		{
			public virtual int NumberProcessorCores { get; protected set; }
			public virtual decimal ProcessorSpeed { get; protected set; }
			public virtual decimal BatteryLife { get; protected set; }
			public virtual int HardDriveSize { get; protected set; }
			public virtual decimal ScreenSize { get; protected set; }
			public virtual decimal Memory { get; protected set; }
			public virtual int HardDriveSpeed { get; protected set; }


			protected TechnicalProduct() { }

			public TechnicalProduct(string brand, string name, string imageUrl, decimal price, int numberCores, decimal processorSpeed, decimal batteryLife, int hardDriveSize, decimal screenSize,
								decimal memory, int hardDriveSpeed)
				: base(brand, name, price, imageUrl)
			{
				NumberProcessorCores = numberCores;
				ProcessorSpeed = processorSpeed;
				BatteryLife = batteryLife;
				Price = price;
				HardDriveSize = hardDriveSize;
				ScreenSize = screenSize;
				Memory = memory;
				HardDriveSpeed = hardDriveSpeed;
			}
		}

		public abstract class Product : Entity
		{
			private readonly Dictionary<string, string> _additionalAttributes;

			public virtual string Brand { get; protected set; }
			public virtual decimal Price { get; protected set; }
			public virtual string ImageUrl { get; protected set; }
			public virtual string Name { get; protected set; }

			public virtual IDictionary<string, string> AdditionalAttributes
			{
				get { return _additionalAttributes; }
			}

			protected Product()
			{
				_additionalAttributes = new Dictionary<string, string>();
			}

			protected Product(string brand, string name, decimal price, string imageUrl)
				: this()
			{
				Brand = brand;
				Name = name;
				Price = price;
				ImageUrl = imageUrl;
			}
		}

		public abstract class Entity
		{
			private int? _oldHashCode;

			public virtual int Id { get; protected set; }

			public virtual bool Equals(Entity other)
			{
				return this == other;
			}

			public static bool operator ==(Entity left, Entity right)
			{
				return Equals(left, right);
			}

			public static bool operator !=(Entity left, Entity right)
			{
				return !Equals(left, right);
			}
		}
	}
}
