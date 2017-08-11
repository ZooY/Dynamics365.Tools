using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using PZone.Xrm.Plugins;
using PZone.Xrm.Plugins.Exceptions;


namespace PZone.StringTools.Plugins
{
    /// <exclude/>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract class AttributesProcessing : PluginBase, IPlugin
    {
        private List<string> _config;


        protected AttributesProcessing(string unsecureConfig) : base(unsecureConfig)
        {
        }


        /// <inheritdoc />
        public override void Configuring(Context context)
        {
            _config = JsonConvert.DeserializeObject<List<string>>(UnsecureConfiguration);
        }


        /// <exclude/>
        protected void SetValues(Context context, Func<string, string> action)
        {
            foreach (var attribute in _config)
            {
                if (!context.Entity.Contains(attribute))
                    continue;
                var value = context.Entity[attribute];
                if (value == null)
                    continue;
                if (value.GetType() != typeof(string))
                    throw new PluginConfigurationException($@"Атрибут ""{attribute}"" не является строковым.");
                context.Entity[attribute] = action(context.Entity[attribute].ToString());
            }
        }
    }
}