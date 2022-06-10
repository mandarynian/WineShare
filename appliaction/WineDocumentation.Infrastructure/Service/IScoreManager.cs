using System.Collections.Generic;
using WineDocumentation.Core.Domain;

namespace WineDocumentation.Infrastructure.Service
{
    public interface IScoreManager : IService
    {
        List<Score> GetScores(Wine wine);
        float GetAvgScores(Wine wine);
    }
}