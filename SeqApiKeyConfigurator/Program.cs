using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Seq.Api;
using Seq.Api.Model.Inputs;

namespace SeqApiKeyConfigurator
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await LoadKeys();

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		static async Task LoadKeys()
		{
			var connection = new SeqConnection("http://localhost:5341");
			var keysJson = File.ReadAllText("ApiKeys.json");
			var keysToLoad = JsonConvert.DeserializeObject<List<ApiKey>>(keysJson);

			foreach (var keyToLoad in keysToLoad)
			{
				Console.WriteLine($"Adding key {keyToLoad.Title}");
				var template = await connection.ApiKeys.TemplateAsync();
				template.Title = keyToLoad.Title;
				template.Token = keyToLoad.Token;
				template.AppliedProperties = keyToLoad.AppliedProperties.Select(p => new InputAppliedPropertyPart { Name = p.Name, Value = p.Value }).ToList();
				await connection.ApiKeys.AddAsync(template);
			}
		}

		static async Task RetrieveKeys()
		{
			var connection = new SeqConnection("http://masonaero:5341");

			var keysToSave = new List<ApiKey>();

			foreach (var apiKey in await connection.ApiKeys.ListAsync())
			{
				if (apiKey.Title == "None")
				{
					continue;
				}

				var persistedApiKey = new ApiKey
				{
					Title = apiKey.Title,
					Token = apiKey.Token,
					AppliedProperties = apiKey.AppliedProperties.Select(p => new AppliedProperty { Name = p.Name, Value = p.Value }).ToList()
				};

				keysToSave.Add(persistedApiKey);
			}

			File.WriteAllText(@"C:\Code\LoggingExamples\SeqApiKeyConfigurator\ApiKeys.json", JsonConvert.SerializeObject(keysToSave, Formatting.Indented));
		}
	}
}
