using ApplicationCore.Configs;
using ApplicationCore.Models;
using Dapper;

namespace Infrastructure.Database.Queries
{
    public class UserStatisticsQuery
    {
        readonly DbConfig _dbConfig;

        public UserStatisticsQuery(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<IEnumerable<UserStatistics>> GetListAsync()
        {
            var query = @"
                SELECT [Id]
                    ,[UserId]
                    ,[TeamId]
                    ,[StatisticsType]
                    ,[Count]
                    ,[Quota]
                    ,[LastUpdateOn]
                    ,[LastMonthlyCycleEnd]
                    ,[LastMonthlyCycleStart]
                FROM [dbo].[UserStatistics]";

            await using var connection = _dbConfig.Connection;
            return await connection.QueryAsync<UserStatistics>(query);
        }

        public async Task<UserStatistics> GetAsync(int id)
        {
            var query = @"
                SELECT [Id]
                    ,[UserId]
                    ,[TeamId]
                    ,[StatisticsType]
                    ,[Count]
                    ,[Quota]
                    ,[LastUpdateOn]
                    ,[LastMonthlyCycleEnd]
                    ,[LastMonthlyCycleStart]
                FROM [dbo].[UserStatistics]
                WHERE Id = @Id";

            await using var connection = _dbConfig.Connection;
            return await connection.QueryFirstOrDefaultAsync<UserStatistics>(query, new { Id = id });
        }
    }
}
