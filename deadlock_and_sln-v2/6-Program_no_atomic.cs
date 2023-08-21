using System;
using System.Threading;

class DedlockExaple
{
	static int sec = 2000;

	static int _counter = 0;

        static Random _r;

	static void Main()
	{
		_r = new Random();
		var t1 = new Thread(() => T1("1"));
		var t2 = new Thread(() => T1("2"));

		t1.Start();
		t2.Start();

		t1.Join();
		t2.Join();
		Console.WriteLine("FInal result: "+_counter);
	}


	static void T1(string tID) 
	{
		for(;;)
		{

			_counter++;
			int v = _counter;
			Thread.Sleep(_r.Next(100, 2001));
			Console.WriteLine("tID=" + tID + ":" + v + ":" + _counter);

			if(_counter >= 5)
				break;
		}
		
	}

	static int GetRandom() 
	{
		return  new Random().Next(1,201);
	}

}

