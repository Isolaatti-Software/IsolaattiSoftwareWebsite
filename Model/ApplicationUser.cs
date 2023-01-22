using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace IsolaattiSoftwareWebsite.Model;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    
}