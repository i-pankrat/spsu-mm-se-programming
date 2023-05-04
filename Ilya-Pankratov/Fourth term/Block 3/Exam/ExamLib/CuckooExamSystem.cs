namespace ExamLib;

public class CuckooExamSystem : IExamSystem
{
    private StripedCuckooHashSet<StudentPassedExam> hastTable;
    private const int defSize = 5;
    private volatile int count;
    public int Count => count;

    public CuckooExamSystem()
    {
        hastTable = new StripedCuckooHashSet<StudentPassedExam>(defSize, new StudentPassedEqualityComparator());
        count = 0;
    }

    public void Add(long studentId, long courseId)
    {
        var student = new StudentPassedExam(studentId, courseId);
        if (hastTable.Add(student))
        {
            Interlocked.Increment(ref count);
        }
    }

    public void Remove(long studentId, long courseId)
    {
        var student = new StudentPassedExam(studentId, courseId);
        if (hastTable.Remove(student))
        {
            Interlocked.Decrement(ref count);
        }
    }

    public bool Contains(long studentId, long courseId)
    {
        var student = new StudentPassedExam(studentId, courseId);
        return hastTable.Contains(student);
    }
}