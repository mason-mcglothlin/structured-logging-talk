using System;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Serilog;
using Serilog.Context;

namespace LoggingExamples.ConsoleApp
{
	public class Examples
	{
		public static void PlainTextAppending()
		{
			var logPath = Path.GetTempFileName();
			var purchaseOrderNumber = 123456;
			File.AppendAllText(logPath, $"{DateTime.Now:G} - Purchase Order {purchaseOrderNumber} received");
		}

		public static void SerilogUnstructured()
		{
			var purchaseOrderNumber = 123456;
			Log.Information($"Purchase Order {purchaseOrderNumber} received");
		}

		public static void SerilogStructured()
		{
			var purchaseOrderNumber = 123456;
			Log.Information("Purchase Order {PurchaseOrderNumber} received", purchaseOrderNumber);
		}

		public static void SerilogDestructuring()
		{
			var mockHttpRequest = MockHttpRequest.Create();

			Log.Verbose("Incoming Http Request {@HttpRequest}", mockHttpRequest);
		}

		public static void SerilogEnriched()
		{
			using (LogContext.PushProperty("RequestId", Guid.NewGuid()))
			{
				var mockHttpRequest = MockHttpRequest.Create();

				Log.Verbose("Incoming Http Request {@HttpRequest}", mockHttpRequest);

				var purchaseOrderNumber = 123456;

				using (LogContext.PushProperty("PurchaseOrderNumber", purchaseOrderNumber))
				{
					Log.Information("Purchase Order received");

					Log.Debug("Saving purchase order to database");

					try
					{
						using (var connection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=LoggingExamplesDb;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\\LoggingExamplesDb.mdf"))
						{
							connection.Execute("dbo.SomeStoredProcedure");
						}
					}
					catch (Exception exception)
					{
						Log.Error(exception, "Error saving purchase order");
					}
				}
			}
		}
	}
}
