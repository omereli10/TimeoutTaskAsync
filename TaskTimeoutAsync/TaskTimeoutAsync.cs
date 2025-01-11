using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTimeoutAsync
{
    class TaskTimeoutAsync<T>
    {
        private String connectionString;
        private int connectionTimeout;

        public TaskTimeoutAsync(String connectionString, int connectionTimeout)
        {
            this.connectionString = connectionString;
            this.connectionTimeout = connectionTimeout;
        }

        public async Task<IEnumerable<T>> RunAsync(Query query, int queryTimeout)
        {
            // Did the SQL QUERY query finish before timeout
            bool didFinish;

            // The task of the SQL SERVER query to execute
            Task<IEnumerable<T>> taskQuery;

            try
            {
                // Establish connection with the SQL SERVER
                using (SqlConnection connection = new SqlConnection(this.connectionString))
                {
                    // Initialize an async task for the querying on SQL SERVER
                    taskQuery = connection.QueryAsync<T>(sql: query.Value, param: query.Params, commandTimeout: this.connectionTimeout);

                    // Query on SQL SERVER async and return if it completed in time
                    didFinish = await Task.WhenAny(taskQuery, Task.Delay(TimeSpan.FromSeconds(queryTimeout))).ConfigureAwait(false) == taskQuery;
                }
            }
            catch (SqlException)
            {
                // Timeout occurred for the SQL SERVER
                throw new ConnectionTimeoutException();
            }
            catch (Exception ex)
            {
                // Internal server error occurred
                throw new InternalServerErrorException($"Query: {query.ToString()}. Exception: {ex.Message}.");
            }

            // If the query finished before timeout
            if (didFinish) return taskQuery.GetAwaiter().GetResult();

            // Timeout occurred for the SQL query
            throw new QueryTimeoutException();
        }
    }
}
