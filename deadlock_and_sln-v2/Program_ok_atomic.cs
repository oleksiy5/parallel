using System;
using System.Threading;
using System.Threading.Tasks;

class DedlockExaple
{
	static int _counter = 0;

	static void Main()
	{
		int numTasks = 10;
		Task[] tasks = new Task[numTasks];
		for(int i=0; i <numTasks; i++)
		{
			tasks[i] = Task.Run(() => T1(i.ToString()));
		}
		Task.WaitAll(tasks);

		Console.WriteLine("_counter: "+_counter);
	}


	//oczekiwany wynik 10*10000=100 000
	static void T1(string tID) 
	{
		int internalCount = 0;
		for(int i=0; i<10000; i++)
		{
			internalCount++;
			Interlocked.Increment(ref _counter);
		}	
		Console.WriteLine("tID=" + tID + "; internalCount=" + internalCount);
	}
}
