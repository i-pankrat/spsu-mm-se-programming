module ThreadPoolTests

open System
open System.Threading
open NUnit.Framework
open ThreadPool

[<Test>]
[<Repeat(5)>]
let ``Task are processed sequentially when thread is single`` () =
    let storage = ResizeArray()
    let task n () = lock storage (fun () -> storage.Add n)
    let work () =
        use pool = new ThreadPool(1, Seq.init 5 task)
        ()

    work()

    for i = 0 to storage.Count - 1 do
        Assert.AreEqual(i, storage[i])

[<Test>]
let ``Argument exception is raised when thread count is negative`` () =
    Assert.Throws(
        typeof<ArgumentException>,
        fun () ->
            use _ = new ThreadPool(-1)
            ()
    ) |> ignore

[<Test>]
[<Repeat(5)>]
let ``Enqueue method should throw an exception after disposing `` () =
    let storage = ResizeArray()
    let task _ () = lock storage (fun () -> storage.Add 1; Thread.Sleep(100))
    let work () =
        let pool = new ThreadPool(3, Seq.init 10 task)
        (pool :> IDisposable).Dispose()
        task () |> pool.Enqueue

    Assert.Throws(typeof<InvalidOperationException>, work) |> ignore
