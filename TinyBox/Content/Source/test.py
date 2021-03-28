from game import TinyBoxGame
from draw import pix, rec


class Game(TinyBoxGame):
    offset = 0

    def draw(self, delta):
        rec(10 + self.offset, 10, 100, 100, 0x00FF00)

    def update(self, delta):
        self.offset += delta * 100
