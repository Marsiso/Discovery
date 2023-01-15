using PexelsDotNetSDK.Api;
using PexelsDotNetSDK.Models;
using System.Threading.Tasks;

namespace Discovery.Services;

public class CarouselService
{
    private const string ApiKey = "563492ad6f91700001000001dfe6486d132a444e9e1aa767ea556fa5";
    private const string DefaultCategory = "nature";

    public async Task<PhotoPage> GetAllCategorizedImages(string category, int pageNumber = 1, int pageSize = 30)
    {
        if (string.IsNullOrEmpty(category))
        {
            category = DefaultCategory;
        }

        var httpClient = new PexelsClient(ApiKey);

        return await httpClient.SearchPhotosAsync(query: category, page: pageNumber, pageSize: pageSize);
    }
}
