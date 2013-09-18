using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace linq
{
    public class Fibonacci
    {
        public IEnumerable<long> Fib(int n, bool asParallel)
        {
            //var range = Enumerable.Range(0, n);
            var range = asParallel
                            //? Enumerable.Range(0, n).AsParallel()
                            ? ParallelEnumerable.Range(0, n).WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                            : Enumerable.Range(0, n);
            
            var toReturn = range
                .Select(x =>
                    {
                        Func<int, long> fib = null;
                        fib = (y =>
                            {
                                if (y < 2) return (long) y;
                                return (fib(y - 1) + fib(y - 2));
                                //return (Task<long>.Factory.StartNew(() => fib(y - 1)).Result + fib(y - 2));
                            });
                        return fib(x);
                    }).ToList();
            return toReturn;
        }

        public IEnumerable<long> Fib2(int n)
        {
            var t = new Stopwatch();
            t.Start();
            Func<int, long, long, IEnumerable<long>> localFib = null;
            localFib = (k, last, nextLast) =>
                {
                    var next = last + nextLast;
                    return k <= 1 
                        ? Enumerable.Repeat(nextLast, 1) 
                        : Enumerable.Repeat(nextLast, 1).Concat(localFib(k - 1, next, last));
                };
            var toReturn = localFib(n, 1, 0).ToList();
            return toReturn;
        }
    }
}
