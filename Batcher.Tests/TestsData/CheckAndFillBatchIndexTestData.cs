namespace Batcher.Tests.TestsData
{
    using System.Collections;
    using System.Collections.Generic;

    internal class CheckAndFillBatchIndexTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                2,
                0,
                new List<int> { 1, 2, 3, 4, 5 },
                new List<int> { 1, 2, 3 },
                5,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 },
                    new List<int> { 4, 5, 6, 7 }
                },
                0
            };

            yield return new object[]
            {
                0,
                0,
                new List<int> { 1, 2, 3 },
                new List<int> { 1, 2, 3 },
                5,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 }
                },
                0
            };

            yield return new object[]
            {
                0,
                0,
                new List<int> { 1, 2, 3 },
                new List<int> { 1, 2, 3 },
                5,
                new IEnumerable<int>[]
                {
                    new List<int> { 1, 2, 3 }
                },
                0
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}