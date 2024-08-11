import __future__ as futures
import cv2
import dlib
import numpy as np
import face_pb2
import face_pb2_grpc

class FaceDetectionServicer(face_pb2_grpc.FaceDetectionServicer):
   def Detect(self, request, context):
      face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml');

      face_image = cv2.imdecode(np.frombuffer(request.image, np.uint8), cv2.IMREAD_COLOR)
      face_image_gray = cv2.cvtColor(face_image,cv2.COLOR_BGR2GRAY);

      faces = face_cascade.detectMultiScale(face_image_gray,scaleFactor = 1.1,minNeighbors = 1)
      
      return face_pb2.FaceDetectionReply(id = request.id,detected = (len(faces) != 0))
