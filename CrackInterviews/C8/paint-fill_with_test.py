import unittest
from ddt import ddt, unpack, data


# int is used to represent color
def solution(grid, new_color, start_row, start_column):
    if grid is None:
        return

    if not type(grid) is list or not type(grid[0]):
        raise ValueError("grid must be a two-dimensional list")

    if grid[start_row][start_column] == new_color:
        return

    grid[start_row][start_column] = new_color

    if start_row + 1 < len(grid):
        solution(grid, new_color, start_row + 1, start_column)
    if start_row - 1 >= 0:
        solution(grid, new_color, start_row - 1, start_column)
    if start_column + 1 < len(grid[0]):
        solution(grid, new_color, start_row, start_column + 1)
    if start_column - 1 >= 0:
        solution(grid, new_color, start_row, start_column - 1)


@ddt
class PaintFillTest(unittest.TestCase):
    @data(([[0 for _ in range(5)] for _ in range(3)], 1, 0, 0),
          ([[0 for _ in range(5)] for _ in range(3)], 1, 2, 0),
          ([[0 for _ in range(5)] for _ in range(3)], 1, 0, 4),
          ([[0 for _ in range(5)] for _ in range(3)], 1, 2, 4))
    @unpack
    def test_solution(self, grid, new_color, start_row, start_column):
        solution(grid, new_color, start_row, start_column)
        for row in grid:
            for value in row:
                self.assertEqual(new_color, value)


if __name__ == "__main__":
    unittest.main()
