// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using RazorLight;

namespace ApiGenerator 
{
	/// <summary> This only exists to make the IDE tooling happy, not actually used to render the templates </summary>
	public class CodeTemplatePage<TModel> : TemplatePage<TModel>
	{
		public override Task ExecuteAsync() => throw new NotImplementedException();

		public Task Execute() => Task.CompletedTask;
	}
}