using System; 
using System.Threading.Tasks;

namespace AsyncAwaitExample 
{ 
	class Program 
	{ 
	    static async Task Main(string[] args) 
	    {

		Console.WriteLine("sync/async/esc");

		while(true)
		{
			string cmd = Console.ReadLine();
			if(cmd == "esc")
				break;
			else if(cmd == "async")
			{
				//benfit with async/await
			      Task t1 = Method1();
			      Task t2 = Method2();
			      await Task.WhenAll(t1, t2);

			}
			else if(cmd == "sync")
			{
				//result the same like sync
                              await Method1();
			      await Method2();
			}
		}
            }

	    static async Task Method1()
	    {
		await Task.Delay(5000);//long term simiulation
		Console.WriteLine("Method1");
	    }

	    static async Task Method2()
	    {
		await Task.Delay(2000);//long term simulation
		Console.WriteLine("Method2");
	    }
	}

}
