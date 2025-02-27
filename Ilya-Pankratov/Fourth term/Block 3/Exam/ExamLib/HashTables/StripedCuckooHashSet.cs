namespace ExamLib.HashTables;

public class StripedCuckooHashSet<T> : APhasedCuckooHashSet<T>, IHashTable<T>
{
    Mutex[,] locks;

    public StripedCuckooHashSet(int capacity) : base(capacity)
    {
        locks = new Mutex[2, capacity];
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < capacity; j++)
            {
                locks[i, j] = new Mutex();
            }
        }
    }

    public StripedCuckooHashSet(int capacity, IEqualityComparer<T> comparer) : this(capacity)
    {
        this.comparer = comparer;
    }

    protected override void Acquire(T x)
    {
        locks[0, Hash0(x) % locks.GetLength(1)].WaitOne();
        locks[1, Hash1(x) % locks.GetLength(1)].WaitOne();
    }

    protected override void Release(T x)
    {
        locks[0, Hash0(x) % locks.GetLength(1)].ReleaseMutex();
        locks[1, Hash1(x) % locks.GetLength(1)].ReleaseMutex();
    }

    protected override void Resize()
    {
        var oldCapacity = capacity;
        for (var i = 0; i < locks.GetLength(1); i++)
        {
            locks[0, i].WaitOne();
        }

        try
        {
            if (capacity != oldCapacity)
            {
                return;
            }

            var oldTable = table;
            capacity = 2 * capacity;
            table = new List<T>[2, capacity];

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < capacity; j++)
                {
                    table[i, j] = new List<T>(listSize);
                }
            }

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < oldCapacity; j++)
                {
                    foreach (var z in oldTable[i, j])
                    {
                        Add(z);
                    }
                }
            }
        }
        finally
        {
            for (var i = 0; i < locks.GetLength(1); i++)
            {
                locks[0, i].ReleaseMutex();
            }
        }
    }

    public static IHashTable<T> GetInstance(int capacity)
    {
        return new StripedCuckooHashSet<T>(capacity);
    }

    public static IHashTable<T> GetInstance(int capacity, IEqualityComparer<T> comparer)
    {
        return new StripedCuckooHashSet<T>(capacity, comparer);
    }
}