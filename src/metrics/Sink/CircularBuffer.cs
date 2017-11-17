using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MessageHandler.Runtime.Metrics
{
    internal class CircularBuffer<T>
    {
        private ConcurrentQueue<T> _buffer = new ConcurrentQueue<T>();
        private readonly int _maxItemCount;

        public CircularBuffer(int maxItemCount)
        {
            _maxItemCount = maxItemCount;
        }

        public void Put(T item)
        {
            _buffer.Enqueue(item);
            if (_buffer.Count > _maxItemCount)
            {
                T dequeued;
                _buffer.TryDequeue(out dequeued);
            }
        }
        public IEnumerable<T> Clear()
        {
            var result = _buffer.ToArray();
            _buffer = new ConcurrentQueue<T>();
            return result;
        }
        
    }
}