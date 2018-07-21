namespace Batcher.Tests.TestsData
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class ContainerSplitToBatchesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null, 5, null };
            yield return new object[]
            {
                new IEnumerable<int>[] { },
                3,
                new int[] { }
            };

            yield return new object[]
            {
                new IEnumerable<int>[] { (new List<int> { 1, 2, 3 }).AsEnumerable() },
                3,
                new List<int> { 1, 2, 3 }
            };

            yield return new object[]
            {
                new IEnumerable<int>[] { (new List<int> { 1, 2, 3 }).AsEnumerable() },
                3,
                new int[] { 1, 2, 3 }
            };

            yield return new object[]
            {
                new IEnumerable<int>[]
                {
                    (new List<int> { 1, 2, 3 }).AsEnumerable(),
                    (new List<int> { 4, 5 }).AsEnumerable()
                },
                3,
                new int[] { 1, 2, 3, 4, 5 }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}