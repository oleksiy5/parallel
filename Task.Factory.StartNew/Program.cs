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
	var r = new Random();
	while(true)
	{
		Console.WriteLine("Long computation proceed has started, but you can use app...");
		Console.WriteLine("Guess the number from 1 to 10. (type number or exit command.)");

		string cmd = Console.ReadLine();

		if(cmd == "exit")
		{
			Console.WriteLine("see you next time!");
			break;
		}
		else
		{
			int random = r.Next(1, 10);
			if(cmd == random.ToString())
				Console.WriteLine("You are WIN!. The number is "+ random) ;
			else
				Console.WriteLine("Try again. The number is "+ random);
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
