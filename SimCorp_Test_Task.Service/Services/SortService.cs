using Serilog;
using SimCorp_Test_Task.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorp_Test_Task.Service.Services
{
    public class SortService : ISort
    {
        public IOrderedEnumerable<KeyValuePair<string, int>> OrderByDescending(Dictionary<string, int> countedWords)
        {
            if (countedWords == null)
            {
                Log.Error("ArgumentNullException: " + ErrorMessages.Input_Dictionary_Can_Not_Be_Null);
                throw new ArgumentNullException(nameof(countedWords), ErrorMessages.Input_Dictionary_Can_Not_Be_Null);
            }

            var sortedWordCount = countedWords
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key);

            return sortedWordCount;
        }
    }
}
