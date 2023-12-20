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


    // Node functions 

    // Function to sum ints 
    public static int SumList(Node<int> n) {
        if (!n.HasNext())
            return n.GetValue();

        return n.GetValue() + SumList(n.GetNext());
    }

    // Function to count units
    public static int CountList<T>(Node<T> obj) {
        if (obj == null)
            return 0;

        if (!obj.HasNext())
            return 1;

        return 1 + CountList<T>(obj.GetNext());
    }

    // Function to print List
    public static void PrintList<T>(Node<T> obj) {
        Node<T> tmp = obj;

        while (tmp != null) {
            Console.Write(tmp.GetValue() + ", ");
            tmp = tmp.GetNext();
        }

        Console.WriteLine();
    }

    // טענת כניסה : אין 
    // טענת יציאה : הפעולה מחזירה שרשרת חוליות של מספרים שלמים
    // והוספה של כל חולייה מתבצעת בסוף השרשרת
    public static Node<int> CreateIntList() {
        Node<int> Orig = null;
        Node<int> Tmp = Orig;

        Console.Write("Enter number : ");
        string Data = Console.ReadLine();

        while (Data != "EXIT") {
            int Number = int.Parse(Data);

            Node<int> Current = new Node<int>(Number);

            // Since Orig and Tmp are parallel
            // therefore need to check only for Orig
            if (Orig == null) {
                Orig = Current;
                Tmp = Orig;
            }
            else {
                Tmp.SetNext(Current);
                Tmp = Tmp.GetNext();
            }

            Console.Write("Enter number : ");
            Data = Console.ReadLine();

        }

        return Orig;
    }

    // טענת כניסה : הפעולה מקבלת רשימה ומספר שלם
    // טענת יציאה : הפעולה מחזירה את מספר הרצפים של המספר ברשימה
    public static int CountNumberFlow(Node<int> obj, int Number) {
        int CountSingle = 0;
        int CountOverAll = 0;
        Node<int> LastPos = obj; // Use to go from start to end

        while (LastPos != null) {
            // Get Current
            int Current = LastPos.GetValue();

            // Check if the current number is the number
            // that we are looking for
            if (Current == Number) {
                CountSingle++;
            }

            // If the current number is not what we look for
            // there is 2 options
            // 1. The flow is done [CountSingle > 0]
            // 2. The flow is empty
            // if the flow is not empty we count that flow
            if (Current != Number && CountSingle > 0) {
                CountOverAll++;
                CountSingle = 0;
            }

            // Go to the next Node
            LastPos = LastPos.GetNext();
        }
        
        // Return our result
        return CountOverAll;
    }

    // טענת כניסה : המפעולה מקבלת רשימה ושני מצייני מיקום
    // טענת יציאה : הפעולה מדפיסה את כל האיברים בין המקומות האלו
    public static void PrintList<T>(Node<T> obj, int StartIndex, int LastIndex) {
        Node<T> LastPos = obj;
        int Index = 0; // Start with index 0

        // [Just for case]
        // Check if the indexed that we got, relevent
        int Count = CountList<T>(obj);
        if (Count < LastIndex)
            return;

        while (LastPos != null) {
            // Get Current
            T Current = LastPos.GetValue();

            // Check if the current index is between the indexes
            if (Index >= StartIndex && Index <= LastIndex)
                Console.Write(String.Format("{0}, ", Current));

            // Update the index
            Index++;

            // Update the to the next
            LastPos = LastPos.GetNext();
        }

        Console.WriteLine(); // BreakLine down
    } 

    // טענת כניסה : הפעולה מקבלת רשימה
    // טענת יציאה : הפעולה מחזירה אותיות אם יש יותר מספר זוגיים או אי זוגיים או שווה
    public static char CountTypes(Node<int> obj) {
        Node<int> LastPos = obj;

        // Create 2 count vars
        int CountZ = 0;
        int CountE = 0;

        while (LastPos != null) {
            // Get Current
            int Current = LastPos.GetValue();

            // Simple checks
            if (Current % 2 == 0)
                CountZ++;

            if (Current % 2 != 0)
                CountE++;

            // Update for next
            LastPos = LastPos.GetNext();
        }

        // יש יותר זוגיים
        if (CountZ > CountE)
            return 'z';

        // יש יותר אי זוגיים
        if (CountZ < CountE)
            return 'e';
        
        // אם הכמות שווה
        return 's';
    }

    public static Node<int> EvenList(Node<int> L) {
        Node<int> PosL = L; // Work with

        Node<int> NewL = null;
        Node<int> LastNewL = NewL;

        while (PosL != null) {
            int Current = PosL.GetValue();

            if (Current % 2 == 0) {
                // need to add
                Node<int> ToAdd = new Node<int>(Current);

                if (NewL == null) {
                    NewL = ToAdd; // set the first one
                    LastNewL = NewL;
                }
                else {
                    LastNewL.SetNext(ToAdd);
                    LastNewL = LastNewL.GetNext();
                }
            }

            PosL = PosL.GetNext();
        }

        return NewL; // return the new one 
    }

    public static Node<int> NewCopyExist(Node<int> L1, Node<int> L2) {
        Node<int> LastL1 = L1;

        Node<int> NewL = null;
        Node<int> LastNewL = NewL;

        while (LastL1 != null) { 
            int Current = LastL1.GetValue();

            if (IsExist(L2, Current)) {
                Node<int> ToAdd = new Node<int>(Current);

                if (NewL == null) {
                    NewL = ToAdd;
                    LastNewL = NewL;
                }
                else {
                    if (!IsExist(NewL, Current)) {
                        LastNewL.SetNext(ToAdd);
                        LastNewL = LastNewL.GetNext();
                    }
                }
            }

            LastL1 = LastL1.GetNext();
        } 

        return NewL;
    }

    public static Node<int> NewCopyNotExist(Node<int> L1, Node<int> L2) {
        Node<int> LastL1 = L1;
        Node<int> LastL2 = L2;

        Node<int> NewL = null;
        Node<int> LastNewL = NewL;

        while (LastL1 != null) { 
            int Current = LastL1.GetValue();

            if (!IsExist(L2, Current)) {
                Node<int> ToAdd = new Node<int>(Current);

                if (NewL == null) {
                    NewL = ToAdd;
                    LastNewL = NewL;
                }
                else {
                    if (!IsExist(NewL, Current)) {
                        LastNewL.SetNext(ToAdd);
                        LastNewL = LastNewL.GetNext();
                    }
                }
            }

            LastL1 = LastL1.GetNext();
        }

        while (LastL2 != null) { 
            int Current = LastL2.GetValue();

            if (!IsExist(L1, Current)) {
                Node<int> ToAdd = new Node<int>(Current);

                if (NewL == null) {
                    NewL = ToAdd;
                    LastNewL = NewL;
                }
                else {
                    if (!IsExist(NewL, Current)) {
                        LastNewL.SetNext(ToAdd);
                        LastNewL = LastNewL.GetNext();
                    }
                }
            }

            LastL2 = LastL2.GetNext();
        } 

        return NewL;
    }

    public static Node<int> ToFirst(int Amount) {
        Node<int> New = null;

        for (int Index = 0; Index < Amount; Index++) {
            Console.Write("Enter number : ");
            int Number = int.Parse(Console.ReadLine());

            New = new Node<int>(Number, New);
        }

        return New;
    }

    public static bool IsListSorted(Node<int> L) {
        Node<int> PosL = L;

        while (PosL.GetNext() != null) {
            if (PosL.GetValue() > PosL.GetNext().GetValue())
                return true;

            PosL = PosL.GetNext();
        }

        return true;
    }

    // 3 -> 4 -> 5 -> 12 -> 19 -> 20 -> 100 -> 101 -> 102 -> 103 -> 104
    public static Node<Range> CreateRangeList(Node<int> L) {

        int First = L.GetValue();
        Node<int> LastL = L;

        Node<Range> NewRange = null;
        Node<Range> LastRange = NewRange;
        int From = L.GetValue();
        Range r;

        while (LastL.GetNext() != null) {
           // int Current = LastL.GetValue();
          //  int Next = LastL.GetNext().GetValue();

          //  Console.WriteLine("c : {0}, n : {1}", Current, Next);
            if (LastL.GetValue() +1 != LastL.GetNext().GetValue()) {
                r = new Range(From, LastL.GetValue());

                if (NewRange == null) {
                    NewRange = new Node<Range>(r);
                    LastRange = NewRange;
                }
                else {
                    LastRange.SetNext(new Node<Range>(r));
                    LastRange = LastRange.GetNext();
                }

                From = LastL.GetNext().GetValue();
            }
            
            LastL = LastL.GetNext();              
        }

        r = new Range(From, LastL.GetValue());

        if (NewRange == null) {
            NewRange = new Node<Range>(r);
            LastRange = NewRange;
        }

        return NewRange;
    } 

    // 
    public static Node<int> Create1_N (int n) {
        Node<int> L = null;
        Node<int> LastL = L;

        for (int i = 1; i <= n; i++) {

            if (L == null) {
                L = new Node<int>(i);
                LastL = L;
            } else {
                LastL.SetNext(new Node<int>(i));
                LastL = LastL.GetNext();
            }

        }

        return L;
    }

    public static Node<int> BuildRandom(int n, int low, int high) {
        Random r = new Random();

        Node<int> L = new Node<int>(r.Next(low, high + 1));
        Node<int> LastL = L;

        for (int i = 1; i < n; i++) {
            LastL.SetNext(new Node<int>(r.Next(low, high + 1)));
            LastL = LastL.GetNext();
        }

        return L;
    }

    public static double AvarageList(Node<int> L) {
        Node<int> PosL = L;

        double Sum = 0;
        int Count = 0;

        while (PosL != null) {
            int Current = PosL.GetValue();

            Sum += Current;
            Count++;

            PosL = PosL.GetNext();
        }

        double Avarage = Sum / Count;

        return Avarage;
    }

    public static int MaxInList(Node<int> L) {
        Node<int> PosL = L;

        int Max = PosL.GetValue();
        PosL = PosL.GetNext();

        while (PosL != null) {
            int Current = PosL.GetValue();

            if (Current > Max) {
                Max = Current;
            }

            PosL = PosL.GetNext();
        }

        return Max;
    }

    public static int MinInList(Node<int> L) {
        Node<int> PosL = L;

        int Min = PosL.GetValue();
        PosL = PosL.GetNext();

        while (PosL != null) {
            int Current = PosL.GetValue();

            if (Current < Min) {
                Min = Current;
            }

            PosL = PosL.GetNext();
        }

        return Min;
    }

    public static int SumOddPlaces(Node<int> L) {
        Node<int> PosL = L;

        int Sum = 0;

        while (PosL != null) {
            int Current = PosL.GetValue();

            Sum += Current;

            if (PosL.GetNext() != null)
                PosL = PosL.GetNext().GetNext();
            else
                PosL = null;
        }

        return Sum;
    }

    public static int CountNumberInList(Node<int> L, int x) {
        Node<int> LastL = L;

        int Count = 0;

        while (LastL != null) {
            int Current = LastL.GetValue();

            if (x == Current)
                Count++;

            LastL = LastL.GetNext();
        }

        return Count;
    }

    public static Node<int> Insert(Node<int> L, int step, int x) {
        if (step == 0) {
            // Insert as first
            return new Node<int>(x, L);
        }

        // Create a var that we will run with
        Node<int> LastL = L;

        // Create a var to count how much we passed
        int Count = 0;

        // Start to loop and check 1 before the end

        // FIXME: we can stop already when Count+ 1 == step since we must already passed it
        // and prob changed
        while (LastL.GetNext() != null) {
            // Add another one, since we checked
            Count++;

            // Check how much we passed based on where we need to add
            if (Count == step) {
                // We set the Last Node we passed a new Next address
                // new Node<int>(x, ...);
                // the LastL.GetNext() will connect our next Node to the new one
                // Therefore we dont lose anything
                LastL.SetNext(new Node<int>(x, LastL.GetNext()));
            }

            // Move the next one.
            LastL = LastL.GetNext();
        }

        // However if we still didnt add and we passed everything
        // We add to the end
        if (step > Count) {
            LastL.SetNext(new Node<int>(x));
        }

        // return the "new" List
        return L;
    }

    public static bool IsExist(Node<int> L, int Number) {
        Node<int> LastL = L;

        while (LastL != null) {
            int Current = LastL.GetValue();

            if (Current == Number)
                return true;

            LastL = LastL.GetNext();
        }

        return false;
    }

    public static bool IsAllExist(Node<int> List1, Node<int> List2) {
        Node<int> LastList2 = List2;

        while (LastList2 != null) {
            int Current = LastList2.GetValue();

            if (!IsExist(List1, Current))
                return false;

            LastList2 = LastList2.GetNext();
        }

        return true;
    }


    static void Main(string[] args)
    {
        // Node lesson 7.12 uwu 
        // <3
        
        // הוספה חוליה לשרשרת חוליות לסוף
        // סדר ההכנסה נשאר לפי הקלט
        //Node<int> t = CreateIntList();//ToFirst(5);
        //PrintList<int>(t); // בדיקת תקינות

        Node<int> t = CreateIntList();
        Node<int> t2 = CreateIntList();
        PrintList<int>(t);
        PrintList<int>(t2);
        Console.WriteLine(IsAllExist(t, t2));

        //PrintList<int>(t, 1, 3);
        //PrintList<int>(NewCopyNotExist(t, t2));
    }
}
