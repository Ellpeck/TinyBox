from TinyBox import Hooks

_resource = Hooks.Hook.Get("Resource")


def font(name, size):
    return _resource.Font(name, size)


def string_width(font, string):
    return _resource.StringWidth(font, string)


def string_height(font, string):
    return _resource.StringHeight(font, string)
