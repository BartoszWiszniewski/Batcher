namespace Batcher.Tests.TestsData
{
    using System.Collections;
    using System.Collections.Generic;

    internal class BatchTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null, null };

            yield return new object[] { new int[] { }, new int[] { }, new List<int> { } };

            yield return new object[]
            {
                new int[] { 1, 2, 3 },
                new int[] { 1, 2, 3 }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 3 },
                new List<int> { 1, 2, 3 }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 2, 3 },
                new int[] { 4, 5 }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 3, 4, 5 },
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5 }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 3, 4, 5 },
                new int[] { 1, 2, 3 },
                new List<int> { 4, 5 }
            };
            yield return new object[]
            {
                new int[] { 1, 2, 3, 4, 5 },
                new List<int> { 1, 2, 3 },
                new int[] { 4, 5 }
            };

            yield return new object[]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                new List<int> { 1, 2, 3 },
                new int[] { 4, 5 },
                new List<int> { 6, 7, 8 },
                new int[] { 9, 0 }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}