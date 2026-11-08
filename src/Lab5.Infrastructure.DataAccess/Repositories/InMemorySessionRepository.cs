using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Sessions;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly List<Session> _sessions;

    public InMemorySessionRepository()
    {
        _sessions = [];
    }

    public void AddSession(Session session)
    {
        _sessions.Add(session);
    }

    public Session? FindSessionByKey(Guid sessionKey)
    {
        return _sessions.FirstOrDefault(session => session.Key == sessionKey);
    }

    public void RemoveSession(Guid sessionKey)
    {
        Session? session = FindSessionByKey(sessionKey);
        if (session is null)
        {
            return;
        }

        _sessions.Remove(session);
    }
}
