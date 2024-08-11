
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Presentation.Services;
using System.IO;
using System.Reflection;

namespace Presentation.Extensions
{
  public static class DownloadRoute
  {
    public static void MapDownloadImage(this IEndpointRouteBuilder endpointRouteBuilder)
    {
      endpointRouteBuilder.MapGet("/download", async ctx =>
      {
        if (!ctx.Request.Query.TryGetValue("guid", out StringValues guid))
          return;
        byte[]? image = await ctx.RequestServices
          .GetRequiredService<UserService>()
          .GetUserImageBufferByGuid(Guid.Parse(guid.ToString()));
        if(image == null)
          return;

        string path = Path.Combine(
          ctx.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath,
          "images",
          string.Concat(guid.ToString(),".png")
        );
        File.WriteAllBytes(path,image);
        FileInfo fileInfo = new FileInfo(path);
        ctx.Response.Headers.Append("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        ctx.Response.Headers.Append("Content-Length", fileInfo.Length.ToString());
        ctx.Response.Headers.Append("Content-Transfer-Encoding", "binary");
        new FileExtensionContentTypeProvider().Mappings.TryGetValue(fileInfo.Extension, out string? contentType);
        ctx.Response.ContentType = contentType ?? "application/octet-stream";
        ctx.Response.SendFileAsync(path).GetAwaiter().GetResult();
        File.Delete(path);
      });
    }
  }
}
