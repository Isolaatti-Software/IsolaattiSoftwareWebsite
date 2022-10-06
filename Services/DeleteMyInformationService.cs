using System.Security.Cryptography;
using System.Text.RegularExpressions;
using IsolaattiSoftwareWebsite.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IsolaattiSoftwareWebsite.Services;

public class NoRequestsFoundException : ApplicationException
{
    public override string Message => "No form was sent, nothing to delete";
}

public class RequestCodeNotCorrectException : ApplicationException
{
    private readonly string _id;
    private readonly string _code;

    public RequestCodeNotCorrectException(string id, string code)
    {
        _id = id;
        _code = code;
    }

    public override string Message => $"Code {_code} is not valid for request with id {_id}";
}

public class DeleteMyInformationService : IDeleteMyInformationService
{
    private readonly IMongoCollection<DeleteMyInformationRequest> _deleteMyInformationRequests;
    private readonly IMongoCollection<ContactUs> _contactUsCollection;
    private readonly IEmailSenderService _emailSender;

    public DeleteMyInformationService(IOptions<MongoConfiguration> mongoConfig, IEmailSenderService emailSender)
    {
        var client = new MongoClient(mongoConfig.Value.ConnectionString);
        var database = client.GetDatabase("isolaattisoftware-db");
        _deleteMyInformationRequests = database.GetCollection<DeleteMyInformationRequest>("delete_info_requests");
        _contactUsCollection = database.GetCollection<ContactUs>("contact_us");
        _emailSender = emailSender;
    }
    
    /// <summary>
    /// Inserts and sends a new code to allow user to remove their data.
    /// </summary>
    /// <param name="email">Email address to sent the code to</param>
    /// <exception cref="FormatException">Email provided is invalid.</exception>
    /// <exception cref="NoRequestsFoundException"></exception>
    public async Task AddRequest(string email)
    {
        var emailRegex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
        if (!emailRegex.IsMatch(email))
        {
            throw new FormatException("Email address is not valid");
        }
        
        // Check if there is at least one "First contact" document
        var documentsFound = await _contactUsCollection.CountDocumentsAsync(doc => doc.Email.Equals(email));

        if (documentsFound < 1)
        {
            throw new NoRequestsFoundException();
        }

        var firstRequest = _contactUsCollection.Find(doc => doc.Email.Equals(email)).First();
        

        var passwordHasher = new PasswordHasher<string>();
        
        var randomData = new byte[64];
        RandomNumberGenerator.Create().GetBytes(randomData);

        var secret = Convert.ToHexString(randomData);

        var newRequest = new DeleteMyInformationRequest
        {
            Email = email,
            HashedSecret = passwordHasher.HashPassword(email, secret)
        };
        
        await _deleteMyInformationRequests.InsertOneAsync(newRequest);
        await _emailSender.SendDeleteMyInfoCode(newRequest.Email, firstRequest?.Name ?? "", newRequest.Id, secret);

    }

    /// <summary>
    /// Validates code and if it is valid it removes the data matching the email the found request points
    /// </summary>
    /// <param name="requestId">id of remove my info request</param>
    /// <param name="code">Un-hashed code</param>
    /// <exception cref="ArgumentException">No request with the given id was found</exception>
    /// <exception cref="RequestCodeNotCorrectException">Request was found, but code is not correct</exception>
    public async Task DeleteData(string requestId, string code)
    {
        var request = await _deleteMyInformationRequests
            .Find(doc => doc.Id.Equals(requestId))
            .FirstOrDefaultAsync();
        if (request == null)
        {
            throw new ArgumentException($"Request with id {requestId} does not exist");
        }

        // verifies key using password hasher
        var passwordHasher = new PasswordHasher<string>();
        var verificationResult = passwordHasher.VerifyHashedPassword(request.Email, request.HashedSecret, code);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new RequestCodeNotCorrectException(requestId, code);
        }
        
        // Code is valid, delete all documents matching email
        await _contactUsCollection.DeleteManyAsync(doc => doc.Email.Equals(request.Email));
        await _deleteMyInformationRequests.DeleteOneAsync(doc => doc.Id.Equals(requestId));
    }

    /// <summary>
    /// Returns the object of the request.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>DeleteMyInformationRequest object</returns>
    public DeleteMyInformationRequest GetDeleteMyInfoRequest(string id)
    {
        return _deleteMyInformationRequests.Find(doc => doc.Id.Equals(id)).FirstOrDefault();
    }
}