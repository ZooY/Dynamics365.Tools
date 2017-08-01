//using System;
//using System.Activities;
//using System.Collections.Generic;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Workflow;
//using Npf.Common.Workflow;
//using Npf.Models;
//using System.Linq;
//using Microsoft.Xrm.Sdk.Query;
//using Npf.EntityTools.Models;


//namespace PZone.EntityTools.Workflow
//{
//    /// <summary>
//    /// Переключение процессов
//    /// </summary>
//    public class SwitchProcess : WorkflowBase
//    {
//        /// <summary>
//        /// Имя процесса
//        /// </summary>
//        [RequiredArgument]
//        [Input("Process Name")]
//        public InArgument<string> Process { get; set; }

//        /// <summary>
//        /// Имя шага
//        /// </summary>
//        [RequiredArgument]
//        [Input("Process Stage Name")]
//        public InArgument<string> ProcessStage { get; set; }

//        /// <inheritdoc />
//        protected override string LoggerName => "Entity Tools";

//        /// <inheritdoc />
//        protected override void Execute(Context context)
//        {
//            var queryEx = new QueryExpression
//            {
//                ColumnSet = new ColumnSet(Metadata.Workflow.Name),
//                EntityName = Metadata.Workflow.LogicalName
//            };

//            var condition = new List<ConditionExpression>
//            {
//                new ConditionExpression()
//                {
//                    AttributeName = Metadata.Workflow.Name,
//                    Operator = ConditionOperator.Equal,
//                    Values = { Process.Get(context) }
//                },
//                new ConditionExpression()
//                {
//                    AttributeName = Metadata.Workflow.StateCode,
//                    Operator = ConditionOperator.Equal,
//                    Values = { WorkflowState.Activated }
//                }
//            };

//            var filter = new FilterExpression { FilterOperator = LogicalOperator.And };
//            filter.Conditions.AddRange(condition);
//            queryEx.Criteria.Filters.Add(filter);

//            var processes = context.Service.RetrieveMultiple(queryEx);
//            var process = processes.Entities.FirstOrDefault();

//            if (process == null)
//                throw new InvalidPluginExecutionException($"Process '{Process.Get(context)}' not found");

//            queryEx = new QueryExpression
//            {
//                ColumnSet = new ColumnSet(Metadata.ProcessStage.StageName),
//                EntityName = Metadata.ProcessStage.LogicalName
//            };

//            condition = new List<ConditionExpression>
//            {
//                new ConditionExpression()
//                {
//                    AttributeName = Metadata.ProcessStage.StageName,
//                    Operator = ConditionOperator.Equal,
//                    Values = { ProcessStage.Get(context) }
//                },
//                new ConditionExpression()
//                {
//                    AttributeName = Metadata.ProcessStage.ProcessId,
//                    Operator = ConditionOperator.Equal,
//                    Values = { process.Id }

//                }
//            };

//            filter = new FilterExpression { FilterOperator = LogicalOperator.And };
//            filter.Conditions.AddRange(condition);
//            queryEx.Criteria.Filters.Add(filter);

//            var stages = context.Service.RetrieveMultiple(queryEx);
//            var stage = stages.Entities.FirstOrDefault();

//            if (stage == null)
//                throw new InvalidPluginExecutionException($"Stage '{Process.Get(context)}' not found");

//            var updatedStage = new Entity(context.EntityName);
//            updatedStage.Id = context.EntityId;

//            updatedStage["stageid"] = stage.Id;//ProcessStageId,
//            updatedStage["processid"] = process.Id; //.WorkflowId
//                                                    //};
//            context.Service.Update(updatedStage);
//        }
//    }
//}