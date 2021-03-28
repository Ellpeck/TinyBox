import game
import resource
import draw


class Game(game.TinyBoxGame):
    frame = 0

    def __init__(self):
        self.tex = resource.tex("tex.png")

    def draw(self, delta):
        scale = 4
        centerX = game.width / 2 - 8 * scale
        centerY = game.height / 2 - 8 * scale
        u = 16 * (int(self.frame) % 4)
        draw.tex(self.tex, centerX, centerY, u, 0, 16, 16, scale=scale, flip=draw.flip_x)
        self.frame += delta * 5
