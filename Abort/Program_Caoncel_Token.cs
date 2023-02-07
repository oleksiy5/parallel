using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
namespace Run;

public class Example
{

   public static void Main()
   {
	//while true ; do cat fi.log ;sleep 1  ; done
	//while true; do clear;tail -1 fi.log;sleep 2;clear; done
	
	var cts = new CancellationTokenSource();
	var token = cts.Token;

	Thread t = new Thread(() => LongComputation(token));
	t.IsBackground = false;//true-background, false-foreground
	t.Start();
	var r = new Random();
	while(true)
	{
		Console.WriteLine("1-10/exit/abort");
		Console.WriteLine("Guess the number from 1 to 10. (type number or exit command.)");

		string cmd = Console.ReadLine();

		if(cmd == "exit")
		{
			Console.WriteLine("see you next time!");
			break;
		}
		else if(cmd == "abort")
		{
			cts.Cancel();
			Console.WriteLine("Thread has aborted ...");
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

   static void LongComputation(CancellationToken token)
   {
	   Console.WriteLine("Start fibonacci computation:");
	   long prev1 = 0;
	   long prev2 = 1;

	   for(int i=1; i < 100; i++)
	   {

		if(token.IsCancellationRequested)
		{
			Console.WriteLine(">Cancel-request<");
			break;
		}

		Thread.Sleep(1000);
		long curr = prev1 + prev2;

		prev1 = prev2;
		prev2 = curr;
		if(curr < 0) break;
		File.AppendAllText("fi.log", curr + Environment.NewLine);
	   }
   }
}

