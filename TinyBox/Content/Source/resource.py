from TinyBox import Hooks

_resource = Hooks.Hook.Get("Resource")


def font(name, scale):
    return _resource.Font(name, scale)


def string_width(font, string):
    return _resource.StringWidth(font, string)


def string_height(font, string):
    return _resource.StringHeight(font, string)
