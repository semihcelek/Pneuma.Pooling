using System.Collections.Generic;

namespace Pneuma.Pooling.Utilities
{
    public static class DictionaryHelper
    {
        public static TKey GetKeyFromValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
        {
            TKey key = default(TKey);
            
            if (!dictionary.ContainsValue(value))
            {
                return key;
            }
            
            foreach ((TKey key1, TValue value1) in dictionary)
            {
                if (EqualityComparer<TValue>.Default.Equals(value1, value))
                {
                    key = key1;
                    break;
                }
            }

            return key;
        }
    }
}
