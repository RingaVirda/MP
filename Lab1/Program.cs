using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var time1 = new TimeAT();
            Console.WriteLine($"time1: {time1}");
            var time2 = new TimeAT(1, 2, 3);
            Console.WriteLine($"time2: {time2}");
            var time3 = new TimeAT(new DateTime(1, 1, 1, 1, 2, 3));
            Console.WriteLine($"time3: {time3}");
            
            try
            {
                new TimeAT(30, 30, 30);
            }
            catch (ValueException e)
            {
                Console.WriteLine(e.Message);
            }
            
            var time4 = time1.Add(time2);
            Console.WriteLine($"time4 = time1 + time2: {time4}");
            var time5 = time4.Sub(time3);
            Console.WriteLine($"time5 = time4 - time3: {time5}");

            var time6 = time5 + time4;
            Console.WriteLine($"time6 = time4 + time5: {time6}");
            var time7 = time6 - new TimeAT(10, 10, 10);
            Console.WriteLine($"time7 = time6 - 10:10:10 : {time7}");
        }
    }
}