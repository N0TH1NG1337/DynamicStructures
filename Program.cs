namespace DynamicStructures;

class Program
{
    public static Queue<T> CopyQueue<T>(Queue<T> q) {
        Queue<T> newOne = new Queue<T>();
        Queue<T> tmp = new Queue<T>();

        while(!q.IsEmpty()) {
            T Current = q.Remove();

            newOne.Insert(Current);
            tmp.Insert(Current);
        }

        // Restore original queue
        while (!tmp.IsEmpty()) {
            q.Insert(tmp.Remove());
        }

        return newOne;
    }

    public static int SumQueue(Queue<int> queue) {
        Queue<int> tmp = CopyQueue<int>(queue);

        int Sum = 0;

        while (!tmp.IsEmpty()) {
            int Current = tmp.Remove();

            Sum += Current;
        }

        return Sum;
    }


    // function that counts and removes a number
    public static int CountAndRemove(Queue<int> queue, int Number) {
        Queue<int> tmp = CopyQueue<int>(queue);

        while (!queue.IsEmpty())
            queue.Remove();

        int Count = 0;

        while (!tmp.IsEmpty()) {
            int Current = tmp.Remove();

            if (Current == Number) 
                // by this we lose (remove) num from queue
                Count++;
            else
                queue.Insert(Current);
        }

        return Count;
    }

    // טענת כניסה : פעולה מקבלת תור של מספרים שלמים
    // אענת יציאה : הפעולה מחזירה תור מטיפוס (חדש) כך שבכל אוביקט יופיע מספר וכמה פעמים הופיע בתור
    // יש לשמור על תור מקורי
    public static Queue<Item> FrecQueue(Queue<int> queue) {
        Queue<Item> newOne = new Queue<Item>();
        Queue<int> tmp = CopyQueue<int>(queue);

        while(!tmp.IsEmpty()) {
            int Current = tmp.Remove();

            int Count = CountAndRemove(tmp, Current) + 1;

            Item newItem = new Item(Current, Count);
            newOne.Insert(newItem);
        }

        return newOne;
    }

    // Ex 13
    // טענת כניסה : 
    // טענת יציאה : 
    public static Queue<int> GetUnusedNumbers(Queue<int> queue) {
        // we will use copy already
        Queue<int> newOnes = new Queue<int>();

        while(!queue.IsEmpty()) {
            int Current = queue.Remove();

            if (!queue.IsEmpty()) {
                int Next = queue.Head();
                int Delta = Next - Current;

                if (Delta > 0) 
                    for (int i = 1; i < Delta; i++) 
                        newOnes.Insert(Current + i);
                    
                
            }
        }

        return newOnes;
    }

    // טענת כניסה : 
    // טענת יציאה : 
    public static Queue<Queue<int>> GetNumbersThatUnused(Queue<Queue<int>> queue) {
        Queue<Queue<int>> tmp = new Queue<Queue<int>>();

        Queue<Queue<int>> Fixed = new Queue<Queue<int>>();

        while (!queue.IsEmpty()) {
            Queue<int> CurrentQueue = queue.Remove();
            Queue<int> CQueue = CopyQueue<int>(CurrentQueue); // Will work with this

            Queue<int> news = GetUnusedNumbers(CQueue);
            Fixed.Insert(news);

            tmp.Insert(CurrentQueue);
        }

        while (!tmp.IsEmpty())
            queue.Insert(tmp.Remove());

        return Fixed;
    } 

    static void Main(string[] args)
    {
        // Create empty queue
        Queue<Queue<int>> GeneralQ = new Queue<Queue<int>>(); 

        Queue<int> Q1 = new Queue<int>();
        Queue<int> Q2 = new Queue<int>();
        Queue<int> Q3 = new Queue<int>();
        Queue<int> Q4 = new Queue<int>();

        Q1.Insert(1);
        Q1.Insert(5);
        Q1.Insert(8);
        Q1.Insert(9);
        Q1.Insert(10);

        Q2.Insert(7);
        Q2.Insert(9);
        Q2.Insert(10);

        GeneralQ.Insert(Q1);
        GeneralQ.Insert(Q2);

        Console.WriteLine(GeneralQ);
        Console.WriteLine(GetNumbersThatUnused(GeneralQ));


        //Console.WriteLine(Q1);
        //Console.WriteLine(CopyQueue<int>(Q1));
        //Console.WriteLine(SumQueue(Q1));
        //Console.WriteLine(FrecQueue(Q1));
        //Console.WriteLine(Q1);
        
    }
}
