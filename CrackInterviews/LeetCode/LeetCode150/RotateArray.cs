namespace LeetCode.LeetCode150;

public class RotateArray
{
    public void Rotate(int[] nums, int k)
    {
        var buffer = new Queue<int>(nums.Take(k));
        for (int i = 0; i < nums.Length; i++)
        {
            var targetIndex = (i + k) % nums.Length;

            buffer.Enqueue(nums[targetIndex]);
            nums[targetIndex] = buffer.Dequeue();
        }
    }
}