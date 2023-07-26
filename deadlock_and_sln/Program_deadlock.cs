using System;
using System.Threading;

class DedlockExaple
{
	static int sec = 2000;
	static object resourceA  = new object();
	static object resourceB  = new object();

	static void Main()
	{
		Console.WriteLine("Deadlock example");
		var t1 = new Thread(T1);
		var t2 = new Thread(T2);
		t1.Start();
		t2.Start();

		t1.Join();
		t2.Join();

		Console.WriteLine("Exec has completed.");

	}

	static void T1() 
	{
		lock(resourceA)
		{
			Console.WriteLine("t1 lock resA");
			Thread.Sleep(sec);
			Console.WriteLine("t1 waiting for resB");
			
			lock(resourceB)
			{
				Console.WriteLine("t1 locked resB");
			}
		}
	}

	static void T2() 
	{
		lock(resourceB)
		{
			Console.WriteLine("t2 lock resB");
			Thread.Sleep(sec);
			Console.WriteLine("t2 waiting for resA");
			
			lock(resourceA)
			{
				Console.WriteLine("t2 locked resA");
			}
		}
	}

}
