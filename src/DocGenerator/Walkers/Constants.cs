// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace DocGenerator.Walkers
{
	public class Constants
	{
		public const string CallOutMatcherRegex = @"//[ \t]*(?<callout>\<\d+\>)[ \t]*(?<text>\S.*)";
	}
}
