using ApplicationCore.Models;
using Infrastructure.Database.Commands;
using Infrastructure.Database.Queries;
using Infrastructure.Tests.Configs;
using Test.Utilities.Attributes;
using Xunit;

namespace Infrastructure.Tests.Database.Commands
{
    public class UserStatisticsCommandTests
    {
        readonly UserStatisticsQuery _query;
        readonly UserStatisticsCommand _uot;
        readonly UserStatistics _entity;

        public UserStatisticsCommandTests()
        {
            var config = new TestConfig();
            _query = new UserStatisticsQuery(config.DbConfig);
            _uot = new UserStatisticsCommand(config.DbConfig);

            _entity = new UserStatistics
            {
                UserId = 3,
                TeamId = 3,
                StatisticsType = "TokenQuota",
                Count = 10000,
                Quota = 100000
            };
        }

        [Fact]
        [WithRollback]
        public async Task Create()
        {
            int lastId = await _uot.CreateAsync(_entity);
            Assert.True(lastId > 0);

            var record = await _query.GetAsync(lastId);
            Assert.NotNull(record);
            Assert.Equal(lastId, record.Id);
        }

        [Fact]
        [WithRollback]
        public async Task Update()
        {
            int lastId = await _uot.CreateAsync(_entity);
            Assert.True(lastId > 0);

            var record = await _query.GetAsync(lastId);
            _entity.LastMonthlyCycleStart = DateTime.UtcNow;
            _entity.LastMonthlyCycleEnd = DateTime.UtcNow.AddMonths(1);
            record.LastMonthlyCycleStart = _entity.LastMonthlyCycleStart;
            record.LastMonthlyCycleEnd = _entity.LastMonthlyCycleEnd;
            int result = await _uot.UpdateAsync(record);

            Assert.True(result > 0);

            record = await _query.GetAsync(lastId);
            Assert.Equal(_entity.LastMonthlyCycleStart.Value.Date, record.LastMonthlyCycleStart.Value.Date);
        }

    }
}
