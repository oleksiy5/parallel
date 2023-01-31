# Parallel.For

Parallel.For is a method in the `System.Threading.Tasks` namespace in C# that allows for the parallel execution of a for loop. It uses the .NET Framework's `Task Parallel Library` (_TPL_) to divide the loop's iterations across multiple threads, potentially improving performance on multi-core systems. The method takes a ParallelOptions object, which can be used to specify the degree of parallelism and a CancellationToken to cancel the loop.

`Task.Factory.StartNew` is a method in the `System.Threading.Tasks` namespace that allows for the creation and execution of a task. A task is a unit of work that can be executed concurrently with other tasks.

`Parallel.For` and `Task.Factory.StartNew` can both be used for parallel execution of code, but `Parallel.For` is typically used for parallelizing loops and Task.Factory.StartNew is used for creating and executing individual tasks.

> In the example, the method performs tasks returns the result to the main task, and the final result is calculated from the partial results.
> Make five modulo computations for 1, 2, 3, 4, 5 numeber. Make it parallel, in the end sum all computaion results, disply sum result on console.

# Task.Factory.StartNew

`Task.Factory.StartNew` is a method in the .NET Framework's Task class that is used to start a new task. **It is typically used when you want to execute a piece of code asynchronously, without blocking the current thread.** This can be useful in scenarios where you need to perform a time-consuming operation, such as:
>
>    /1/ downloading a file,
>    
>    /2/ processing large amounts of data, 
>    
>    /3/ cpu expensive calculations
>
without causing the user interface to become unresponsive. Additionally, using Task.Factory.StartNew can help to improve the performance of your application by allowing it to take full advantage of multiple cores or processors.

string url = "http://example.com/largefile.zip";

Task.Factory.StartNew(() =>
{
    using (WebClient client = new WebClient())
    {
        client.DownloadFile(url, "largefile.zip");
    }
});

# Thread

A `foreground thread` is a thread that has higher priority than a background thread and continues executing until it is completed. A `background thread` is a low-priority thread that runs in the background, typically used for tasks that don't need to block the UI, such as a file download. The system may end a background thread at any time to free up system resources.

# Thread Vs Task

Thread and Task are both used for asynchronous programming in .NET. The main difference is:
/1/ `Thread` is a `lower-level` concept and creates a separate execution context in the process.
/2/ `Task` is a higher-level concept that represents an asynchronous operation and is built on top of threads. It provides a more convenient and flexible way to handle asynchronous operations compared to threads.

In general, it is recommended to use Tasks for most asynchronous operations, as they provide a more modern and convenient way of handling asynchronous operations than Threads.

