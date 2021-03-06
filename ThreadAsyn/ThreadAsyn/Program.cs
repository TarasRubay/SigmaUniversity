using System.Collections.Concurrent;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
///The Goal of this task is to see "Congratulations, task completed!" in Console,
/// Do not modify any code, except the part with comments TODO
/// </summary>
public class Worker
{
    private static Object locker = new();
    public class ExecutedTasks
    {
        public readonly List<Runnable> successful = new List<Runnable>();
        public readonly ISet<Runnable> failed = new HashSet<Runnable>();
        public readonly ISet<Runnable> timedOut = new HashSet<Runnable>();
    }

    public class Runnable
    {
        private readonly Action action;

        public Runnable(Action action)
        {
            this.action = action;
        }

        public void Run()
        {
            action();
        }
    }

    public ExecutedTasks Execute(ICollection<Runnable> actions, TimeSpan timeout)
    {
        ExecutedTasks result = new ExecutedTasks();
        List<Task> continueTasks = new List<Task>();
        var cts = new CancellationTokenSource();
        cts.CancelAfter(timeout);
        var cancellationToken = cts.Token;
        int counter = 1;
        foreach (var runnable in actions)
        {
            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
            Task task = taskCompletionSource.Task;
            cancellationToken.Register(() =>
            {
                taskCompletionSource.TrySetCanceled();
            });
            //TODO Add missing logic here.
            Task.Run(() =>
            {
             
                try
                {
                    runnable.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine("exception");
                    taskCompletionSource.SetException(e);
                    return;
                }
                    taskCompletionSource.TrySetResult(null);
                    Console.WriteLine($"task {counter++}; status {task.Status}");

                
                
            }
            , cancellationToken
             );

            var continueTask = task.ContinueWith(t => ContinuationFunction(t, runnable, result));
            continueTasks.Add(continueTask);
        }

        Task.WaitAll(continueTasks.ToArray());
        return result;
    }

    private void ContinuationFunction(Task task, Runnable action, ExecutedTasks result)
    {
        lock (locker)
        {
            if (task.IsFaulted) result.failed.Add(action);
        else if (task.IsCompletedSuccessfully) result.successful.Add(action);
        else if (task.IsCanceled) result.timedOut.Add(action);
        }
    }
}

public class Validator
{
    public static void Main()
    {
        Worker worker = new Worker();
        var timeoutSec = 4;
        var result = worker.Execute(new List<Worker.Runnable>()
                {
                    new Worker.Runnable(
                        () => { Thread.Sleep(1000); }),
                    new Worker.Runnable(() => { Thread.Sleep(20000); }),
                    new Worker.Runnable(() => { Thread.Sleep(20000); }),
                    new Worker.Runnable(() => { throw new Exception(); })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                    , new Worker.Runnable(() => { })
                }
            , TimeSpan.FromSeconds(timeoutSec));

        var success = result.successful.Count == 9 && result.failed.Count == 1 && result.timedOut.Count == 2;
        if (success)
        {
            Console.WriteLine("Congratulations, task completed!");
            Console.WriteLine($"successful {result.successful.Count};failed {result.failed.Count};timedOut {result.timedOut.Count}");
        }
        else
        {
            //throw new Exception("Task Failed!");
            Console.WriteLine("Task Failed!");
            Console.WriteLine($"successful {result.successful.Count};failed {result.failed.Count};timedOut {result.timedOut.Count}");
        }

        Console.ReadLine();
    }
}