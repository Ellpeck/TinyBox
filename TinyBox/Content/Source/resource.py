from TinyBox.Hooks import Resource


def font(name, scale):
    return Resource.Font(name, scale)


def tex(name):
    return Resource.Tex(name)


def string_width(fnt, string):
    return Resource.StringWidth(fnt, string)


def string_height(fnt, string):
    return Resource.StringHeight(fnt, string)
