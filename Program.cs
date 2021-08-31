using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelExample
{
    class Program
    {
        static void Main(string[] args )
        {
            int limit = 1;
            int iteracion = 1;
            int incremento;
            

            if ( args.Length > 0 )
            if (!Int32.TryParse(args[0], out limit ))
                limit = 1;

            incremento = limit;
            if ( args.Length > 1 && !Int32.TryParse(args[1], out incremento ))
                incremento = limit;
            
            iteracion = limit / incremento;


            Console.WriteLine($"Límite: {limit}");
            Console.WriteLine($"Iteraciones: {iteracion}");
            Console.WriteLine($"Incremento: {incremento}");


            long iTotal = 0, watchTotal = 0, watchForParallelTotal = 0;

            Console.WriteLine($"|\tn\t|\tS (ms)\t|\tP (ms)\t|\t");    
            for (int i = incremento; i <= limit; i = i + incremento)
            {
                var numbers = Enumerable.Range(0, i).ToList();

                var watchForParallel = Stopwatch.StartNew();
                EjecutarTransaccionWithParallel(numbers);
                watchForParallel.Stop();

                var watch = Stopwatch.StartNew();
                EjecutarTransaccion(numbers);
                watch.Stop();

                Console.WriteLine($"|\t{i}\t|\t{watch.ElapsedMilliseconds}\t|\t{watchForParallel.ElapsedMilliseconds}\t|\t");    
                iTotal += i;
                watchTotal += watch.ElapsedMilliseconds;
                watchForParallelTotal  += watchForParallel.ElapsedMilliseconds;

            }
            
            Console.WriteLine($"Total n: {iTotal}\nTotal serial (ms):{watchTotal}\nTotal paralelo (ms):{watchForParallelTotal}\n");    
            // Console.ReadLine();
        }


        private static void EjecutarTransaccionWithParallel(IList<int> numbers)
        {
//            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                CuentasManager.Transaccion(
                    getCuenta(),
                    getMonto(),
                    getSigno()
                );
            });
        }

        
        private static void EjecutarTransaccion(IList<int> numbers)
        {
//            var primeNumbers = new ConcurrentBag<int>();
            foreach (var item in numbers)
            {
                CuentasManager.Transaccion(
                    getCuenta(),
                    getMonto(),
                    getSigno()
                );    
            }
        }

        
        private static int getCuenta (){
            Random rnd = new Random();
            return rnd.Next(1, 10);
        }

        private static decimal getMonto (){
            Random rnd = new Random();
            return Math.Round( Convert.ToDecimal(  rnd.Next(1, 10000) /100.00 ), 2);
        }

        private static int getSigno (){
            Random rnd = new Random();
            return rnd.Next(0, 1) == 1 ? 1 : -1;
        }

    }
}