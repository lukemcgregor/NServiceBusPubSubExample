using Messages.Events;
using NServiceBus;
using System;

namespace Subscriber
{
	class SomethingHappenedHandler : IHandleMessages<SomethingHappened>
	{
		public void Handle(SomethingHappened message)
		{
			Console.WriteLine("Something happened event recieved with: " + message.SomeText);
		}
	}
}
