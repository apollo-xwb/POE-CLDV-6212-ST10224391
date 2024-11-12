using Microsoft.AspNetCore.Mvc;
using POE_CLDV_6221_ST10224391.Models;
using POE_CLDV_6221_ST10224391.Services;

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

    public IActionResult SubmitToDatabase()
    {
        return View();
    }


    // Uploads chosen image to Azure Blob Storage and adds a message to the Queue
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

    // Uploads chosen file to Azures File Storage then adds a message to the Queue
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

    // List customer profiles from Azure Table Storage
    public async Task<IActionResult> ListCustomerProfiles()
    {
        var profiles = await _tableStorageService.GetCustomerProfilesAsync();
        return View(profiles);
    }

    // List images from Azure Blob Storage
    public async Task<IActionResult> ListImages()
    {
        var images = await _blobStorageService.ListImagesAsync();
        return View(images);
    }

    // List files from Azure File Storage
    public async Task<IActionResult> ListFiles()
    {
        var files = await _fileStorageService.ListFilesAsync();
        return View(files);
    }

    // List messages from Azure Queue Storage
    public async Task<IActionResult> ListMessages()
    {
        var messages = await _queueStorageService.ListMessagesAsync();
        return View(messages);
    }

    // Shows the create customer profile form
    public IActionResult CreateCustomerProfile()
    {
        return View();
    }

    // Creates a customers profile
    [HttpPost]
    public async Task<IActionResult> CreateCustomerProfile(CustomerProfile model)
    {
        if (ModelState.IsValid)
        {
            // Creates unique partition and row keys
            model.PartitionKey = "CustomerProfile"; // Sets the partition keys as needed
            model.RowKey = Guid.NewGuid().ToString(); // Unique identifier for a profile

            // Calls the service for inserting the customers profiles into the Azure Table Storage
            await _tableStorageService.InsertCustomerProfileAsync(model);
            return RedirectToAction("ListCustomerProfiles");
        }
        return View(model);
    }
}
