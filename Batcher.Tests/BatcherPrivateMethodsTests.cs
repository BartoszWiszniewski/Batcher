/// <summary>
/// Example of testing private methods if we want our class to be like "black box"
/// but we still need unit tests, for private parts
/// </summary>
namespace Batcher.Tests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Xunit;
    using Xunit.Frameworks.Autofac;

    [UseAutofacTestFramework]
    public class BatcherPrivateMethodsTests
    {
        private readonly Helpers.IAccessHelper accessHelper;
        private readonly BatcherService batcher;

        public BatcherPrivateMethodsTests()
        {
        }

        public BatcherPrivateMethodsTests(Helpers.IAccessHelper accessHelper, BatcherService batcher)
        {
            this.accessHelper = accessHelper;
            this.batcher = batcher;
        }

        [Theory]
        [ClassData(typeof(TestsData.CheckAndFillBatchIndexTestData))]
        public void CheckAndFillBatch_should_be_equivalent_to_expected(
            int expectedIndex,
            int expectedForIndex,
            IEnumerable<int> expectedBatch,
            List<int> batch,
            int itemsPerBatch,
            IEnumerable<int>[] itemsContainers,
            int forIndex)
        {
            var result = this.accessHelper.CallGeneric<int, int>(this.batcher, "CheckAndFillBatch", batch, itemsPerBatch, itemsContainers, forIndex);

            result.Should().Be(expectedIndex);
            forIndex.Should().Be(expectedForIndex);
            batch.Should().BeEquivalentTo(expectedBatch);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 4, 3)]
        public void GetBatchesCount_should_be_equivalent_to_expected(int expected, int itemsCount, int itemsPerBatch)
        {
            var result = this.accessHelper.Call<int>(this.batcher, "GetBatchesCount", itemsCount, itemsPerBatch);

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0)]
        public void GetBatchesCount_should_throw_DivideByZeroException(int itemsCount, int itemsPerBatch)
        {
            Action call = () => { this.accessHelper.Call<int>(this.batcher, "GetBatchesCount", itemsCount, itemsPerBatch); };
            call.Should().ThrowExactly<DivideByZeroException>();
        }

        [Theory]
        [InlineData(10, 0, 10, 10)]
        [InlineData(2, 0, 2, 10)]
        [InlineData(1, 1, 2, 10)]
        [InlineData(5, 5, 25, 5)]
        [InlineData(0, 5, 5, 1)]
        public void GetItemsToTakeCount_should_be_equivalent_to_expected(int expected, int index, int itemsCount, int itemsPerBatch)
        {
            var result = this.accessHelper.Call<int>(this.batcher, "GetItemsToTakeCount", index, itemsCount, itemsPerBatch);

            result.Should().Be(expected);
        }
    }
}