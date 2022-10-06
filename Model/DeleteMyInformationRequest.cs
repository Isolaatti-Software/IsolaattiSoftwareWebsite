using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IsolaattiSoftwareWebsite.Model;

public class DeleteMyInformationRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Email { get; set; }
    public string HashedSecret { get; set; }
}