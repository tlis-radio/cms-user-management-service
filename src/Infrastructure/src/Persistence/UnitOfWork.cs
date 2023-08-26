using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.UserManagement.Infrastructure.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    public IUserRepository UserRepository => _lazyUserRepository.Value;

    public IRoleRepository RoleRepository => _lazyRoleRepository.Value;

    public IUserRoleHistoryRepository UserRoleHistoryRepository => _lazyUserRoleHistoryRepository.Value;

    private bool _disposed;

    private readonly ILogger<UnitOfWork> _logger;

    private readonly UserManagementDbContext _dbContext;

    private readonly Lazy<IUserRepository> _lazyUserRepository;

    private readonly Lazy<IRoleRepository> _lazyRoleRepository;

    private readonly Lazy<IUserRoleHistoryRepository> _lazyUserRoleHistoryRepository;


    public UnitOfWork(UserManagementDbContext dbContext, ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _lazyUserRepository = new(() => new UserRepository(_dbContext));
        _lazyRoleRepository = new(() => new RoleRepository(_dbContext));
        _lazyUserRoleHistoryRepository = new(() => new UserRoleHistoryRepository(_dbContext));
        _logger = logger;
    }

    public void SetStateUnchanged<TEntity>(params TEntity[] entities) where TEntity : class
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError("{Exception}", exception.Message);
            
            throw exception switch
            {
                UniqueConstraintException => new EntityAlreadyExistsException(),
                DbUpdateConcurrencyException => new EntityNotFoundException(),
                _ => new Exception(exception.Message)
            };
        }
    }

    public async Task ExecutePendingMigrationsAsync()
    {
        var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync()).ToList();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation("Applying migrations: {Join}", string.Join(',', pendingMigrations));

            await _dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }

    public void Dispose()
    {
        if (!_disposed)
            _dbContext.Dispose();

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}