using FluentAssertions;
using SimCorp_Test_Task.Service.Interfaces;
using SimCorp_Test_Task.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimCorp_Test_Task.Tests.ServicesTests
{
    public class SortServiceTests : IClassFixture<SortService>
    {
        private SortService _sortService;
        public SortServiceTests(SortService sortService)
        {
            _sortService = sortService;
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Return_Sorted_Words_When_Dictionary_Is_Not_Null()
        {
            var countedWords = new Dictionary<string, int>
            {
                { "do", 3 },
                { "that", 1 },
                { "Go", 2 }
            };

            var result = _sortService.OrderByDescending(countedWords);

            result.Should().NotBeNull();
            result.First().Key.Should().Be("do");
            result.Last().Key.Should().Be("that");
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Throw_Argument_Null_Exception_When_Dictionary_Is_Null()
        {
            Dictionary<string, int> countedWords = null;

            Action action = () => _sortService.OrderByDescending(countedWords);

            action.Should().Throw<ArgumentNullException>()
                .WithMessage("The input dictionary cannot be null. (Parameter 'countedWords')")
                .And.ParamName.Should().Be("countedWords");
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Sort_Dictionary_By_Value_Then_By_Key()
        {
            var countedWords = new Dictionary<string, int>
            {
                {"do", 2},
                {"that", 2},
                {"Go", 1},
                {"so", 1},
                {"thing", 1},
                {"well", 1},
                {"you", 1}
            };

            var sortedWordCount = _sortService.OrderByDescending(countedWords).ToDictionary(k => k.Key, v => v.Value);

            sortedWordCount.Should().ContainInOrder(countedWords.OrderByDescending(x => x.Value).ThenBy(x => x.Key));
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Return_Empty_Dictionary_When_Input_Dictionary_Is_Empty()
        {
            var countedWords = new Dictionary<string, int>();

            var sortedWordCount = _sortService.OrderByDescending(countedWords);

            sortedWordCount.Should().BeEmpty();
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Return_Sorted_Words_When_Multiple_Words_Have_The_Same_Count()
        {
            var countedWords = new Dictionary<string, int>
            {
                { "do", 3 },
                { "that", 1 },
                { "Go", 3 },
                { "so", 1 },
            };

            var result = _sortService.OrderByDescending(countedWords);

            result.Should().NotBeNull();
            result.First().Key.Should().Be("do");
            result.Last().Key.Should().Be("that");
        }

        [Fact]
        public void SortService_OrderByDescending_Should_Sort_Dictionary_By_Key_If_Dictionary_Contains_Multiple_Elements_With_Same_Value()
        {
            var countedWords = new Dictionary<string, int>
            {
                {"do", 2},
                {"that", 2},
                {"Go", 2},
                {"so", 2},
                {"thing", 2},
                {"well", 2},
                {"you", 2}
            };

            var sortedWordCount = _sortService.OrderByDescending(countedWords);

            var expected = countedWords
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToList();

            sortedWordCount.ToList().Should().ContainInOrder(expected);
        }
    }
}
