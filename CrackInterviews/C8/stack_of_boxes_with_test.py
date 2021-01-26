import unittest
from ddt import ddt, data, unpack


class Box:
    def __init__(self, width, length, height):
        self.width = width
        self.length = length
        self.height = height

    def current_can_be_above(self, box):
        if box is not None and not type(box) == Box:
            raise ValueError("input must be a box")

        return self.height <= box.height and self.width <= box.width and self.length <= box.length


def solution(boxes):
    if type(boxes) != list or (len(boxes) > 0 and not type(boxes[0]) == Box):
        raise ValueError("input type is invalid")

    stack_map = [0 for _ in range(len(boxes))]
    boxes.sort(key=lambda x: x.height, reverse=True)
    max_height = 0
    for i in range(len(boxes)):
        height = create_stack(boxes, i, stack_map)
        max_height = max(height, max_height)

    return max_height


def create_stack(boxes, bottomIndex, stack_map):
    if stack_map[0] > 0:
        return stack_map[bottomIndex]

    current_box = boxes[bottomIndex]
    max_height = 0
    for i in range(bottomIndex + 1, len(boxes)):
        if boxes[i].current_can_be_above(current_box):
            height = create_stack(boxes, i, stack_map)
            max_height = max(max_height, height)

    max_height += current_box.height
    stack_map[bottomIndex] = max_height
    return max_height


@ddt
class StackOfBoxesTests(unittest.TestCase):
    @data(([Box(2,2,2), Box(1,1,1), Box(3,3,3)], 6),
    ([Box(1,2,4), Box(1,3,2), Box(9,38,31)], 35))
    @unpack
    def test_solution(self, boxes, expected_height):
        result = solution(boxes)
        self.assertEqual(expected_height, result)


if __name__ == "__main__":
    unittest.main()
