using System.Collections.Generic;

namespace Two_C_Two_P.Web.DTOs.Upload
{
    public class UploadResultDto
    {
        public List<string> Errors { get; set; } = new List<string>();

        public List<string> Info { get; set; } = new List<string>();

        public List<string> Warnings { get; set; } = new List<string>();
    }
}
