using System;
using System.Threading;

class DedlockExaple
{
	static readonly object  _lockP = new();
	static readonly object  _lockRW = new();

	static int _printCounter;
	static int _readWriteCounter;
	
	static void Main()
	{
		Console.WriteLine("Deadlock example");

		var t1 = new Thread(() => ReadFileAndPrint("t1"));
		//var t2 = new Thread(() => ReadFileAndPrint("t2"));
		var t2 = new Thread(() => PrintAndWriteFile("txt to print","t2"));
		t2.Start();
		t1.Start();
		
		t1.Join();
		t2.Join();
		
		Console.WriteLine("Exec has completed.");
	}

	static void ReadFileAndPrint(string thID)
	{
		lock(_lockRW)
		{
			Read(thID);
			lock(_lockP)
			{
				Print(thID);
			}
		}
	}
	
	static void PrintAndWriteFile(string txt, string thID)
	{
		lock(_lockP)
		{
			Print(thID);
			lock(_lockRW)
			{
				Write(thID);	
			}
		}
	}

	static void Print(string thID)
	{
		Console.WriteLine("print "+thID);
		Thread.Sleep(200);
		_printCounter++;
	}

	static void Write(string thID)
	{
		Console.WriteLine("write "+thID);
		Thread.Sleep(200);
		_readWriteCounter++;
	}
	 
	static void Read(string thID)
	{
		Console.WriteLine("read "+thID);
		Thread.Sleep(200);
		_readWriteCounter++;
	}
}
