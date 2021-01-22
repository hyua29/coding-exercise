import unittest
from ddt import ddt, data, unpack

possible_steps = [1, 2, 3]


def solution(steps):
    if steps is None:
        return 0
    if steps == 0:
        return 1

    buffer = [0 for _ in range(steps + 1)]
    buffer[0] = 1

    for i in range(1, steps + 1):
        current_possible_steps = 0
        for s in possible_steps:
            if i - s >= 0:
                current_possible_steps = current_possible_steps + buffer[i - s]
        buffer[i] = current_possible_steps

    return buffer[steps]


@ddt
class TripleStepTests(unittest.TestCase):

    @data((None, 0), (0, 1), (1, 1), (2, 2), (3, 4))
    @unpack
    def test_solution(self, steps, expected_result):
        result = solution(steps)
        self.assertEqual(result, expected_result)


if __name__ == "__main__":
    unittest.main()
