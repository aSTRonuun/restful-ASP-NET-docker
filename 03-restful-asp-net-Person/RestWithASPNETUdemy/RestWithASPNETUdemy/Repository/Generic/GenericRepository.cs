﻿using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(p => p.Id == id);
        }

        public T Update(T item)
        {
            if (!Exists(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(item).CurrentValues.SetValues(result);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.Id == (id));

        }
    }
}
