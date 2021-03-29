import game
import resource
import draw


class Game(game.TinyBoxGame):
    frame = 0

    def __init__(self):
        self.tex = resource.tex("tex.png")

    def draw(self, delta):
        scale = 4
        center_x = game.width / 2 - 8 * scale
        center_y = game.height / 2 - 8 * scale
        u = 16 * (int(self.frame) % 4)
        draw.tex(self.tex, center_x, center_y, u, 0, 16, 16, scale=scale, flip=draw.flip_x)
        self.frame += delta * 5
