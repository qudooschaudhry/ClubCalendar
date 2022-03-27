using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Domain.Extensions
{
    public static class TableStorageExtensions
    {
        public static Guid StringToGuidKey(this string rowKey)
        { 
            return new Guid(rowKey);
        }

        public static string GuidToStringKey(this Guid id)
        {
            return $"{id}";
        }
    }
}
