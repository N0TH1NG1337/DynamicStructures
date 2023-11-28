namespace DynamicStructures;


class Queue<T>
{
    private Node<T> First;//הצבעה לתחילת התור

    private Node<T> Last;//הצבעה לחוליה אחרונה בתור
    //-----------------------------------
    public Queue()
    {
        this.First = null;

        this.Last = null;
    }
    //-----------------------------------
    public bool IsEmpty()
    {
        return this.First == null;
    }
    //-----------------------------------
    public void Insert(T x)
    {
        Node<T> Temp = new Node<T>(x);

        if (this.First == null)

            this.First = Temp;
        else

            this.Last.SetNext(Temp);

        this.Last = Temp;
    }
    //-------------------------------------
    public T Remove()
    {
        T x = this. First.GetValue();
               
        First = First.GetNext();

        if (this.First == null)

            this.Last = null;

        return x;
    }
    //-------------------------------------
    public T Head()
    {
        return (this.First.GetValue());
    }

    //-------------------------------------
    //פעולה גנרית משכפלת תור בתוך המחלקה
    //public Queue<T> CopyQueue()
    //{
    //    Queue<T> CopyQ = new Queue<T>();
    //    Node<T> Pos = this.First;
    //    while (Pos != null)
    //    {
               
    //        CopyQ.Insert(Pos.GetValue());
    //        Pos = Pos.GetNext();
    //    }
    //    return CopyQ;
    //}
    
    //-------------------------------------
    public override string ToString()
    {
        string st = "[";

        Node<T> pos = this.First;

        while (pos != null)
        {
            st += pos.GetValue();

            if (pos.GetNext() != null)
                st += ",";

            pos = pos.GetNext();
        }
        st += "]";
        return (st);
    }
}
