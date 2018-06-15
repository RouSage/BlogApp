using BlogApp.Data;
using System;

namespace BlogApp.Service
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ICategoryRepository Categories { get; }
        ITagRepository Tags { get; }
        IRepository<Contact> ContactRepository { get; }

        void Save();
    }
}
