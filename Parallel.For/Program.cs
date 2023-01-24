using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Run;

public class Example
{
   public static void Main()
   {
   	Console.WriteLine("use: sync/async/exit");
   	while(true)
   	{
	      var com = new Compute();
	      string mod = Console.ReadLine();
	      if(mod == "exit")
	      	break;
	      int r = com.GetResult(mod); 
	      Console.WriteLine("result({0}): {1}", mod, r);
        }
   }
}


class Compute
{
	public int GetResult(string mod)
	{     
	    int[] data = {1,2,3,4,5};
	    int[] r = new int[data.Length];
	    if(mod == "async")
	    {
		    Parallel.For(0, data.Length, i => 
				    {
				       r[i] = Mod(data[i],"t"+data[i]);
				    });
	    }
	    else if(mod == "sync")
            {
		    var tasks = new List<Task<int>>();
		    for(int i=0;i<data.Length;i++)
		    {
			    r[i] = Mod(data[i],"t"+data[i]);
		    }
	    }
	    return r.Sum();
	}

	public int Mod(int i, string log)
        {
           int mod = i % 2;
           int sleep = (3000);

           Console.WriteLine(log+";time"+sleep+";result "+mod);

	   Thread.Sleep(sleep);

           return mod;
        }	
}
