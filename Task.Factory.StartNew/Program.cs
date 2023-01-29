using System;
using System.Threading;
using System.Text;
using System.IO;
namespace Run;

public class Example
{
   public static void Main()
   {
	//while true ; do cat fi.log ;sleep 1  ; done
	Console.WriteLine("Start Main");
	Task t = Task.Factory.StartNew(() => LongComputation());
	while(true)
	{
		Console.WriteLine("Computation proceed but you can use app.");
		string cmd = Console.ReadLine();
		if(cmd == "exit")
		{
			Console.WriteLine("see you next time!");
			break;
		}
		else
		{
			Console.WriteLine("I am async :) "+cmd );
		}

	}
   	Console.WriteLine("Finish Main");
   }

   static void LongComputation()
   {
	   Console.WriteLine("Start fibonacci computation:");
	   long prev1 = 0;
	   long prev2 = 1;

	   for(int i=1; i < 100; i++)
	   {
		Thread.Sleep(1000);
		long curr = prev1 + prev2;

		prev1 = prev2;
		prev2 = curr;
		if(curr < 0) break;
		File.AppendAllText("fi.log", curr + Environment.NewLine);
	   }
   }
}
