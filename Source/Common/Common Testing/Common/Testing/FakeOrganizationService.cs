using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using PZone.Xrm;


namespace PZone.Common.Testing
{
    public class FakeOrganizationService : IOrganizationService, IDisposable
    {
        protected List<Guid> DeletedEntities = new List<Guid>();


        public List<Entity> Entities { get; set; } = new List<Entity>();


        public List<EntityMetadata> EntitiesMetadata { get; set; } = new List<EntityMetadata>();


        public List<AttributeMetadata> AttributesMetadata { get; set; } = new List<AttributeMetadata>();


        /// <summary>
        /// Creates a record. 
        /// </summary>
        /// <returns>
        /// Type:Returns_Guid
        /// The ID of the newly created record.
        /// </returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"/>. An entity instance that contains the properties to set in the newly created record.</param>
        public virtual Guid Create(Entity entity)
        {
            entity.Id = Guid.Empty;
            var sb = new StringBuilder();
            sb.AppendLine("Create entity");
            sb.AppendLine(entity.EntityInfo());
            System.Diagnostics.Trace.Write(sb.ToString());
            return entity.Id;
        }


        /// <summary>
        /// Retrieves a record.
        /// </summary>
        /// <returns>
        /// Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"/>
        /// The requested entity.
        /// </returns>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"/>. A query that specifies the set of columns, or attributes, to retrieve. </param><param name="id">Type: Returns_Guid. property_entityid that you want to retrieve.</param><param name="entityName">Type: Returns_String. property_logicalname that is specified in the entityId parameter.</param>
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Retrieve entity ===");
            sb.AppendLine($"EntityName = {entityName}");
            sb.AppendLine($"ID = {id}");
            sb.AppendLine(columnSet.AllColumns
                ? "ColumnSet = All"
                : $"ColumnSet = {string.Join(", ", columnSet.Columns)}");
            System.Diagnostics.Trace.WriteLine(sb.ToString());
            if (DeletedEntities.Contains(id))
            {
                throw new Exception("The record was previously removed");
            }
            var entity = Entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
                throw new Exception($"Record with ID = {id} is not found");
            return entity;
        }


