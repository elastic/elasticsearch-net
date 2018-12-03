using System;
using System.Runtime.Serialization;

//namespace Nest
//{
//	internal static class JsonExtensions
//	{
//
//		public static TConverter GetStatefulConverter<TConverter>(this JsonSerializer serializer)
//			where TConverter : JsonConverter
//		{
//			var resolver = serializer.ContractResolver as ElasticContractResolver;
//			var realConverter = resolver?.PiggyBackState?.ActualJsonConverter as TConverter;
//			return realConverter;
//		}
//	}
//}
