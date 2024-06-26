using CreWin.Infrastructure.Context;
using CreWin.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using CreWin.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreWin.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CreWinContext _context;

        public UnitOfWork(CreWinContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(DbContext));
        }

        public int SaveChanges() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();


    }




    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly ECommerceContext _context;

    //    private CategoryRepository _categoryRepository;
    //    private ProductRepository _productRepository;


    //    public UnitOfWork(ECommerceContext context, ProductRepository productRepository)
    //    {
    //        _context = context ?? throw new ArgumentNullException(nameof(context));
    //        _categoryRepository = new CategoryRepository(_context);
    //        _productRepository = new ProductRepository(_context);
    //    }

    //    //public CategoryRepository CategoryRepository => _categoryRepository ?? (this._categoryRepository = new CategoryRepository(_context));

    //    public ICategoryRepository Categories => _categoryRepository;
    //    public IProductRepository Products => _productRepository;



    //    public void SaveChanges()
    //    {
    //        try
    //        {
    //            using (var transaction = _context.Database.BeginTransaction())
    //            {
    //                try
    //                {
    //                    _context.SaveChanges();
    //                    transaction.Commit();
    //                }
    //                catch
    //                {
    //                    transaction.Rollback();
    //                    throw;
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            //TODO:Logging
    //        }
    //    }
    //    private bool disposed = false;
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                _context.Dispose();
    //            }
    //        }
    //        this.disposed = true;
    //    }
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //}
}
