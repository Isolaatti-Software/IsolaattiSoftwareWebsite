using System.Text.RegularExpressions;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IsolaattiSoftwareWebsite.Services;

public class FirstContactFormResponsesService : IFirstContactFormResponses
{
    private readonly IMongoCollection<ContactUs> _contactUsCollection;
    
    public FirstContactFormResponsesService(IOptions<MongoConfiguration> mongoConfig)
    {
        var client = new MongoClient(mongoConfig.Value.ConnectionString);
        var database = client.GetDatabase("isolaattisoftware-db");
        _contactUsCollection = database.GetCollection<ContactUs>("contact_us");
    }

    public async Task SaveForm(ContactUs contactUsObj)
    {
        await _contactUsCollection.InsertOneAsync(contactUsObj);
    }
    
    public async Task<IEnumerable<ContactUs>> GetFirstContactsByEmailAddress(string emailAddress)
    {
        return await _contactUsCollection.Find(doc => doc.Email.Equals(emailAddress)).Limit(100).ToListAsync();
    }

    public async Task<IEnumerable<ContactUs>> GetFirstContact(string? lastId, int count)
    {
        if (lastId == null)
        {
            return await _contactUsCollection
                .Find(doc => true)
                .SortByDescending(doc => doc.Id)
                .Limit(count)
                .ToListAsync();
        }
        var filter = Builders<ContactUs>.Filter.Gt("id", lastId);
        return await _contactUsCollection
            .Find(filter)
            .SortByDescending(doc => doc.Id)
            .Limit(count)
            .ToListAsync();
    }

    public bool ValidateForm(ContactUs contactUsObj)
    {
        if (contactUsObj.Name.Length < 1)
        {
            return false;
        }

        if (contactUsObj.LastName.Length < 1)
        {
            return false;
        }

        var emailRegex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
        if (!emailRegex.IsMatch(contactUsObj.Email))
        {
            return false;
        }

        if (contactUsObj.Message.Length < 100)
        {
            return false;
        }

        return true;
    }
}