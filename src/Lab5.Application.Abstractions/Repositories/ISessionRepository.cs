using Lab5.Application.Models.Sessions;

namespace Lab5.Application.Abstractions.Repositories;

public interface ISessionRepository
{
    void AddSession(Session session);

    Session? FindSessionByKey(Guid sessionKey);

    void RemoveSession(Guid sessionKey);
}
