using Grpc.Net.Client;
using Presentation.Models;
using Presentation.Protos;
using System.Text;
using System.Threading.Channels;

namespace Presentation.Services;

public class FaceService
{
  private static readonly GrpcChannel _grpcChannel = 
    GrpcChannel.ForAddress("http://pyface:5002", new GrpcChannelOptions
    {
      MaxSendMessageSize = 1024 * 1024 * 1024,
      MaxReceiveMessageSize = 1024 * 1024 * 1024
    });

  private readonly IWebHostEnvironment _webHostEnvironment;

  public FaceService(IWebHostEnvironment webHostEnvironment)
  {
    _webHostEnvironment = webHostEnvironment;
  }

  public UserImage DetectImageIsHuman(Guid userGuid,Stream image)
  {
    FaceDetection.FaceDetectionClient client = new FaceDetection.FaceDetectionClient(_grpcChannel);

    FaceDetectionReply reply = client.Detect(new FaceDetectionRequest
    {
      Id = userGuid.ToString(),
      Image = Google.Protobuf.ByteString.FromStream(image)
    });

    byte[] imageArr = ReadToEnd(image);

    image.Close();

    return new UserImage
    {
      Guid = userGuid,
      Detected = reply.Detected,
      Image = imageArr
    };
  }

  public static byte[] ReadToEnd(Stream stream)
  {
    long originalPosition = 0;

    if (stream.CanSeek)
    {
      originalPosition = stream.Position;
      stream.Position = 0;
    }

    try
    {
      byte[] readBuffer = new byte[4096];

      int totalBytesRead = 0;
      int bytesRead;

      while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
      {
        totalBytesRead += bytesRead;

        if (totalBytesRead == readBuffer.Length)
        {
          int nextByte = stream.ReadByte();
          if (nextByte != -1)
          {
            byte[] temp = new byte[readBuffer.Length * 2];
            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
            readBuffer = temp;
            totalBytesRead++;
          }
        }
      }

      byte[] buffer = readBuffer;
      if (readBuffer.Length != totalBytesRead)
      {
        buffer = new byte[totalBytesRead];
        Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
      }
      return buffer;
    }
    finally
    {
      if (stream.CanSeek)
      {
        stream.Position = originalPosition;
      }
    }
  }
}
