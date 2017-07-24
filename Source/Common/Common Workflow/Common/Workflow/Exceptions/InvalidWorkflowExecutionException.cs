using System;
using PZone.Exceptions;


namespace PZone.Common.Workflow.Exceptions
{
    /// <summary>
    /// Ошибка выполнения действия процесса.
    /// </summary>
    public class InvalidWorkflowExecutionException : CommonException
    {
        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public InvalidWorkflowExecutionException(string message) : this(message, null)
        {
        }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        /// <param name="exception">Исключение, приведшее к ошибке.</param>
        public InvalidWorkflowExecutionException(string message, Exception exception) : base(10200000, message, exception)
        {
        }
    }
}