using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Run;

public class Example
{
   public static void Main()
   {
   	Console.WriteLine("use: async/exit");
   	while(true)
   	{
	      var com = new Compute();
	      string mod = Console.ReadLine();
	      if(mod == "exit")
	      	break;
	      com.Exec(mod); 
        }
   }
}


class Compute
{
	public async Task Exec(string mod)
	{     
	    int[] data = {1,2,3,4,5};
	    var tasks = new List<Task<int>>();
	   
	    if(mod == "async")
	    {
	   	    for(int i = 0;i < data.Length; i++)
	    	    {
	    	    	tasks.Add(Task.Factory.StartNew(() => Mod(i,"t"+i)));
	    	    }	  
	    }
	    
	    var whenAll = Task.WhenAll(tasks);
	    await whenAll;
	    int sum = whenAll.Result.Sum();
	    Console.WriteLine("sum:{0}",sum);
	}

	int Mod(int i, string log)
        {
           int mod = i % 2;
           int sleep = (3000);
	   Thread.Sleep(sleep);

           Console.WriteLine(log+";time"+sleep+";result "+mod);

           return mod;
        }	
}
