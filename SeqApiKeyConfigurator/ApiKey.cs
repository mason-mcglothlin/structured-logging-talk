using System.Collections.Generic;

namespace SeqApiKeyConfigurator
{
	public class ApiKey
	{
		public string Title { get; set; }

		public string Token { get; set; }

		public List<AppliedProperty> AppliedProperties { get; set; }

		public ApiKey()
		{
			AppliedProperties = new List<AppliedProperty>();
		}
	}

	public class AppliedProperty
	{
		public string Name { get; set; }

		public object Value { get; set; }
	}
}
