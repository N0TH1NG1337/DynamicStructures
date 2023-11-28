namespace DynamicStructures;

class Item {
    private int Num; // Number
    private int Count; // Amount of Number

    public Item(int NewNum, int NewCount) {
        this.Num = NewNum;
        this.Count = NewCount;
    }

    public override string ToString() {
        return String.Format("[Num : {0}, Count : {1}]", this.Num, this.Count);
    }
}