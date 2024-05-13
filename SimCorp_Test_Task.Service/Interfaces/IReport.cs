using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorp_Test_Task.Service.Interfaces
{
    public interface IReport
    {
        Dictionary<string, int> CountWords(string filePath);
    }
}
