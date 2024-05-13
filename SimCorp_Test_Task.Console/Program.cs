using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var filePathLogging = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(filePathLogging)
    //.WriteTo.Console()
    .CreateLogger();

var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "text.txt");

IReport reporter = new ReportService();
IFile file = new FileService();
Dictionary<string, int> wordCount = reporter.CountWords(filePath);
file.PrintWordsToConsole(wordCount);
