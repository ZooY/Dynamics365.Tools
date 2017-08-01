using System;
using Microsoft.Xrm.Sdk;
using PZone.Xrm.Sdk;


namespace PZone.Common.Plugins
{
    /// <summary>
    /// Контекст выполнения подключаемого модуля.
    /// </summary>
    public class Context
    {
        private readonly Lazy<IPluginExecutionContext> _context;
        private readonly Lazy<IOrganizationService> _service;
        private readonly Lazy<IOrganizationService> _impersonatedService;
        private readonly Lazy<ITracingService> _tracingService;
        private readonly Lazy<Entity> _prymaryEntity;
        private readonly Lazy<Entity> _extendedEntity;
        private readonly Lazy<Entity> _preImage;
        private readonly Lazy<Entity> _postImage;
        private readonly Lazy<Stage> _stage;
        private readonly Lazy<Message> _message;

        /// <summary>
        /// Флаг тестовая организация или нет
        /// </summary>
        public Lazy<bool> IsTestingOrganization;


        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="serviceProvider">Провайдер констекста выпролнения.</param>
        public Context(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            _context = new Lazy<IPluginExecutionContext>(() => (IPluginExecutionContext)ServiceProvider.GetService(typeof(IPluginExecutionContext)));
            _service = new Lazy<IOrganizationService>(() => ServiceProvider.GetService(SourceContext.UserId));
            _impersonatedService = new Lazy<IOrganizationService>(() => ServiceProvider.GetService(SourceContext.InitiatingUserId));
            _tracingService = new Lazy<ITracingService>(() => ServiceProvider.GetTracingService());
            _prymaryEntity = new Lazy<Entity>(() => SourceContext.GetContextEntity());
            _preImage = new Lazy<Entity>(() => SourceContext.GetDefaultPreEntityImage());
            _postImage = new Lazy<Entity>(() => SourceContext.GetDefaultPostEntityImage());
            _stage = new Lazy<Stage>(() => (Stage)SourceContext.Stage);
            _message = new Lazy<Message>(() => (Message)Enum.Parse(typeof(Message), SourceContext.MessageName));
            _extendedEntity = new Lazy<Entity>(() =>
            {
                Entity entity = null;
                if (SourceContext.PreEntityImages.ContainsKey("Image"))
                    entity = PreImage.Clone();
                if (entity == null)
                    entity = Entity.Clone();
                else
                    entity.Extend(Entity);
                if (!SourceContext.PostEntityImages.ContainsKey("Image"))
                    return entity;
                if (entity == null)
                    entity = PostImage.Clone();
                else
                    entity.Extend(PostImage);
                return entity;
            });
        }


        /// <summary>
        /// Провайдер констекста выпролнения.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }


        /// <summary>
        /// Ссылка на экземпляр CRM-сервиса, запусщенного от имени пользователя, указанного при регистрации плагина.
        /// </summary>
        public IOrganizationService Service => _service.Value;


        /// <summary>
        /// Ссылка на экземпляр CRM-сервиса, запусщенного от имени пользователя, инициировавшего запуск подключаемого модуля.
        /// </summary>
        public IOrganizationService ImpersonatedService => _impersonatedService.Value;


        /// <summary>
        /// Cервис трассировки.
        /// </summary>
        public ITracingService TracingService => _tracingService.Value;


        /// <summary>
        /// Исходный стандартный контекст подключаемого модуля.
        /// </summary>
        public IPluginExecutionContext SourceContext => _context.Value;


        /// <summary>
        /// Ссылка на основную сущность контекста подключаемого модуля.
        /// </summary>
        public Entity Entity => _prymaryEntity.Value;


        /// <summary>
        /// Ссылка на снимок сущности до выполенения операции (Pre Image).
        /// </summary>
        public Entity PreImage => _preImage.Value;


        /// <summary>
        /// Ссылка на снимок сущности после выполенения операции (Post Image).
        /// </summary>
        public Entity PostImage => _postImage.Value;


        /// <summary>
        /// Стадия выполенения подключаемого модуля.
        /// </summary>
        public Stage Stage => _stage.Value;


        /// <summary>
        /// Событие подключаемого модуля.
        /// </summary>
        public Message Message => _message.Value;


        /// <summary>
        /// Возвращает "расширенную сущность", включающую поля основной сущности, а также поля снимков состояния.
        /// </summary>
        public Entity ExtendedEntity => _extendedEntity.Value;
    }
}