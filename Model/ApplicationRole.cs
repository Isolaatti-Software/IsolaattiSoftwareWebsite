using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace IsolaattiSoftwareWebsite.Model;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
    
}