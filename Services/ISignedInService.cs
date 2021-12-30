using AlphaBlogging.Models;

namespace AlphaBlogging.Services
{
    public interface ISignedInService
    {
        public ApplicationUser GetAuthorId(string user);
    }
}
