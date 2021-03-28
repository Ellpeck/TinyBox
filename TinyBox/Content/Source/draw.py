from TinyBox import Hooks

_draw = Hooks.Hook.Get("Draw")


def pix(x, y, color):
    _draw.Pix(x, y, color)


def rec(x, y, width, height, color):
    _draw.Rec(x, y, width, height, color)
