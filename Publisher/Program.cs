using Messages.Events;
using NServiceBus;
using System;

namespace Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			#region NServiceBus Config
			Console.Title = "Publisher";
			Configure.Transactions.Enable();

			var bus = Configure.With()
				.DefineEndpointName("publisher.input")
				.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"))
				.DefaultBuilder()
				.Log4Net()
				.UseTransport<Msmq>()
				.MsmqSubscriptionStorage("publisher")
				.UnicastBus()
					.LoadMessageHandlers()
				.CreateBus()
				.Start(
					() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
			#endregion

			string input = "";
			while (input != "q")
			{
				Console.WriteLine("Type something to send (or q to quit)");

				input = Console.ReadLine();
				bus.Publish(new SomethingHappened() { SomeText = input });

				Console.WriteLine("Sending something happened: " + input);
			}
		}
	}

}