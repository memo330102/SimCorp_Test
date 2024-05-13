using FluentAssertions;
using Moq;
using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimCorp_Test_Task.Tests.ServicesTests
{
    public class FileServiceTests 
    {
        private readonly FileService _fileService;
        private readonly Mock<ISort> _sortServiceMock;
        private string fileFolder = AppDomain.CurrentDomain.BaseDirectory;
        public FileServiceTests()
        {
            _sortServiceMock = new Mock<ISort>();
            _fileService = new FileService(_sortServiceMock.Object);
        }

        [Fact]
        public void FileService_ReadFromFile_Should_Return_Data_When_File_Exists()
        {
            var filePath = Path.Combine(fileFolder, "text.txt");
            var fileData = "Go do that thing that you do so well";

            _fileService.CreateAndWriteToFile(filePath, fileData);
            var result = _fileService.ReadFromFile(filePath);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(fileData);
        }
    }
}
