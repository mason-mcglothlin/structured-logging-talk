using System.Collections.Generic;

namespace LoggingExamples.ConsoleApp
{
	public class MockHttpRequest
	{
		public string Url { get; set; }

		public string Method { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public MockHttpRequest()
		{
			Headers = new Dictionary<string, string>();
		}

		public static MockHttpRequest Create()
		{
			return new MockHttpRequest
			{
				Method = "POST",
				Url = "/api/PurchaseOrders",
				Headers = {
					{ "accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8" },
					{ "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.121 Safari/537.36" }
				}
			};
		}
	}
}
