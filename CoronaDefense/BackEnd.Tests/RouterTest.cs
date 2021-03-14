// <copyright file="RouterTest.cs" company="NTNU: SWA group 1 (2021)">
// Copyright (c) NTNU: SWA group 1 (2021). All rights reserved.
// </copyright>

using BackEnd.APIEndpoint;
using System;
using System.Collections.Generic;
using Xunit;

namespace BackEnd.Tests
{
  /// <summary>
  /// Unit-testing of <see cref="Router"/>.
  /// </summary>
  public class RouterTest
  {
    /// <summary>
    /// Tests that messages sent to the <see cref="Router"/> are rerouted to the correct <see cref="Router.IReceiver"/>s.
    /// </summary>
    [Fact]
    public void MessageTest()
    {
      TestEndpoint endpoint = new TestEndpoint();
      Router router = new Router(endpoint);

      TestReceiver receiver1 = new TestReceiver();
      TestReceiver receiver2 = new TestReceiver();

      long address1 = router.Register(receiver1);
      long address2 = router.Register(receiver2);

      IGlobalMessage message1 = new TestGlobalMessage("One");
      ILocalMessage message2 = new TestLocalMessage(address1, "Two");
      ILocalMessage message3 = new TestLocalMessage(address2, "Three");
      ILocalMessage message4 = new TestLocalMessage(address2, "Four");

      endpoint.SendGlobalMessage(message1);
      endpoint.SendLocalMessage(message2);
      endpoint.SendLocalMessage(message3);
      endpoint.SendLocalMessage(message4);

      ILocalMessage message;
      Assert.True(receiver1.Messages.TryDequeue(out message));
      Assert.True(((TestLocalMessage)message).Message == "Two");

      Assert.True(receiver2.Messages.TryDequeue(out message));
      Assert.True(((TestLocalMessage)message).Message == "Three");
      Assert.True(receiver2.Messages.TryDequeue(out message));
      Assert.True(((TestLocalMessage)message).Message == "Four");
    }

    /// <summary>
    /// Tests that registering <see cref="Router.IReceiver"/>s to <see cref="Router"/>s works correctly.
    /// </summary>
    [Fact]
    public void RegisterTest()
    {
      TestEndpoint endpoint = new TestEndpoint();
      Router router = new Router(endpoint);

      TestReceiver receiver1 = new TestReceiver();
      TestReceiver receiver2 = new TestReceiver();
      TestReceiver receiver3 = new TestReceiver();

      long address1 = router.Register(receiver1);

      long address2 = router.Register(receiver2);
      Assert.NotEqual(address1, address2);

      long address3 = router.Register(receiver3);
      Assert.NotEqual(address1, address3);
      Assert.NotEqual(address2, address3);
    }

    private class TestEndpoint : IAPIEndpoint
    {
      private List<IObserver> Observers { get; } = new List<IObserver>();

      public void AttachObserver(IObserver observer)
      {
        this.Observers.Add(observer);
      }

      public void SendGlobalMessage(IGlobalMessage message)
      {
        foreach (IObserver observer in this.Observers)
        {
          observer.OnGlobalMessage(message);
        }
      }

      public void SendLocalMessage(ILocalMessage message)
      {
        foreach (IObserver observer in this.Observers)
        {
          observer.OnLocalMessage(message);
        }
      }
    }

    private class TestGlobalMessage : IGlobalMessage
    {
      public string Message { get; }

      public TestGlobalMessage(string message)
      {
        this.Message = message;
      }
    }

    private readonly struct TestLocalMessage : ILocalMessage
    {
      public long Address { get; }

      public string Message { get; }

      public TestLocalMessage(long address, string message)
      {
        this.Address = address;
        this.Message = message;
      }
    }

    private class TestReceiver : Router.IReceiver
    {
      public Queue<ILocalMessage> Messages { get; } = new Queue<ILocalMessage>();

      public void OnMessage(ILocalMessage message)
      {
        this.Messages.Enqueue(message);
      }
    }
  }
}
