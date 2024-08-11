from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor

class FaceDetectionRequest(_message.Message):
    __slots__ = ("image", "id")
    IMAGE_FIELD_NUMBER: _ClassVar[int]
    ID_FIELD_NUMBER: _ClassVar[int]
    image: bytes
    id: str
    def __init__(self, image: _Optional[bytes] = ..., id: _Optional[str] = ...) -> None: ...

class FaceDetectionReply(_message.Message):
    __slots__ = ("id", "detected")
    ID_FIELD_NUMBER: _ClassVar[int]
    DETECTED_FIELD_NUMBER: _ClassVar[int]
    id: str
    detected: bool
    def __init__(self, id: _Optional[str] = ..., detected: bool = ...) -> None: ...
