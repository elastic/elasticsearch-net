using System;
using System.Collections.Generic;
using System.Linq;
using ApiGenerator.Overrides;
using ApiGenerator.Overrides.Descriptors;

namespace ApiGenerator.Domain
{
	public static class ApiQueryParametersPatcher
	{

		public static Dictionary<string, ApiQueryParameters> Patch(
			string urlPath,
			IDictionary<string, ApiQueryParameters> source,
			IEndpointOverrides overrides,
			bool checkCommon = true)
		{
			if (source == null) return null;

			var globalOverrides = new GlobalOverrides();
			var declaredKeys = source.Keys;
			var skipList = CreateSkipList(globalOverrides, overrides, declaredKeys);
			var partialList = CreatePartialList(globalOverrides, overrides, declaredKeys);

			var renameLookup = CreateRenameLookup(globalOverrides, overrides, declaredKeys);
			var obsoleteLookup = CreateObsoleteLookup(globalOverrides, overrides, declaredKeys);

			var patchedParams = new Dictionary<string, ApiQueryParameters>();
			var name = overrides?.GetType().Name ?? urlPath ?? "unknown";
			foreach (var kv in source)
			{
				var queryStringKey = kv.Key;
				kv.Value.QueryStringKey = queryStringKey;

				if (checkCommon && RestApiSpec.CommonApiQueryParameters.Keys.Contains(queryStringKey))
				{
					ApiGenerator.Warnings.Add($"key '{queryStringKey}' in {name} is already declared in _common.json");
					continue;
				}

				if (!renameLookup.TryGetValue(queryStringKey, out var preferredName)) preferredName = kv.Key;
				kv.Value.ClsName = CreateCSharpName(preferredName);

				if (skipList.Contains(queryStringKey)) continue;

				if (partialList.Contains(queryStringKey)) kv.Value.RenderPartial = true;

				if (obsoleteLookup.TryGetValue(queryStringKey, out var obsolete)) kv.Value.Obsolete = obsolete;

				//make sure source_enabled takes a boolean only
				if (preferredName == "source_enabled") kv.Value.Type = "boolean";

				patchedParams.Add(preferredName, kv.Value);
			}

			return patchedParams;

		}

		private static string CreateCSharpName(string queryStringKey)
		{
			if (string.IsNullOrWhiteSpace(queryStringKey)) return "UNKNOWN";

            var cased = queryStringKey.ToPascalCase();
			switch (cased)
			{
				case "Type":
				case "Index":
				case "Source":
				case "Script":
					return cased + "QueryString";
				default:
					return cased;
			}
		}

		private static IList<string> CreateSkipList(IEndpointOverrides global, IEndpointOverrides local, ICollection<string> declaredKeys) =>
			CreateList(global, local, "skip", e => e.SkipQueryStringParams, declaredKeys);

		private static IList<string> CreatePartialList(IEndpointOverrides global, IEndpointOverrides local, ICollection<string> declaredKeys) =>
			CreateList(global, local, "partial", e => e.RenderPartial, declaredKeys);

		private static IDictionary<string, string> CreateLookup(IEndpointOverrides global, IEndpointOverrides local, string type,
			Func<IEndpointOverrides, IDictionary<string, string>> @from, ICollection<string> declaredKeys)
		{
			var d = new Dictionary<string, string>();
			foreach (var kv in from(global)) d[kv.Key] = kv.Value;

			if (local == null) return d;

			var localDictionary = from(local);
			foreach (var kv in localDictionary) d[kv.Key] = kv.Value;

			var name = local.GetType().Name;
			foreach (var p in localDictionary.Keys.Except(declaredKeys))
				ApiGenerator.Warnings.Add($"On {name} {type} key '{p}' is not found in spec");

			return d;
		}

		private static IList<string> CreateList(IEndpointOverrides global, IEndpointOverrides local, string type,
			Func<IEndpointOverrides, IEnumerable<string>> @from, ICollection<string> declaredKeys)
		{
			var list = new List<string>();
			if (global != null) list.AddRange(from(global));
			if (local != null)
			{
				var localList = from(local).ToList();
				list.AddRange(localList);
				var name = local.GetType().Name;
				foreach (var p in localList.Except(declaredKeys))
					ApiGenerator.Warnings.Add($"On {name} {type} key '{p}' is not found in spec");
			}
			return list.Distinct().ToList();
		}

		private static IDictionary<string, string> CreateRenameLookup(IEndpointOverrides global, IEndpointOverrides local,
			ICollection<string> declaredKeys) =>
			CreateLookup(global, local, "rename", e => e.RenameQueryStringParams, declaredKeys);

		private static IDictionary<string, string> CreateObsoleteLookup(IEndpointOverrides global, IEndpointOverrides local,
			ICollection<string> declaredKeys) =>
			CreateLookup(global, local, "obsolete", e => e.ObsoleteQueryStringParams, declaredKeys);

	}
}
