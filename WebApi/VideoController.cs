using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace WebApi;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    private static string GetVideoName(int videoId)
    {
        return videoId switch
        {
            1 => "ocean.mp4",
            2 => "forest.mp4",
            3 => "rocks.mp4",
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    [HttpGet("{id:int}")]
    public FileStreamResult GetVideo([FromRoute]int id)
    {
        var videoName = GetVideoName(id);

        var videoPath = @"C:\Users\ondrej.exner\Downloads\{0}";
        var videoStream = new FileStream(string.Format(videoPath, videoName), FileMode.Open);

        var response = new FileStreamResult(videoStream, new MediaTypeHeaderValue("video/mp4"))
        {
            FileDownloadName = videoName,
        };

        return response;
    }
}
