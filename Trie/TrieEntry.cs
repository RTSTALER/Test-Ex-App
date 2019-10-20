using System.Collections.Generic;

namespace TestExApp
{
  
    public struct TrieEntry<TKey, TValue>
    {
        
        public TrieEntry(IEnumerable<TKey> key, TValue value)
        {
            Key = key;
            Value = value;
        }

       
        public IEnumerable<TKey> Key { get; }

      
        public TValue Value { get; }
    }
}
