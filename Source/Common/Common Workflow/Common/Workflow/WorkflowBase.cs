using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using PZone.Activities;
using PZone.Common.Workflow.Exceptions;
using PZone.Xrm.Sdk;


namespace PZone.Common.Workflow
{
    /// <summary>
    /// Базовый класс действий процессов.
    /// </summary>
    public abstract class WorkflowBase : CodeActivity
    {
        private bool _firstExecute = true; // флаг первого выполнения действия.

        /// <summary>
        /// Флаг вызова исключения процесса при ошибке выполнения действия.
        /// </summary>
        /// <remarks>
        /// Если влаг установлен, то при возникновении ошибки будет вызвано исколючение и 
        /// выполнение процесса будет прекращено. Если флаг не установлен, то исключение вызвано не
        /// будет, выходной параметр <see cref="HasError"/> будет иметь значение <c>true</c>, а в 
        /// параметре <see cref="ErrorMessage"/> будeт сообщение об ошибке.
        /// </remarks>
        [Input("Throw Excteption on Error")]
        [Default("false")]
        public InArgument<bool> ThrowException { get; set; } = false;


        /// <summary>
        /// Флаг ошибки выполнения действия процесса.
        /// </summary>
        [Output("Execution Error")]
        public OutArgument<bool> HasError { get; set; }


        /// <summary>
        /// Текст ошибки выполнения действия процесса.
        /// </summary>
        [Output("Error Message")]
        public OutArgument<string> ErrorMessage { get; set; }


        /// <summary>
        /// Стандартный метод запуска основной бизнес-логики действия процесса.
        /// </summary>
        /// <param name="executionContext">Контекста выполенения действия процесса.</param>
        protected override void Execute(CodeActivityContext executionContext)
        {
            var context = new Context(executionContext);

            try
            {
                if (_firstExecute)
                {
                    try
                    {
                        Configuring(context);
                        _firstExecute = false;
                    }
                    catch (Exception ex)
                    {
                        throw new WorkflowConfigurationException("System component setting error.\n Please contact support.", ex);
                    }
                }
                Execute(context);
            }
            catch (WorkflowConfigurationException ex)
            {
                TraceException(context, ex);
                if (!HasError.Get(executionContext))
                    SetError(context, ex.Message);
                if (ThrowException.Get(context))
                    throw;
            }
            catch (InvalidWorkflowExecutionException ex)
            {
                TraceException(context, ex);
                if (!HasError.Get(executionContext))
                    SetError(context, ex.Message);
                if (ThrowException.Get(context))
                    throw;
            }
            catch (Exception ex)
            {
                TraceException(context, ex);
                if (!HasError.Get(executionContext))
                    SetError(context, ex.Message);
                if (ThrowException.Get(context))
                    throw new InvalidWorkflowExecutionException("An unexpected system error.\n Please contact support.", ex);
            }
        }


        /// <summary>
        /// Метод конфигурирования действия, выполняющийся один раз при первом выпролнении.
        /// </summary>
        /// <param name="context">Контекст выполнения действия.</param>
        /// <remarks>
        /// В случае возникновения ошибки в процессе конфигурирования метод будет вызван повторно при следующем запуске действия.
        /// </remarks>
        protected virtual void Configuring(Context context)
        {
        }


        /// <summary>
        /// Метод, содержащий основную бизнес-логику.
        /// </summary>
        /// <param name="context">Контекст выполнения действия процесса.</param>
        protected abstract void Execute(Context context);


        /// <summary>
        /// Записывание данных исключения в сервис трассировки.
        /// </summary>
        /// <param name="context">Конекст выполнения.</param>
        /// <param name="exception">Исключение.</param>
        protected virtual void TraceException(Context context, Exception exception)
        {
            context.TracingService.Trace("=== Activity Context ===");
            context.TracingService.Trace(context.SourceActivityContext);
            context.TracingService.Trace("=== Context ===");
            context.TracingService.Trace(context.SourceContext);
            context.TracingService.Trace("=== Exception ===");
            var ex = exception;
            while (ex != null)
            {
                context.TracingService.Trace(ex);
                ex = ex.InnerException;
            }
        }


        /// <summary>
        /// Установка значения ошибки выполнения.
        /// </summary>
        /// <param name="context">Конекст выполнения.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <exception cref="Exception">Вызывается если параметр <see cref="ThrowException"/> установлен в <c>true</c>.</exception>
        protected virtual void SetError(Context context, string message)
        {
            HasError.Set(context, true);
            ErrorMessage.Set(context, message);
            if (ThrowException.Get(context))
                throw new Exception(message);
        }
    }
}