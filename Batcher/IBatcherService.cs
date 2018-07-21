namespace Batcher
{
    using System.Collections.Generic;

    public interface IBatcherService
    {
        IEnumerable<Type> Batch<Type>(params IEnumerable<Type>[] itemsContainers);

        IEnumerable<IEnumerable<Type>> SplitToBatches<Type>(int itemsPerBatch, IEnumerable<Type> items);

        IEnumerable<IEnumerable<Type>> SplitToBatches<Type>(int itemsPerBatch, params IEnumerable<Type>[] itemsContainers);
    }
}