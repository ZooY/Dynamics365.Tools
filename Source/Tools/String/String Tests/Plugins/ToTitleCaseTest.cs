﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using PZone.StringTools.Plugins;
using PZone.Xrm.Plugins;
using PZone.Xrm.Testing;


namespace PZone.Tests.StringTools.Plugins
{
    [TestClass]
    public class ToTitleCaseTest
    {
        private const string CONFIG_STR = @"
[
    ""firstname"",
    ""lastname"",
    ""middlename""
]";



        [TestMethod]
        public void SimpleData()
        {
            var entity = new Entity("contact", Guid.NewGuid())
            {
                ["firstname"] = "РОМАН",
                ["lastname"] = "копаев-ЗаДуНайский",
                ["middlename"] = "анатольевич"
            };
            IPluginExecutionContext context = new FakePluginExecutionContext(entity);
            IServiceProvider serviceProvider = new FakeServiceProvider(context);

            var plugin = new ToTitleCase(CONFIG_STR);
            plugin.Execute(serviceProvider);

            var result = context.GetContextEntity();
            Assert.AreEqual("Роман", result["firstname"]);
            Assert.AreEqual("Копаев-Задунайский", result["lastname"]);
            Assert.AreEqual("Анатольевич", result["middlename"]);
        }


        [TestMethod]
        public void PartialData()
        {
            var entity = new Entity("contact", Guid.NewGuid())
            {
                ["firstname"] = "рОМАН",
                ["middlename"] = "анатольевич"
            };
            IPluginExecutionContext context = new FakePluginExecutionContext(entity);
            IServiceProvider serviceProvider = new FakeServiceProvider(context);

            var plugin = new ToTitleCase(CONFIG_STR);
            plugin.Execute(serviceProvider);

            var result = context.GetContextEntity();
            Assert.AreEqual("Роман", result.GetAttributeValue<string>("firstname"));
            Assert.IsNull(result.GetAttributeValue<string>("lastname"));
            Assert.AreEqual("Анатольевич", result.GetAttributeValue<string>("middlename"));
        }
    }
}