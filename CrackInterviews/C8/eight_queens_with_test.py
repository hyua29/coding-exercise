import unittest
from ddt import unpack, ddt, data


def solution(grid_size):
    if type(grid_size) != int or grid_size < 0:
        raise ValueError("grid_size must be a non-negative integer")
    results = []
    for column in range(0, grid_size):
        buffer = []
        try_pick(0, column, buffer, results, grid_size)

    return results


def try_pick(row, column, buffer, results, grid_size):
    if not is_position_valid(buffer, row, column):
        return

    buffer.append((row, column))

    if len(buffer) == grid_size:
        results.append(buffer.copy())
    else:
        for next_column in range(0, grid_size):
            try_pick(row + 1, next_column, buffer, results, grid_size)

    buffer.pop()


def is_position_valid(buffer, new_row, new_column):
    for i in range(len(buffer)):
        (row, column) = buffer[i]

        if new_column == column:
            return False

        if new_column == column + abs(new_row - row) or new_column == column - abs(new_row - row):
            return False

    return True


@ddt
class EightQueenTest(unittest.TestCase):
    @data(1, 2, 3, 4, 5, 6, 7, 8)
    def test_solution(self, grid_size):
        results = solution(grid_size)
        for result in results:
            rows = [False for _ in range(grid_size)]
            columns = [False for _ in range(grid_size)]
            for i in range(len(result)):
                (row, column) = result[i]

                rows[row] = True
                rows[column] = True

                if i > 0:
                    for j in range(i - 1):
                        (previous_row, previous_column) = result[j]
                        self.assertEqual(False, previous_column ==
                                         column + abs(previous_row - row))
                        self.assertEqual(False, previous_column ==
                                         column - abs(previous_row - row))
                        self.assertEqual(False, previous_row ==
                                         row + abs(previous_column - column))
                        self.assertEqual(False, previous_row ==
                                         row - abs(previous_column - column))

            self.assertEqual(0, rows.count(lambda x: x != True))
            self.assertEqual(0, columns.count(lambda x: x != True))


if __name__ == "__main__":
    unittest.main()
