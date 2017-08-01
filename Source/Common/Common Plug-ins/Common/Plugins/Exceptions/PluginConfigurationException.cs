using System;
using PZone.Exceptions;


namespace PZone.Common.Plugins.Exceptions
{
    /// <summary>
    /// Некорректная настройка подключаемого модуля.
    /// </summary>
    public class PluginConfigurationException : ConfigurationException
    {
        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public PluginConfigurationException(string message) : base(10120000, message)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        /// <param name="exception">Исключение, приведшее к ошибке.</param>
        public PluginConfigurationException(string message, Exception exception) : base(10120000, message, exception)
        {
        }
    }
}