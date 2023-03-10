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

# Thread

A `foreground thread` is a thread that has higher priority than a background thread and continues executing until it is completed. A `background thread` is a low-priority thread that runs in the background, typically used for tasks that don't need to block the UI, such as a file download. The system may end a background thread at any time to free up system resources.

# Thread Vs Task

Thread and Task are both used for asynchronous programming in .NET. The main difference is:
>
> /1/ `Thread` is a `lower-level` concept and creates a separate execution context in the process.
>
> /2/ `Task` is a higher-level concept that represents an asynchronous operation and is built on top of threads. It provides a more convenient and 
> flexible way to handle asynchronous operations compared to threads.
>
In general, it is recommended to use Tasks for most asynchronous operations, as they provide a more modern and convenient way of handling asynchronous operations than Threads.

# Process Vs Thread Vs Task

Process, Thread, and Task are related but distinct concepts in computer science:

/1/ **Process**: An instance of a program in execution. Each process runs in its own isolated environment and has its own memory space, system resources, and execution context.

/2/ **Thread**: A thread is a lightweight unit of execution within a process that can run simultaneously with other threads in the same process. It shares the process's resources, including memory and system resources.

/3/ **Task**: A Task is a higher-level construct for asynchronous programming in .NET. It represents a single operation that can be executed asynchronously. Tasks are built on top of threads and use them for execution, but they provide a more convenient and flexible way to handle asynchronous operations compared to threads.

In general, a process is the container for executing a program, a thread is a unit of execution within a process, and a task is a higher-level concept for asynchronous programming that can be executed by a thread.

# Abort, Suspend (not supported 6.0)

`Thread.Abort` and `Thread.Suspend` methods are not supported in modern versions of .NET because they have several disadvantages:

`Abort`: The Abort method abruptly terminates the thread, which can cause unhandled exceptions and other unpredictable behavior. This can lead to resource leaks, corrupted data, and other serious problems.

`Suspend`: The Suspend method suspends the execution of a thread, which can cause deadlocks if the suspended thread holds a lock that other threads are waiting for. This can lead to serious performance issues and make the program difficult to debug.

Therefore, these methods were deprecated and are no longer recommended for use. Instead, it is recommended to use a `shared variable` or a `cancellation token` to stop a thread gracefully.

# Shared variable Vs cancelation token (recomended)

In .NET Core, the recommended way to stop a thread is to use a **shared variable** or a **cancellation token** that the thread checks periodically. If the thread is checking the variable or token and sees that it's time to stop, it can gracefully exit.

Both approach is good and operate on the thread level, but `cancelation token` is more elegant.
 
# async/await

`Async/Await` is a pattern in C# that enables asynchronous, `non-blocking programming`. The **async** keyword is used to declare a method that contains asynchronous operations and the **await** keyword is used to wait for the result of an asynchronous operation. This pattern makes it easy to write asynchronous code that is both readable and maintainable, and it helps prevent issues with blocking the UI thread or running out of resources.

# async/await Vs Thread are two concepts for handling concurrency in .NET Core:

async/await is a language feature that enables you to write asynchronous code in a way that looks and behaves like synchronous code. It makes it easier to write and maintain asynchronous code, and it provides a higher-level abstractions for managing concurrency.

Thread is a lower-level way of handling concurrency, and it provides a way to run code in parallel on multiple threads. With Thread, you have more control over how your code runs in a multithreaded environment, but it can be more difficult to write and maintain compared to async/await.

In general, it's recommended to use async/await for most concurrency scenarios in .NET Core, as it provides a more readable and maintainable way to write asynchronous code. However, in some cases, such as when you need fine-grained control over threading, Thread may be a better choice.

# When to use Async/Await

There are basically two scenarios where Async/Await is the right solution.

/1/ **I/O-bound work**: Your code will be waiting for something, such as data from a database, reading a file, a call to a web service. In this case you should use Async/Await, but not use the Task Parallel Library.

/2/ **CPU-bound work**: Your code will be performing a complex computation. In this case, you should use Async/Await but spawn the work off on another thread using Task.Run. You may also consider using the Task Parallel Library.

# async/await vs sync

basic simulation to do with return result when we need summarise somthing.


using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program {
    static async Task Main(string[] args) {
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
    }
}

