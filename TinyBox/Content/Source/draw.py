from TinyBox.Hooks import Draw

flip_none = 0
flip_x = 1
flip_y = 2
flip_both = 1 | 2


def pix(x, y, color=0xFFFFFF):
    Draw.Pix(x, y, color)


def rect(x, y, width, height, color=0xFFFFFF):
    Draw.Rect(x, y, width, height, color)


def tex(tex, x, y, u, v, w, h, color=0xFFFFFF, scale=1, flip=flip_none):
    Draw.Tex(tex, x, y, u, v, w, h, color, scale, flip)


def string(font, x, y, string, color=0xFFFFFF):
    Draw.String(font, x, y, string, color)
