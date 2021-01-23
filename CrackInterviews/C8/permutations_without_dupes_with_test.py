import unittest
from ddt import data, unpack, ddt


def solution(inputs):
    if type(inputs) != list:
        raise ValueError("inputs must be a list")

    results = []
    buffer = []

    solution_aux(inputs, buffer, results)

    return results


def solution_aux(inputs, buffer, results):
    if len(buffer) == len(inputs):
        results.append(buffer.copy())
        return

    for input in inputs:
        if input not in buffer:
            buffer.append(input)
            solution_aux(inputs, buffer, results)
            buffer.remove(input)

@ddt
class PermutationTests(unittest.TestCase):
    @data(
        (
            ["1", "2", "3"],
            [["1", "2", "3"], ["1", "3", "2"], ["2", "1", "3"], ["2", "3", "1"], ["3", "1", "2"], ["3", "2", "1"]]
        )
    )
    @unpack
    def test_solution(self, inputs, expected_results):
        results = solution(inputs)
        self.assertEqual(len(expected_results), len(results))
        for i, result in enumerate(results):
            print(result)
            for j, r in enumerate(result):
                self.assertEquals(expected_results[i][j], results[i][j])


if __name__ == "__main__":
    unittest.main()

