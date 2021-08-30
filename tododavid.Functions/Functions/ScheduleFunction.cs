using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;
using tododavid.Functions.Entities;

namespace tododavid.Functions.Functions
{
    public static class ScheduleFunction
    {
        [FunctionName("ScheduleFunction")]
        public static async Task Run(
            [TimerTrigger("0 */2 * * * *")] TimerInfo myTimer,
            [Table("todo", Connection = "AzureWebJobsStorage")] CloudTable todoTable,
            ILogger log)
        {
            log.LogInformation($"Deleting complete function trigger function executed at: {DateTime.Now}");

            string filter = TableQuery.GenerateFilterConditionForBool("IsCompleted", QueryComparisons.Equal, true);
            TableQuery<TodoEntity> query = new TableQuery<TodoEntity>().Where(filter);
            TableQuerySegment<TodoEntity> completedTodos = await todoTable.ExecuteQuerySegmentedAsync(query, null);
            int deleted = 0;
            foreach (TodoEntity completedTodo in completedTodos)
            {
                await todoTable.ExecuteAsync(TableOperation.Delete(completedTodo));
                deleted++;
            }

            log.LogInformation($"Deleted: {deleted} items at: {DateTime.Now}");
        }
    }
}
