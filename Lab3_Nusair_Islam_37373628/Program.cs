using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Lab3Q1
{
    class Program
    {
        static void Main(string[] args)
        {
            // map and mutex for thread safety
            Stopwatch sw = new Stopwatch();
          Mutex mutex = new Mutex();
          Dictionary<string, int> wcountsSingleThread = new Dictionary<string, int>();


          var filenames = new List<string> {
                "../../data/shakespeare_antony_cleopatra.txt",
                "../../data/shakespeare_hamlet.txt",
                "../../data/shakespeare_julius_caesar.txt",
                "../../data/shakespeare_king_lear.txt",
                "../../data/shakespeare_macbeth.txt",
                "../../data/shakespeare_merchant_of_venice.txt",
                "../../data/shakespeare_midsummer_nights_dream.txt",
                "../../data/shakespeare_much_ado.txt",
                "../../data/shakespeare_othello.txt",
                "../../data/shakespeare_romeo_and_juliet.txt",
           };

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE THREAD
            //=============================================================
            sw.Start();
           foreach(var f in filenames)
            {
                HelperFunctions.countcharacterwords(f, mutex, wcountsSingleThread);
            }
            List<Tuple<int, string>> sortedTuples = 
                HelperFunctions.sortcharactersbywordcount(wcountsSingleThread);

            HelperFunctions.printlistoftuples(sortedTuples);
            sw.Stop();
            Console.WriteLine( $"SingleThread is Done! Time elapsed: {sw.ElapsedMilliseconds}ms");
            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN MULTIPLE THREADS
            //=============================================================
            Dictionary<string, int> wcountsMultiThread = new Dictionary<string, int>();
            List<Thread> threadList = new List<Thread>();
            sw.Restart();
            foreach(var f in filenames)
            {
                Thread t = new Thread(() => 
                HelperFunctions.countcharacterwords(f,mutex, wcountsMultiThread));

                threadList.Add(t);
                t.Start();
            }
            foreach(Thread t in threadList)
            {
                t.Join();
            }
            List<Tuple<int, string>> multiTupleSorted =
                HelperFunctions.sortcharactersbywordcount(wcountsMultiThread);

            HelperFunctions.printlistoftuples(multiTupleSorted);

            sw.Stop();
           Console.WriteLine( $"MultiThread is Done! Time elapsed: {sw.ElapsedMilliseconds}ms");
           return;
        }
    }
}
