using IsolaattiSoftwareWebsite.Model;

namespace IsolaattiSoftwareWebsite.Services;

public interface IDeleteMyInformationService
{
    /// <summary>
    /// Inserts and sends a new code to allow user to remove their data.
    /// </summary>
    /// <param name="email">Email address to sent the code to</param>
    /// <exception cref="FormatException">Email provided is invalid.</exception>
    /// <exception cref="NoRequestsFoundException"></exception>
    Task AddRequest(string email);

    /// <summary>
    /// Validates code and if it is valid it removes the data matching the email the found request points
    /// </summary>
    /// <param name="requestId">id of remove my info request</param>
    /// <param name="code">Un-hashed code</param>
    /// <exception cref="ArgumentException">No request with the given id was found</exception>
    /// <exception cref="RequestCodeNotCorrectException">Request was found, but code is not correct</exception>
    Task DeleteData(string requestId, string code);

    DeleteMyInformationRequest? GetDeleteMyInfoRequest(string id);

}