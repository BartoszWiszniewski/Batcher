namespace Batcher.Tests.TestsData
{
    using System.Collections;
    using System.Collections.Generic;

    internal class ContainersSplitToBatchesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null, 0, null };

            yield return new object[]
            {
                new List<List<int>>
                {
                    new List<int> { 1, 2, 3 }
                },
                5,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 }
                }
            };

            yield return new object[]
            {
                new List<List<int>>
                {
                    new List<int> { 1, 2, 3, 4, 5 }
                },
                5,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 },
                    new List<int> { 4, 5 }
                }
            };

            yield return new object[]
            {
                new List<List<int>>
                {
                    new List<int> { 1, 2, 3 },
                    new List<int> { 4, 5 }
                },
                3,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 },
                    new List<int> { 4, 5 }
                }
            };

            yield return new object[]
            {
                new List<List<int>>
                {
                    new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }
                },
                10,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 },
                    new List<int> { 4, 5 },
                    new List<int> { 6, 7 },
                    new List<int> { 8 }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}