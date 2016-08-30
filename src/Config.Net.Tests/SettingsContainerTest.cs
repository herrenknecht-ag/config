﻿using System;
using Config.Net.Stores;
using NUnit.Framework;

namespace Config.Net.Tests
{
   [TestFixture]
   public class SettingsContainerTest
   {
      private IConfigStore _store;

      [SetUp]
      public void SetUp()
      {
         _store = new InMemoryConfigStore();
      }

      class MyContainer : SettingsContainer
      {
         #region crap
         private IConfigStore _store;

         public MyContainer(IConfigStore store) : base("MyApp")
         {
            _store = store;
         }
         #endregion

         public Option<TimeSpan> StrongSpan = new Option<TimeSpan>(TimeSpan.FromDays(1));

         public Option<int> Timeout = new Option<int>(2);

         protected override void OnConfigure(IConfigConfiguration configuration)
         {
            configuration.AddStore(_store);
         }
      }

      [Test]
      public void Read_IntegerTimeout_Reads()
      {
         var c = new MyContainer(_store);

         int timeout = c.Timeout;

         Assert.AreEqual(2, timeout);
      }
   }
}