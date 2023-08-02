using System;
using System.Threading;

class DedlockExaple
{
	//todo show bad example with lock
	static int  _lockP = 0;
	static object  _lockRW = new();

	static int _printCounter;
	static int _readWriteCounter;
	
	static void Main()
	{
		Console.WriteLine("Deadlock example");

		var t1 = new Thread(() => ReadFileAndPrint("t1"));
		//var t2 = new Thread(() => ReadFileAndPrint("t2"));
		var t2 = new Thread(() => PrintAndWriteFile("txt to print","t2"));
		t1.Start();
		t2.Start();
		
		t1.Join();
		t2.Join();
		
		Console.WriteLine("Exec has completed.");
	}

	static void ReadFileAndPrint(string thID)
	{
		int sec = DateTime.Now.Millisecond;
		int m = (sec%2)*2;
		Thread.Sleep(sec*m);
		if(0 == Interlocked.Exchange(ref _lockP, 1))
		{
			for(int i = 0; i < 3; i++)
			{
				lock(_lockRW)
				{
					Read(thID);
					lock((object)_lockP)
					{
						Print(thID);
					}
				}
			}
			Interlocked.Exchange(ref _lockP, 0);
		}
		else
		{
			Thread.Sleep(2000);
			ReadFileAndPrint(thID);
		}
	}
	
	static void PrintAndWriteFile(string txt, string thID)
	{
		int sec = DateTime.Now.Millisecond;
		int m = (sec%2)*2;
		Thread.Sleep(sec*m);
		if(0 == Interlocked.Exchange(ref _lockP, 1))
		{
			Console.WriteLine("Lock for th"+thID);
			
			//code

			for(int i = 0; i < 3; i++)
			{
				lock((object)_lockP)
				{
					Print(thID);
					lock(_lockRW)
					{
						Write(thID);	
					}
				}
			}


			Interlocked.Exchange(ref _lockP, 0);
		}
		else
		{
			Thread.Sleep(2000);
			PrintAndWriteFile(txt, thID);
		}
	}

	static void Print(string thID)
	{
		int c = _printCounter + 1;
		Thread.Sleep(200);
		_printCounter = c;
		Console.WriteLine("print "+thID+";count "+_printCounter);
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
