from TinyBox import Hooks

_draw = Hooks.Hook.Get("Draw")


def pix(x, y, color):
    _draw.Pix(x, y, color)


def rect(x, y, width, height, color):
    _draw.Rect(x, y, width, height, color)


def string(font, x, y, string, color):
    _draw.String(font, x, y, string, color)
