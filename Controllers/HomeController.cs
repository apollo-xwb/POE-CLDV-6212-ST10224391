using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POE_CLDV_6221_ST10224391.Models;
using POE_CLDV_6221_ST10224391.Services;
using System.Threading.Tasks;

namespace POE_CLDV_6221_ST10224391.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AzureTableStorageService _tableStorageService;
        private readonly AzureBlobStorageService _blobStorageService;
        private readonly AzureQueueStorageService _queueStorageService;
        private readonly AzureFileStorageService _fileStorageService;

        public HomeController(
            ILogger<HomeController> logger,
            AzureTableStorageService tableStorageService,
            AzureBlobStorageService blobStorageService,
            AzureQueueStorageService queueStorageService,
            AzureFileStorageService fileStorageService)
        {
            _logger = logger;
            _tableStorageService = tableStorageService;
            _blobStorageService = blobStorageService;
            _queueStorageService = queueStorageService;
            _fileStorageService = fileStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageUploadModel model)
        {
            if (model.ImageFile != null)
            {
                await _blobStorageService.UploadImageAsync(model.ImageFile);
                await _queueStorageService.AddMessageAsync($"Uploaded image '{model.ImageFile.FileName}'");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadModel model)
        {
            if (model.File != null)
            {
                await _fileStorageService.UploadFileAsync(model.File);
                await _queueStorageService.AddMessageAsync($"Uploaded file '{model.File.FileName}'");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ListCustomerProfiles()
        {
            var profiles = await _tableStorageService.GetCustomerProfilesAsync();
            return View(profiles);
        }

        public async Task<IActionResult> ListImages()
        {
            var images = await _blobStorageService.ListImagesAsync();
            return View(images);
        }

        public async Task<IActionResult> ListFiles()
        {
            var files = await _fileStorageService.ListFilesAsync();
            return View(files);
        }

        public async Task<IActionResult> ListMessages()
        {
            var messages = await _queueStorageService.ListMessagesAsync();
            return View(messages);
        }
    }
}
