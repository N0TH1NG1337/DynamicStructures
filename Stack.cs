namespace DynamicStructures;

public class Stack<T>
{
    private Node<T> First;

    public Stack()
    {
        this.First = null;
    }

    public bool IsEmpty()
    {
        return this.First == null;

    }

    public void Push(T x)
    {
        this.First = new Node<T>(x, this.First);
    }

    public T Pop()
    {
        T x = this.First.GetValue();
        this.First = this.First.GetNext();

        return x;
    }

    public T Top()
    {
        return this.First.GetValue();
    }
    //שכפול מחסנית
    //public Stack<T> CopyStack()
    //{
    //    Stack<T> CopyS = new Stack<T>();
    //    Node<T> Temp = null;
    //    Node<T> Pos = this.First;
    //    while (Pos != null)
    //    {
    //        Temp = new Node<T>(Pos.GetValue(), Temp);
    //        Pos = Pos.GetNext();
    //    }
    //    while (Temp != null)
    //    {
    //        CopyS.Push(Temp.GetValue());
    //        Temp = Temp.GetNext();
    //    }
    //    return CopyS;


    //}

    public override string ToString()
    {
        string str = "[";

        Node<T> Pos = this.First;
        while (Pos != null)
        {


            str += Pos.GetValue() + ",";
            Pos = Pos.GetNext();
        }
        str += "]";

        return str;
    }

}