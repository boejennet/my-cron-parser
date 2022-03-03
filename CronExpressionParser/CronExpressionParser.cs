using System;
using System.Collections.Generic;
using System.Linq;

namespace CronExpressionParser
{
    public class CronExpressionParserWorker
    {
        public int FieldNameLength = 14;
        
        public void Parse(string input)
        {
            var splitInput = input.Split(' ');
            
            if(splitInput.Length != 6)
            {
                Console.WriteLine("Incorrect format.");
                return;
            }
            
            var minutes = ParseField(splitInput.ElementAt(0), 60, true);
            var hours = ParseField(splitInput.ElementAt(1), 24);
            var daysOfMonth = ParseField(splitInput.ElementAt(2), 31);
            var months = ParseField(splitInput.ElementAt(3), 12);
            var daysOfWeek = ParseDaysOfWeek(splitInput.ElementAt(4));
            var command = splitInput.ElementAt(5);
            
            Print(minutes, hours, daysOfMonth, months, daysOfWeek, command);
        }
        
        private void Print(IEnumerable<int> minutes, IEnumerable<int> hours, IEnumerable<int> daysOfMonth,
            IEnumerable<int> months, IEnumerable<int> daysOfWeek, string command)
        {
            Console.WriteLine($"{GetFieldName("minute")}{string.Join(" ", minutes)}");
            Console.WriteLine($"{GetFieldName("hour")}{string.Join(" ", hours)}");
            Console.WriteLine($"{GetFieldName("day of month")}{string.Join(" ", daysOfMonth)}");
            Console.WriteLine($"{GetFieldName("month")}{string.Join(" ", months)}");
            Console.WriteLine($"{GetFieldName("day of week")}{string.Join(" ", daysOfWeek)}");
            Console.WriteLine($"{GetFieldName("command")}{command}");
        }
        
        private IEnumerable<int> ParseDaysOfWeek(string input)
        {
            var parseVal = ParseField(input, 7);
            
            if(parseVal != null)
                return parseVal;
                
            if(input.Contains(","))
            {
                List<int> daysNum = new List<int>();
                var splitInput = input.Split(',');
                foreach(var day in splitInput)
                {
                    weekday dayEnum = new weekday();
                    if (Enum.TryParse(day, out dayEnum))
                    {
                        daysNum.Add((int)dayEnum);
                    }
                }
                return daysNum;
            }
            
            return null;
        }
        
        public IEnumerable<int> ParseField(string input, int maxRange, bool baseZero = false)
        {
            try
            {
                if (input == "*")
                {
                    return Range(baseZero ? 0 : 1, maxRange, 1);
                }
                else if (input.Contains("*/"))
                {
                    int interval = int.Parse(input.Substring(2));
                    return Range(0, 59, interval);
                }
                else if (input.Contains(","))
                {
                    var output = new List<int>();
                    foreach(var i in input.Split(','))
                    {
                        int outInt;
                        if (int.TryParse(i, out outInt))
                            output.Add(outInt);
                        else
                            throw new Exception();
                    }
                    return output;
                }
                else if (input.Contains("-"))
                {
                    var limits = input.Split('-').Select(i => int.Parse(i));
                    return Range(limits.ElementAt(0), limits.ElementAt(1), 1);
                }
                else if (int.TryParse(input, out int outInt))
                {
                    return new int[] { outInt };
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }

            return null;
        }
        
        private string GetFieldName(string name)
        {
            return String.Concat(name.PadRight(FieldNameLength).Take(FieldNameLength));
        }

        private IEnumerable<int> Range(int min, int max, int step)
        {
            for (int i = min; i <= max; i += step) yield return i;
        }
        
        private enum weekday
        {
            mon = 1,
            tue = 2,
            wed = 3,
            thu = 4,
            fri = 5,
            sat = 6,
            sun = 7
        }
        
        private enum month
        {
            jan = 1,
            feb = 2,
            mar = 3,
            apr = 4,
            may = 5,
            jun = 6,
            jul = 7,
            aug = 8,
            sep = 9,
            oct = 10,
            nov = 11,
            dec = 12
        }
    }
}
