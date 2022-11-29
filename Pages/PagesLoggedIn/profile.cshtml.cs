using Gaines_Opus_Institute_Current.FileUploadService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gaines_Opus_Institute_Current.Pages.PagesLoggedIn
{
    public class profileModel : PageModel
    {
        private readonly ILogger<profileModel> _logger;
        private readonly IFileUploadService fileUploadService;
        public string FilePath;
        public profileModel(ILogger<profileModel> logger, IFileUploadService fileUploadService)
        {
            _logger = logger;
            this.fileUploadService = fileUploadService;
        }

        public void OnGet()
        {
          
        }
        public async void OnPost(IFormFile file)
        {
            if (file != null)
            {
               FilePath = await fileUploadService.UploadFileAsync(file);
            }
            
        }
    }
}
