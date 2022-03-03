using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CronExpressionParser.Tests
{
    public class CronExpressionParserTests
    {
        [Fact]
        public void ParseField_commaSeperated()
        {
            var cronExpressionParser = new CronExpressionParserWorker();
            List<int> actual = cronExpressionParser.ParseField("1,2,3", 60).ToList();
            List<int> expected = new() {1, 2, 3};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseField_wildcard()
        {
            var cronExpressionParser = new CronExpressionParserWorker();
            List<int> actual = cronExpressionParser.ParseField("*", 15, true).ToList();
            List<int> expected = new() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseField_range()
        {
            var cronExpressionParser = new CronExpressionParser();
            List<int> actual = cronExpressionParser.ParseField("1-5", 15, true).ToList();
            List<int> expected = new() {1, 2, 3, 4, 5};

            Assert.Equal(expected, actual);
        }
    }
}
