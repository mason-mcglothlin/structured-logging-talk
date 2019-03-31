using System;
using System.Linq;
using System.Reflection;
using Serilog;
using Serilog.Exceptions;

namespace LoggingExamples.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.Enrich.WithThreadId()
				.Enrich.WithMachineName()
				.MinimumLevel.Verbose()
				.WriteTo.Console()
			    .WriteTo.Seq("http://localhost:5341", apiKey: "pt4kgNtNLcKDZIp7RXja") //Production
				//.WriteTo.Seq("http://localhost:5341", apiKey: "iJALLVo402CQZpSaFqaD") //Stage
				//.WriteTo.Seq("http://localhost:5341", apiKey: "au9mcOrmgohdBdd4cSIv") //Alternate Prod
				//.WriteTo.Seq("http://localhost:5341", apiKey: "6d9FxKATDmRZnbf1Z2SY") //Alternate Stage
				.CreateLogger();

			while (true)
			{
				var selectedOperation = ChooseOperation();

				if (string.IsNullOrEmpty(selectedOperation))
				{
					Console.WriteLine("Invalid selection: " + selectedOperation);

				}
				else if (selectedOperation == "exit")
				{
					break;
				}
				else
				{
					Console.Clear();
					RunExample(selectedOperation);

					for (int i = 0; i < 5; i++)
					{
						Console.WriteLine();
					}
				}
			}
		}

		static void RunExample(string methodName)
		{
			var methods = GetExampleMethods();
			var selectedMethod = methods.Single(m => m.Name.Equals(methodName, StringComparison.InvariantCultureIgnoreCase));
			selectedMethod.Invoke(null, null);
		}

		static string ChooseOperation()
		{
			Console.WriteLine("Choose a method:");

			var methods = GetExampleMethods();

			foreach (var method in methods)
			{
				Console.WriteLine("\t" + method.Name);
			}

			var userInput = Console.ReadLine().Trim();

			if (userInput.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
			{
				return "exit";
			}

			var selectedMethod = methods.Select(m => m.Name)
										.SingleOrDefault(m => m.Equals(userInput, StringComparison.InvariantCultureIgnoreCase));


			return selectedMethod;
		}

		static MethodInfo[] GetExampleMethods()
		{
			var methods = typeof(Examples).GetMethods(BindingFlags.Static | BindingFlags.Public);
			return methods;
		}
	}
}
