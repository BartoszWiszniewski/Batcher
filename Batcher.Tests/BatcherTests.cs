namespace Batcher.Tests
{
    using System.Collections.Generic;

    using Batcher.Tests.TestsData;

    using FluentAssertions;

    using Xunit;
    using Xunit.Frameworks.Autofac;

    [UseAutofacTestFramework]
    public class BatcherTests
    {
        private readonly IBatcherService batcher;

        public BatcherTests()
        {
        }

        public BatcherTests(IBatcherService batcher)
        {
            this.batcher = batcher;
        }

        [Theory]
        [ClassData(typeof(BatchTestData))]
        public void Batch_should_be_equivalent_to_expected(IEnumerable<int> expected, params IEnumerable<int>[] items)
        {
            var result = this.batcher.Batch(items);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(ContainerSplitToBatchesTestData))]
        public void Container_SplitToBatches_should_be_equivalent_to_expected(IEnumerable<IEnumerable<int>> expected, int itemsPerBatch, IEnumerable<int> items)
        {
            var result = this.batcher.SplitToBatches(itemsPerBatch, items);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(ContainersSplitToBatchesTestData))]
        public void Containers_SplitToBatches_should_be_equivalent_to_expected(IEnumerable<IEnumerable<int>> expected, int itemsPerBatch, params IEnumerable<int>[] itemsContainers)
        {
            var result = this.batcher.SplitToBatches(itemsPerBatch, itemsContainers);
            result.Should().BeEquivalentTo(expected);
        }
    }
}