using System.Diagnostics;

namespace LeetCode.Atlassian;

public class FindMinimumInRotatedSortedArray
{
    public int FindMin(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        if (nums.Length == 1)
        {
            return nums[0];
        }

        int start = 0, end = nums.Length - 1;
        int mid;

        while (start <= end)
        {
            mid = (start + end) / 2;
            if (mid > 0 && nums[mid] < nums[mid - 1])
            {
                return nums[mid];
            }

            if (nums[start] <= nums[mid] && nums[mid] > nums[end])
            {
                start = mid + 1;
            }
            else
            {
                end = mid - 1;
            }
        }

        return nums[start];
    }
}

public class FindMinimumInRotatedSortedArrayTests
{
    private FindMinimumInRotatedSortedArray _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new FindMinimumInRotatedSortedArray();
    }

    [Test]
    public void FindMin_ShouldReturnTheMinimumValue_WhenNumsIsSortedInAscendingOrder()
    {
        // Arrange
        int[] nums = {1, 2, 3, 4, 5};

        // Act
        var result = _sut.FindMin(nums);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void FindMin_ShouldReturnTheMinimumValue_WhenNumsIsRotatedSortedArray()
    {
        // Arrange
        int[] nums = {4, 5, 6, 7, 0, 1, 2};

        // Act
        var result = _sut.FindMin(nums);

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void FindMin_ShouldReturnTheMinimumValue_WhenNumsHasOnlyOneElement()
    {
        // Arrange
        int[] nums = {1};

        // Act
        var result = _sut.FindMin(nums);

        // Assert
        Assert.That(result, Is.EqualTo(1));
    }
}