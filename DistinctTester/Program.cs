using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistinctTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products =   
    {  
        new Product { Name = "apple", Code = 9 },   
        new Product { Name = "orange", Code = 4 },   
        new Product { Name = "apple", Code = 9 },  
        new Product { Name = "lemon", Code = 9 }  
    };

            //Exclude duplicates.  
            IEnumerable<Product> noduplicates = products.Distinct(p => p.Name).Distinct(p => p.Code);

            foreach (var product in noduplicates)
            {
                Console.WriteLine(product.Name + " " + product.Code);
            }

            Console.ReadLine();
        }
    }

    public class Product
    {
        public string Name
        {
            get;
            set;
        }

        public int Code
        {
            get;
            set;
        }
    }

    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;

        public CommonEqualityComparer(Func<T, V> keySelector)
        {
            this.keySelector = keySelector;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
        }
    }

    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }
    }  
}
