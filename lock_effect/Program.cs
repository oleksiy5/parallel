using System;
using System.Threading;
using System.IO;
using System.Diagnostics;


namespace playground
{
    class Program
    {
        static string[] _bigArr = null;
        
        static int countFor9 = 0; 
        static int countFor0 = 0;
        static int countForOther = 0;
        static List<string> ls_0 = new List<string>();
        static int sum = 0;

        static int delay = 50;

        static async Task Main(string[] args)
		{
            StartDemo();
            LoadMockData();
            
            Console.WriteLine("(s)ync/(a)/sync/(al)async_with_lock");
            string mode = Console.ReadLine();

            var timer = new Stopwatch();
            timer.Start();
             
            {
                if(mode == "s")
                    MakeCalculationsFromBegin();
                else if(mode == "a" || mode == "al")        
                    await MakeCalculationsAsync(mode);   
            }
         
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff"); 
            Console.WriteLine(foo);
            DisplayResult();
        }

        //ver. asynch
        static async Task MakeCalculationsAsync(string mode)
        {
            var tasks = new List<Task>();
            
            int min = 0;
            int max = _bigArr.Length;
            int part= 200 * 1000;//200K 
            int div = max/part;
            Console.WriteLine(div);
                         
            var integerList = new List<int>();           
            for(int t=0;t<div;t++)            
                integerList.Add(t);
            
            Parallel.ForEach(integerList, t =>
            {
                int start = t * part;
                int end = part;
                int divL = (div - 1);   
                if(t == divL)//last part                
                    end = max - (divL * part);                
                MakeCalculationsPartly(t, start, end, mode);
            });        
        }

        static void MakeCalculationsPartly(int threadID,int start, int end, string mode)
        {
            if(ls_0 == null)
                ls_0 = new List<string>();

            Console.WriteLine("THID =" + threadID + "_" + start + "_" + end);

            long l = _bigArr.Length; 
            int sleep = 5;
            int index = 3;                
            for(long i = start; i < (start+end); i++)
            {
                Console.WriteLine("TASK_"+threadID+"_START_"+start+"_END_"+end+"_IDX_"+i);                            
                string s = _bigArr[i];
                int charPos = (s.Length - index);                        
                string ch = s.Substring(charPos, index); 
                if(ch == "000")
                {
                    if(mode == "al")
                    {
                        lock(ls_0)
                        {                                    
                            ls_0.Add(s);
                            Thread.Sleep(delay);
                        }
                    }    
                    else
                    {
                        ls_0.Add(s);
                        Thread.Sleep(delay);
                    }                    
                }
            }
        }

        //problem bez LOCK'a, daje nieprzewidywalne wyniki
        //expected result 848   
        static void MakeCalculationsFromBegin()
        {
            if(ls_0 == null)
                ls_0 = new List<string>();

            long l = _bigArr.Length; 
            int sleep = 5;
            int index = 3;
                
            for(long i=0; i < l; i++)
            {
                Console.WriteLine("BEGIN__"+i);                            
                string s = _bigArr[i];
                int charPos = (s.Length - index);                        
                string ch = s.Substring(charPos, index); 
                if(ch == "000")
                {
                        ls_0.Add(s);
                        Thread.Sleep(delay);                                            
                }
            }                                    
        }
        static void DisplayResult()
        {
            Console.Write("RESULT: ");
            Console.WriteLine("ls_0: " + ls_0.Count());    
            Console.WriteLine("---");
        }

        static void LoadMockData()
        {
           _bigArr = File.ReadAllLines("big_file.csv");
           Console.WriteLine(_bigArr.Length);
        }

        static void StartDemo()
        {
            Console.WriteLine("Start playground");
            Console.WriteLine("");
            int i = 0;
            for(;;)
            {
                i++;
                Thread.Sleep(150);
                Console.Write(". ");
                if(i == 5)
                   break;
            }

        }
    } 
}
