﻿using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using Serilog;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text;

ISort _sortService = new SortService();
IFile _fileService = new FileService(_sortService);
IReport _reportService = new ReportService(_fileService);

var fileFolder = AppDomain.CurrentDomain.BaseDirectory;
var filePathLogging = Path.Combine(fileFolder, "logs.txt");
if (!_fileService.IsFileExist(filePathLogging))
{
    _fileService.CreateAndWriteToFile(filePathLogging, "");
}
// Logging configuration
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.File(filePathLogging)
.CreateLogger();


var filePath = Path.Combine(fileFolder, "text.txt");
if (!_fileService.IsFileExist(filePath))
{
    _fileService.CreateAndWriteToFile(filePath, "Go do that thing that you do so well");
}
Dictionary<string, int> wordCount = _reportService.CountWords(filePath);
_fileService.PrintWordsToConsole(wordCount);
