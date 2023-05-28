namespace SearchAndSort;

public static class ArrayExtension
{
    public static void Swap(this int[] array, int i, int j)
    {
        var temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}