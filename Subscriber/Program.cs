using NServiceBus;
using System;

namespace Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Subscriber";
			Configure.Transactions.Enable();

			Configure.With()
				.DefineEndpointName("subscriber.input")
				.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"))
				.DefaultBuilder()
				.Log4Net()
				.UseTransport<Msmq>()
				.MsmqSubscriptionStorage("subscriber")
				.UnicastBus()
					.LoadMessageHandlers()
				.CreateBus()
				.Start(
					() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

			ConsoleKeyInfo input;
			do
			{
				input = Console.ReadKey();
			}
			while (input.Key != ConsoleKey.Escape);


		}
	}
}