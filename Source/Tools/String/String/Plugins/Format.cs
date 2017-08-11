using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using PZone.Xrm;
using PZone.Xrm.Plugins;


namespace PZone.StringTools.Plugins
{
    /// <summary>
    /// Формирование значения строкового атрибута на основе строки формата.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Плагин позволяет установить значение строкового атрибута в соответствии со строкой формата на основе вычисляемых значений.
    /// </para>
    /// <para>
    /// Настройка плагина производиться с помощью строки в формате JSON, определяемой при регистрации плагина. Строка имеет следующий формат:
    /// <code>
    /// {
    ///     attribute1_name: {
    ///         Format: "format_string",
    ///             Args: [
    ///                 "attribute1",
    ///                 "attribute2",
    ///                 ...
    ///                 "attributeN"
    ///             ]
    ///         },
    ///     ...
    ///     attributeN_name: {
    ///         Format: "format_string",
    ///         Args: [
    ///             "attribute1",
    ///             "attribute2",
    ///             ...
    ///             "attributeN"
    ///         ]
    ///     }
    /// }
    /// </code>
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Узел</term>
    /// <term>Описание</term>
    /// </listheader>
    /// <item>
    /// <description>attribute1_name .. attributeN_name</description>
    /// <description>Определяет имя атрибута строкового типа, значение которого будет формироваться.</description>
    /// </item>
    /// <item>
    /// <description>Config/Format</description>
    /// <description>Строка формата. Для вставки аргументов используются паттерны <c>{x}</c>, где x - порядковый номер атрибута. Нумерация начинается с 0. Пример строки формата: <c>Code: {0}</c>. Другой пример: <c>{0} {1} {2}</c>.</description>
    /// </item>
    /// <item>
    /// <description>Config/Args</description>
    /// <description>Содержит дочерние элементы, которые определяют аргументы формата, подставляемые в паттерны <c>{x}</c>. Порядок расположения дочерних элементов определяет порядок их подстановки в строку формата. Так, первый элемент будет подставлен вместо паттерна <c>{0}</c>, второй - вместо {1} и т. д.</description>
    /// </item>
    /// <item>
    /// <description>Config/Args/Attribute</description>
    /// <description>Описывает аргумент строки формата, представляющий собой значение указанного атрибута сущности. Например, описание следующего вида <c>&lt;Attribute&gt;lastname&lt;/Attribute&gt;</c> говорит о том, что в качестве аргумента, необходимо подставить значение атрибута <c>lastname</c>.</description>
    /// </item>
    /// </list>
    /// <para>
    /// <code title="Пример настройки для формирования ФИО контакта">
    /// Step
    /// Message:    	        Create
    /// Primary Entity:         contact
    /// Name:                   Npf.StringTools.Plugins.Format: Формирование ФИО при создании контакта
    /// Run in User's Context:  Calling User
    /// Execution Order:        10
    /// Stage:                  Pre-operation
    /// Unsecure Configuration: {
    ///                             fullname: {
    ///                                 Format: "{0} {1} {2}",
    ///                                 Args: [
    ///                                     "lastname",
    ///                                     "firstname",
    ///                                     "middlename",
    ///                                 ]
    ///                             }
    ///                         }
    /// 
    /// Step
    /// Message:    	        Update
    /// Primary Entity:         contact
    /// Filtering Attributes:   firstname, middlename, lastname
    /// Name:                   Npf.Plugins.StringTools.Format: Формирование ФИО при обновлении контакта
    /// Run in User's Context:  Calling User
    /// Execution Order:        10
    /// Stage:                  Pre-operation
    /// Unsecure Configuration: {
    ///                             fullname: {
    ///                                 Format: "{0} {1} {2}",
    ///                                 Args: [
    ///                                     "lastname",
    ///                                     "firstname",
    ///                                     "middlename",
    ///                                 ]
    ///                             }
    ///                         }
    ///     Image
    ///     Image Type: Pre Image
    ///     Name:       Image
    ///     Alias:      Image
    ///     Parameters: firstname, middlename, lastname
    /// </code>
    /// </para>
    /// </remarks>
// ReSharper disable once RedundantExtendsListEntry
    public class Format : PluginBase, IPlugin
    {
        private Dictionary<string, Options> _config;


        /// <inheritdoc />
        public Format(string unsecureConfig) : base(unsecureConfig)
        {
        }


        /// <inheritdoc />
        public override void Configuring(Context context)
        {
            _config = JsonConvert.DeserializeObject<Dictionary<string, Options>>(UnsecureConfiguration);
        }


        /// <inheritdoc />
        public override void Execute(Context context)
        {
            try
            {
                foreach (var attribute in _config)
                {
                    var args = new List<object>();
                    foreach (var argument in attribute.Value.Args)
                    {
                        if (context.Entity.Contains(argument))
                            args.Add(context.Entity[argument].ToString());
                        else if (context.Message == Message.Update && context.PreImage.Contains(argument))
                            args.Add(context.PreImage[argument].ToString());
                        else
                            args.Add(string.Empty);
                    }
                    context.Entity[attribute.Key] = string.Format(attribute.Value.Format, args.ToArray());
                }
            }
            catch (Exception)
            {
                context.TracingService.Trace("=== Plug-in Config ===");
                context.TracingService.Trace(_config);
                throw;
            }
        }


        /// <exclude/>
        public class Options
        {
            public string Format { get; set; }
            public List<string> Args { get; set; }
        }
    }
}