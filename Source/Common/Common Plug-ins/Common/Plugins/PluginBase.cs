using System;
using Microsoft.Xrm.Sdk;
using PZone.Common.Plugins.Exceptions;
using PZone.Xrm.Sdk;


namespace PZone.Common.Plugins
{
    /// <summary>
    /// Базовый класс подключаемых модулей.
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        private bool _firstExecute = true;


        /// <summary>
        /// Не защищенная конфигурация.
        /// </summary>
        public string UnsecureConfiguration { get; private set; }


        /// <summary>
        /// Конструтор класса.
        /// </summary>
        /// <param name="unsecureConfig">Не защищенная конфигурация.</param>
        protected PluginBase(string unsecureConfig)
        {
            UnsecureConfiguration = unsecureConfig;
        }


        /// <summary>
        /// Стандартный метод запуска основной бизнес-логики подключаемого модуля.
        /// </summary>
        /// <param name="serviceProvider">Провайдер контекста выполенения подключаемого модуля.</param>
        /// <exception cref="InvalidPluginExecutionException">Ошибка настройки компонента системы.</exception>
        /// <exception cref="InvalidPluginExecutionException">Произошла не ожидаемая ошибка системы.</exception>
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = new Context(serviceProvider);
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
                        throw new PluginConfigurationException("System component setting error.\n Please contact support.", ex);
                    }
                }
                Execute(context);
            }
            catch (PluginConfigurationException ex)
            {
                TraceException(context, ex);
                throw;
            }
            catch (InvalidPluginExecutionException ex)
            {
                TraceException(context, ex);
                throw;
            }
            catch (Exception ex)
            {
                TraceException(context, ex);
                throw new InvalidPluginExecutionException("An unexpected system error.\n Please contact support.", ex);
            }
        }


        /// <summary>
        /// Записывание данных исключения в сервис трассировки.
        /// </summary>
        /// <param name="context">Конекст выполнения.</param>
        /// <param name="exception">Исключение.</param>
        protected virtual void TraceException(Context context, Exception exception)
        {
            context.TracingService.Trace("=== Plug-in Config ===");
            context.TracingService.Trace(UnsecureConfiguration);
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
        /// Метод, содержащий основную бизнес-логику.
        /// </summary>
        /// <param name="context">Контекст выполнения подключаемого модуля.</param>
        public abstract void Execute(Context context);


        /// <summary>
        /// Метод конфигурирования подключаемого модуля, выполняющийся один раз при первом выпролнении.
        /// </summary>
        /// <param name="context">Контекст выполнения подключаемого модуля.</param>
        /// <remarks>
        /// В случае возникновения ошибки в процессе конфишурирования метод будет вызван повторно при следующем запуске модуля.
        /// </remarks>
        public virtual void Configuring(Context context)
        {
        }

    }
}