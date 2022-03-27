using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Extensions
{
    public static class UtilityExtensions
    {
        public static T ThrowIfNull<T>(this T o, string paramName) where T : class 
        {
            if (o ==null) 
                throw new ArgumentNullException(paramName);

            return o;
        }
    }
}
