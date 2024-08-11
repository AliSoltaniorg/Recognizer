from concurrent.futures.thread import ThreadPoolExecutor
import grpc
import face_pb2
import face_pb2_grpc
import faceDetectionServicer

# for (x,y,w,h) in faces:
#   cv2.rectangle(face_image,(x,y),(x+w,y+h),(255,0,0),2)
  
# cv2.imshow('Detected',face_image)

# cv2.waitKey(0)
# cv2.destroyAllWindows()


def serve():
    maxMsgLength = 1024*1024*1024  
    server = grpc.server(ThreadPoolExecutor(max_workers=10),options=[
        ('grpc.max_message_length', maxMsgLength),
        ('grpc.max_send_message_length', maxMsgLength),
        ('grpc.max_receive_message_length', maxMsgLength)])
    face_pb2_grpc.add_FaceDetectionServicer_to_server(faceDetectionServicer.FaceDetectionServicer(),server)
    server.add_insecure_port("[::]:5002")
    server.start()
    server.wait_for_termination()
    


serve()