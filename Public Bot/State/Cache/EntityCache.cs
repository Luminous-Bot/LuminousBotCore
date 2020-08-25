using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public_Bot
{
    public class DuplicateIdItemException : Exception
    {
        public DuplicateIdItemException() { }
        public DuplicateIdItemException(string message) : base(message) { }
        public DuplicateIdItemException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateIdItemException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class SingleIDEntityCache<T> where T : class, IEntityID 
    {
        private Type _type = typeof(T);
        private List<T> _CacheList { get; set; } = new List<T>();
        public T this[ulong Id] 
        { 
            get => _CacheList.Any(x => x.Id == Id) ? _CacheList.Find(x => x.Id == Id) : null; 

            set => AddOrReplace(value); 
        }

        /// <summary>
        /// Returns the ammount of items in the Cache
        /// </summary>
        public int Count 
            => _CacheList.Count;

        /// <summary>
        /// Adds a new item in the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            // Since we should not have 2 users with the same id, check this :D
            if (_CacheList.Any(x => x.Id == value.Id))
                throw new DuplicateIdItemException($"List already contains {value.Id} for type of {_type.Name}!");
            
            // We passed the check, so lets add it!
            _CacheList.Add(value);
        }

        /// <summary>
        /// Replaces an item in the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Replace(T value)
        {
            // Check if that item exists
            if (_CacheList.Any(x => x.Id == value.Id))
            {
                // Find the index of the requested item
                var indx = _CacheList.FindIndex(x => x.Id == value.Id);

                // Replace it with the value
                _CacheList[indx] = value;
            }
            else
            {
                // If it didn't exist in the list, add it to it
                Add(value);
            }
        }

        /// <summary>
        /// Adds an item into the cache, if it already exists then it will be replaced
        /// </summary>
        /// <param name="value"></param>
        public void AddOrReplace(T value)
        {
            if (_CacheList.Any(x => x.Id == value.Id))
                Replace(value);
            else
                Add(value);
        }
        
        /// <summary>
        /// Returns a result based on an expression
        /// </summary>
        /// <param name="predicate">The expression to search by</param>
        /// <returns></returns>
        public bool Any(Func<T, bool> predicate)
            => _CacheList.Any(predicate);

        /// <summary>
        /// Clears the Cache
        /// </summary>
        public void Clear()
            => _CacheList.Clear();

        /// <summary>
        /// Returns if the item exists in the Cache
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
            => _CacheList.Contains(value);

        /// <summary>
        /// Removes an item from the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
            => _CacheList.Remove(value);
    }
    public class DoubleIDEntityCache<T> where T : class, IDoubleEntityID
    {
        private Type _type = typeof(T);
        private List<T> _CacheList { get; set; } = new List<T>();
        public T this[ulong Id, ulong GuildID] { get => _CacheList.Any(x => x.Id == Id && x.GuildID == GuildID) ? _CacheList.Find(x => x.Id == Id && x.GuildID == GuildID) : null; set => AddOrReplace(value); }

        /// <summary>
        /// Returns the ammount of items in the Cache
        /// </summary>
        public int Count
            => _CacheList.Count;

        /// <summary>
        /// Adds a new item in the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            // Since we should not have 2 users with the same id, check this :D
            if (_CacheList.Any(x => x.Id == value.Id && x.GuildID == value.GuildID))
                throw new DuplicateIdItemException($"List already contains {value.Id} for type of {_type.Name}!");

            // We passed the check, so lets add it!
            _CacheList.Add(value);
        }

        /// <summary>
        /// Replaces an item in the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Replace(T value)
        {
            // Check if that item exists
            if (_CacheList.Any(x => x.Id == value.Id && x.GuildID == value.GuildID))
            {
                // Find the index of the requested item
                var indx = _CacheList.FindIndex(x => x.Id == value.Id && x.GuildID == value.GuildID);

                // Replace it with the value
                _CacheList[indx] = value;
            }
            else
            {
                // If it didn't exist in the list, add it to it
                Add(value);
            }
        }

        /// <summary>
        /// Adds an item into the cache, if it already exists then it will be replaced
        /// </summary>
        /// <param name="value"></param>
        public void AddOrReplace(T value)
        {
            if (_CacheList.Any(x => x.Id == value.Id && x.GuildID == value.GuildID))
                Replace(value);
            else
                Add(value);
        }

        /// <summary>
        /// Returns a result based on an expression
        /// </summary>
        /// <param name="predicate">The expression to search by</param>
        /// <returns></returns>
        public bool Any(Func<T, bool> predicate)
            => _CacheList.Any(predicate);

        /// <summary>
        /// Clears the Cache
        /// </summary>
        public void Clear()
            => _CacheList.Clear();

        /// <summary>
        /// Returns if the item exists in the Cache
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
            => _CacheList.Contains(value);

        /// <summary>
        /// Removes an item from the Cache
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
            => _CacheList.Remove(value);
    }
}
