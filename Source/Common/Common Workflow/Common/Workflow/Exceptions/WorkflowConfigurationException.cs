using System;
using PZone.Exceptions;


namespace PZone.Common.Workflow.Exceptions
{
    /// <summary>
    /// Ошибка настройки действия процесса.
    /// </summary>
    public class WorkflowConfigurationException : ConfigurationException
    {
        private const int CODE = 10220000;

        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public WorkflowConfigurationException(string message) : base(CODE, message)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Сообщение об ошибке для отображения пользователю.</param>
        /// <param name="exception">Вложенное исключение.</param>
        public WorkflowConfigurationException(string message, Exception exception) : base(CODE, message, exception)
        {
        }

        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="code">Код ошибки</param>
        /// <param name="message">Сообщение об ошибке для отображения пользователю.</param>
        public WorkflowConfigurationException(int code, string message) : base(code, message)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="code">Код ошибки.</param>
        /// <param name="message">Сообщение об ошибке для отображения пользователю.</param>
        /// <param name="exception">Вложенное исключение.</param>
        public WorkflowConfigurationException(int code, string message, Exception exception) : base(code, message, exception)
        {
        }
    }
}