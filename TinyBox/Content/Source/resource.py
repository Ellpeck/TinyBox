from TinyBox.Hooks import Resource


def font(name, scale):
    return Resource.Font(name, scale)


def tex(name):
    return Resource.Tex(name)


def string_width(font, string):
    return Resource.StringWidth(font, string)


def string_height(font, string):
    return Resource.StringHeight(font, string)
