using System.Collections.Generic;
using System.Linq;

namespace DataObfuscation.Application.Extensions
{
	public static class IEnumerableExtension
	{
		public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
			=> self.Select((item, index) => (item, index));
	}
}
