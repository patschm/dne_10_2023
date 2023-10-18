namespace ConsoleThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Synchroon();
            //ASynchroon();
            //ASyncSchoon();
            //AsyncFout();
            //AsyncCancel();
            //AsyncHip();
            //AsyncMess();
            AsyncPanic();
            Console.WriteLine("En verder...");
            Console.ReadLine();
        }

        static object stokje = new object();

        private static void AsyncPanic()
        {
            int counter = 0;

            Parallel.For(0, 10, idx => {
                Console.WriteLine($"Start thread {idx}");
                lock (stokje)
                {
                    int tmp = counter;
                    Task.Delay(500).Wait();
                    tmp++;
                    counter = tmp;
                    Console.WriteLine(counter);
                }
            });

        }

        private static async void AsyncMess()
        {
            var t1 = LongAddAsync(1, 2);
            var t2 = LongAddAsync(2, 3);
            var t3 = LongAddAsync(3, 4);


            await Task.WhenAll(t1, t2, t3);
            Console.WriteLine(t1.Result + t2.Result + t3.Result);
        }

        private static async Task AsyncHip()
        {
            var t1 = Task.Run(() => LongAdd(4, 5));

            int result = await t1;
            Console.WriteLine($"Result: {result}");

            result = await LongAddAsync(37, 5);
            Console.WriteLine($"Result: {result}");
        }

        private static void AsyncCancel()
        {
            CancellationTokenSource nikko = new CancellationTokenSource();
            CancellationToken bommetje = nikko.Token;


            Oneindig(bommetje);

            Task.Delay(5000).Wait();
            nikko.Cancel();
        }

        private static void AsyncFout()
        {
            //try
            //{
            //    Task.Run(() =>
            //    {
            //        Task.Delay(1000).Wait();
            //        throw new Exception("Oooops");
            //    });
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                throw new Exception("Oooops");
            }).ContinueWith(pt => { 
                if (pt.Exception != null) Console.WriteLine(pt.Exception.InnerException?.Message);
            });

        }

        private static void ASyncSchoon()
        {
            // Task<int> t1 = Task.Run(() => LongAdd(4, 5));
            Task.Run(() => LongAdd(4, 5))
                .ContinueWith(pTask => {
                    Console.WriteLine($"Result: {pTask.Result}");
                });
        
            

        }
        private static void ASynchroon()
        {
            //Task<int> t1 = new Task<int>(() => LongAdd(3, 4));
            //t1.Start();

            Task<int> t1 = Task.Run(() => LongAdd(4, 5));

            do {
                Console.Write(".");
                Task.Delay(100).Wait();
            } while (!t1.IsCompleted);

            Console.WriteLine($"Result: {t1.Result}");

        }
        private static void Synchroon()
        {
            int result = LongAdd(3, 4);
            Console.WriteLine($"Result: {result}");
        }

        static int LongAdd(int a, int b)
        {
            Task.Delay(10000).Wait();
            return a + b;
        }
        static Task<int> LongAddAsync(int a, int b)
        {
            return Task.Run(() => LongAdd(a, b));
        }

        static void Oneindig(CancellationToken bommetje)
        {
            Task.Run(() => {
                while (true)
                {
                    if (bommetje.IsCancellationRequested)
                    {
                        return;
                    }
                    Console.WriteLine("Ok");
                    Task.Delay(500).Wait();
                }
            });
        }
    }
}