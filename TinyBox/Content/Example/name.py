from game import TinyBoxGame
import resource
import draw
import math


class Game(TinyBoxGame):
    font = 0
    time = 0
    colors = [0xFF0000, 0xFFA500, 0xFFFF00, 0x008000, 0x0000FF, 0x4B0082, 0xEE82EE]

    def __init__(self):
        self.font = resource.font("builtin/OrangeKid", 3)

    def draw(self, delta):
        offset = 0
        for i, c in enumerate("TinyBox"):
            sin = math.sin((self.time + i) * 5) * 10
            draw.string(self.font, 24 + offset, 50 + sin, c, self.colors[i])
            offset += resource.string_width(self.font, c)
        self.time += delta