        /// <summary>
        /// Updates an existing record.
        /// </summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"/>. An entity instance that has one or more properties set to be updated in the record.</param>
        public virtual void Update(Entity entity)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Update entity");
            sb.AppendLine(entity.EntityInfo());
            System.Diagnostics.Trace.WriteLine(sb.ToString());
        }


        /// <summary>
        /// Deletes a record.
        /// </summary>
        /// <param name="id">Type: Returns_Guid. property_entityid that you want to delete.</param><param name="entityName">Type: Returns_String. property_logicalname that is specified in the entityId parameter.</param>
        public void Delete(string entityName, Guid id)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Delete entity");
            sb.AppendLine($"EntityName = {entityName}");
            sb.AppendLine($"ID = {id}");
            System.Diagnostics.Trace.WriteLine(sb.ToString());
            DeletedEntities.Add(id);
        }


        /// <summary>
        /// Executes a message in the form of a request, and returns a response.
        /// </summary>
        /// <returns>
        /// Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"/>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.
        /// </returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"/>. A request instance that defines the action to be performed.</param>
        public virtual OrganizationResponse Execute(OrganizationRequest request)
        {
            var retrieveAttributeRequest = request as RetrieveAttributeRequest;
            if (retrieveAttributeRequest != null)
            {
                var attributeMetadata = AttributesMetadata.FirstOrDefault(e => e.LogicalName == retrieveAttributeRequest.LogicalName);
                if (attributeMetadata == null)
                    throw new Exception($"Attribute metadata with logical name = {retrieveAttributeRequest.LogicalName} is not found");
                var response = new RetrieveAttributeResponse { ["AttributeMetadata"] = attributeMetadata };
                return response;
            }
            var retrieveEntityRequest = request as RetrieveEntityRequest;
            if (retrieveEntityRequest != null)
            {
                var entityMetadata = EntitiesMetadata.FirstOrDefault(e => e.LogicalName == retrieveEntityRequest.LogicalName);
                if (entityMetadata == null)
                    throw new Exception($"Entity metadata with logical name = {retrieveEntityRequest.LogicalName} is not found");
                var response = new RetrieveEntityResponse { ["EntityMetadata"] = entityMetadata };
                return response;
            }
            if (request is RetrieveAllEntitiesRequest)
            {
                var response = new RetrieveAllEntitiesResponse { ["EntityMetadata"] = EntitiesMetadata.ToArray() };
                return response;
            }

            var executeMultipleRequest = request as ExecuteMultipleRequest;
            if (executeMultipleRequest != null)
            {
                var collection = new ExecuteMultipleResponseItemCollection();
                var i = 0;
                foreach (var requestItem in executeMultipleRequest.Requests)
                {

                    var createRequest = requestItem as CreateRequest;
                    if (createRequest != null)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine("Create entity");
                        createRequest.Target.Id = Guid.Empty;
                        sb.AppendLine(createRequest.Target.EntityInfo());
                        System.Diagnostics.Trace.Write(sb.ToString());
                        var createResponse = new CreateResponse { ["Id"] = Guid.Empty };
                        collection.Add(new ExecuteMultipleResponseItem
                        {
                            RequestIndex = i++,
                            Response = createResponse,
                            Fault = null
                        });
                        continue;
                    }
                    throw new NotImplementedException(requestItem.GetType().ToString());
                }
                var response = new ExecuteMultipleResponse { Results = { ["Responses"] = collection } };
                return response;
            }
            throw new NotImplementedException(request.GetType().ToString());
        }


        /// <summary>
        /// Creates a link between records.
        /// </summary>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"/>. property_relationshipname to be used to create the link. </param><param name="entityName">Type: Returns_String. property_logicalname that is specified in the entityId parameter.</param><param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param><param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"/>. property_relatedentities to be associated.</param>
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Deletes a link between records.
        /// </summary>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"/>. property_relationshipname to be used to remove the link.</param><param name="entityName">Type: Returns_String. property_logicalname that is specified in the entityId parameter.</param><param name="entityId">Type: Returns_Guid. property_entityid from which the related records are disassociated.</param><param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"/>. property_relatedentities to be disassociated.</param>
        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Retrieves a collection of records.
        /// </summary>
        /// <returns>
        /// Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"/>The collection of entities returned from the query.
        /// </returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"/>. A query that determines the set of records to retrieve.</param>
        public virtual EntityCollection RetrieveMultiple(QueryBase query)
        {
            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (query is QueryExpression)
                return RetrieveMultiple((QueryExpression)query);
            if (query is FetchExpression)
                return RetrieveMultiple((FetchExpression)query);
            if (query is QueryByAttribute)
                return RetrieveMultiple((QueryByAttribute)query);
            throw new NotImplementedException("RetrieveMultiple для данного вида запроса не реализован.");
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull
        }


        private EntityCollection RetrieveMultiple(QueryByAttribute query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Retrieve entity collection by QueryByAttribute ===");
            sb.AppendLine(QueryExpressionInfo(query));
            var entities = new List<Entity>();
            foreach (var entity in Entities.Where(e => e.LogicalName == query.EntityName))
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                for (var i = 0; i < query.Attributes.Count; i++)
                {
                    var attribute = query.Attributes[i];
                    if (entity.Contains(attribute) && entity[attribute].Equals(query.Values[i]))
                        entities.Add(entity);
                }
            }

            sb.AppendLine("Retrieved " + entities.Count + " entities");
            System.Diagnostics.Trace.WriteLine(sb.ToString());
            return new EntityCollection(entities);
        }


        private EntityCollection RetrieveMultiple(QueryExpression query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Retrieve entity collection by QueryExpression ===");
            sb.AppendLine(QueryExpressionInfo(query));
            var entities = Entities.Where(e => e.LogicalName == query.EntityName).ToList();
            sb.AppendLine("Retrieved " + entities.Count + " entities");
            System.Diagnostics.Trace.WriteLine(sb.ToString());
            return new EntityCollection(entities);
        }


        private EntityCollection RetrieveMultiple(FetchExpression query)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Retrieve entity collection by FetchExpression ===");
            sb.AppendLine(query.Query);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(query.Query);
            var entityNode = xmlDoc.SelectSingleNode("/fetch/entity");
            if (entityNode?.Attributes == null)
                throw new Exception("Некорректный FetchXML-запрос.");
            var entityName = entityNode.Attributes["name"].Value;
            var entities = Entities.Where(e => e.LogicalName == entityName).ToList();
            sb.AppendLine("Retrieved " + entities.Count + " entities");
            System.Diagnostics.Trace.WriteLine(sb.ToString());
            return new EntityCollection(entities);
        }


        private string QueryExpressionInfo(QueryExpression query)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"EntityName = {query.EntityName}");
            sb.AppendLine(query.ColumnSet.AllColumns
                ? "ColumnSet = All"
                : $"ColumnSet = {string.Join(", ", query.ColumnSet.Columns)}");
            if (query.Criteria != null)
            {
                sb.AppendLine($"Criteria (FilterOperator = {query.Criteria.FilterOperator})");
                foreach (var condition in query.Criteria.Conditions)
                {
                    sb.AppendLine($"\t{condition.AttributeName} {condition.Operator} \"{string.Join("\", \"", condition.Values)}\"");
                }
            }
            return sb.ToString();
        }


        private string QueryExpressionInfo(QueryByAttribute query)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"EntityName = {query.EntityName}");
            sb.AppendLine(query.ColumnSet.AllColumns
                ? "ColumnSet = All"
                : $"ColumnSet = {string.Join(", ", query.ColumnSet.Columns)}");
            sb.AppendLine("Criteria");
            for (var i = 0; i < query.Attributes.Count; i++)
                sb.AppendLine($"\t{query.Attributes[i]} = {query.Values[i]}");
            return sb.ToString();
        }


        public void Dispose()
        {
            System.Diagnostics.Trace.WriteLine("Dispose " + GetType());
        }
    }
}