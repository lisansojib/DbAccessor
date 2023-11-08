using ApplicationCore.Models;
using Infrastructure.Database.Commands;
using Infrastructure.Database.Queries;
using Infrastructure.Tests.Configs;
using Test.Utilities.Attributes;
using Xunit;

namespace Infrastructure.Tests.Database.Queries
{
    public class UserStatisticsQueryTests
    {
        readonly UserStatisticsQuery _uot;
        readonly UserStatisticsCommand _command;
        readonly UserStatistics _entity;

        public UserStatisticsQueryTests()
        {
            var config = new TestConfig();
            _uot = new UserStatisticsQuery(config.DbConfig);
            _command = new UserStatisticsCommand(config.DbConfig);

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
        public async Task List()
        {
            await _command.CreateAsync(_entity);
            var records = await _uot.GetListAsync();
            Assert.NotEmpty(records);
        }

        [Fact]
        [WithRollback]
        public async Task Get()
        {
            int lastId = await _command.CreateAsync(_entity);

            var record = await _uot.GetAsync(lastId);
            Assert.NotNull(record);
            Assert.Equal(_entity.Count, record.Count);
        }
    }
}
