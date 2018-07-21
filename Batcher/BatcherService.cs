namespace Batcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BatcherService : IBatcherService
    {
        public IEnumerable<Type> Batch<Type>(params IEnumerable<Type>[] itemsContainers)
        {
            if (itemsContainers == null)
            {
                return null;
            }

            var result = new List<Type>();
            foreach (var itemContainer in itemsContainers)
            {
                result.AddRange(itemContainer);
            }

            return result;
        }

        public IEnumerable<IEnumerable<Type>> SplitToBatches<Type>(int itemsPerBatch, params IEnumerable<Type>[] itemsContainers)
        {
            if (itemsContainers == null)
            {
                return null;
            }

            var result = new List<List<Type>>();

            var index = 0;
            for (var i = 0; i < itemsContainers.Length; i++)
            {
                var itemsContainer = itemsContainers[i];
                if (itemsContainer == null)
                {
                    continue;
                }

                var batches = this.SplitToBatches(itemsPerBatch, itemsContainer, index);

                // if we used next items container to fill last batch, we dont want duplicate items so we will need to skip this items for next batch
                index = this.CheckAndFillBatch(batches.Last(), itemsPerBatch, itemsContainers, ref i);

                result.AddRange(batches);
            }

            return result;
        }

        public IEnumerable<IEnumerable<Type>> SplitToBatches<Type>(int itemsPerBatch, IEnumerable<Type> items)
        {
            return this.SplitToBatches(itemsPerBatch, items, 0, 0);
        }

        private bool CanFillBatch<T>(List<T> batch, int itemsPerBatch, IEnumerable<T>[] itemsContainers, int forIndex)
        {
            return batch.Count() < itemsPerBatch && forIndex < itemsContainers.Length - 1;
        }

        private bool CanFillRemainingItems<T>(IEnumerable<T> items, int remaining)
        {
            return items.Count() > remaining;
        }

        private int CheckAndFillBatch<T>(List<T> batch, int itemsPerBatch, IEnumerable<T>[] itemsContainers, ref int forIndex)
        {
            var index = 0;

            while (this.CanFillBatch(batch, itemsPerBatch, itemsContainers, forIndex))
            {
                var remaining = itemsPerBatch - batch.Count();
                var containerIndex = forIndex + 1;
                var items = itemsContainers[containerIndex];

                this.SetIndexes(ref forIndex, ref index, remaining, containerIndex, items);

                var remainingItems = this.GetRemainingItems(items, remaining);
                batch.AddRange(remainingItems);
            }

            return index;
        }

        private List<List<Type>> GetBatches<Type>(int itemsPerBatch, IEnumerable<Type> items, int startIndex, int endIndex)
        {
            var result = new List<List<Type>>();

            for (var i = startIndex; i < endIndex; i += itemsPerBatch)
            {
                var itemsToTake = this.GetItemsToTakeCount(i, endIndex, itemsPerBatch);
                var itemsToAdd = items.Skip(i).Take(itemsToTake).ToList();

                result.Add(itemsToAdd);
            }

            return result;
        }

        private int GetBatchesCount(int itemsCount, int itemsPerBatch)
        {
            if (itemsPerBatch == 0)
            {
                throw new DivideByZeroException();
            }

            return Convert.ToInt32(Math.Ceiling((double)itemsCount / itemsPerBatch));
        }

        private int GetItemsToTakeCount(int index, int itemsCount, int itemsPerBatch)
        {
            var itemsToTake = itemsPerBatch;
            if (itemsCount - index < itemsPerBatch)
            {
                itemsToTake = itemsCount - index;
            }

            return itemsToTake;
        }

        private IEnumerable<T> GetRemainingItems<T>(IEnumerable<T> items, int remaining)
        {
            if (this.CanFillRemainingItems(items, remaining))
            {
                return items.Take(remaining).ToList();
            }

            return items.Take(remaining).ToList();
        }

        private void SetIndexes<T>(ref int forIndex, ref int index, int remaining, int containerIndex, IEnumerable<T> items)
        {
            if (this.CanFillRemainingItems(items, remaining))
            {
                index = remaining;
            }
            else
            {
                forIndex = containerIndex;
            }
        }

        /// <param name="endIndex">if less,equal 0 or grater than items count, will be set to items count</param>
        private List<List<Type>> SplitToBatches<Type>(int itemsPerBatch, IEnumerable<Type> items, int startIndex = 0, int endIndex = 0)
        {
            if (items == null)
            {
                return null;
            }

            if (this.ValidEndIndex(endIndex, items))
            {
                endIndex = items.Count();
            }

            return this.GetBatches(itemsPerBatch, items, startIndex, endIndex);
        }

        private bool ValidEndIndex<T>(int endIndex, IEnumerable<T> items)
        {
            return endIndex <= 0 || endIndex > items.Count();
        }
    }
}