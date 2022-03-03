using System;
using CronExpressionParser;

namespace dot_net_cron_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //dotnet run "*/15 0 1,15 * 1-5 /usr/bin/find"
            string input = args[0];
            var cronExpressionParser = new CronExpressionParserWorker();
	        cronExpressionParser.Parse(input);
        }
    }
}
