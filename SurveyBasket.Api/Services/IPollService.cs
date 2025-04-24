using SurveyBasket.Models;

namespace SurveyBasket.Services;

public interface IPollService
{
    IEnumerable<Poll> GetAll();
    Poll? Get(int id);
    Poll Add(Poll poll);
    bool Upate(int id, Poll poll);
    bool Delete(int id);


}
