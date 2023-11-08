using ApplicationCore.Configs;
using ApplicationCore.Models;
using Dapper;

namespace Infrastructure.Database.Commands
{
    public class UserStatisticsCommand
    {
        readonly DbConfig _dbConfig;

        public UserStatisticsCommand(DbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public async Task<int> CreateAsync(UserStatistics entity)
        {
            var query = @"
                INSERT INTO [dbo].[UserStatistics]
                    ([UserId]
                    ,[TeamId]
                    ,[StatisticsType]
                    ,[Count]
                    ,[Quota]
                    ,[LastUpdateOn]
                    ,[LastMonthlyCycleEnd]
                    ,[LastMonthlyCycleStart])
                VALUES
                    (@UserId
                    ,@TeamId
                    ,@StatisticsType
                    ,@Count
                    ,@Quota
                    ,@LastUpdateOn
                    ,@LastMonthlyCycleEnd
                    ,@LastMonthlyCycleStart);

                SELECT SCOPE_IDENTITY()";

            await using var connection = _dbConfig.Connection;
            return await connection.QuerySingleOrDefaultAsync<int>(query, entity);
        }

        public async Task<int> UpdateAsync(UserStatistics entity)
        {
            var query = @"
                UPDATE [dbo].[UserStatistics]
                   SET [UserId] = @UserId
                      ,[TeamId] = @TeamId
                      ,[StatisticsType] = @StatisticsType
                      ,[Count] = @Count
                      ,[Quota] = @Quota
                      ,[LastUpdateOn] = @LastUpdateOn
                      ,[LastMonthlyCycleEnd] = @LastMonthlyCycleEnd
                      ,[LastMonthlyCycleStart] = @LastMonthlyCycleStart
                WHERE Id = @Id";

            await using var connection = _dbConfig.Connection;
            return await connection.ExecuteAsync(query, entity);
        }
    }
}
