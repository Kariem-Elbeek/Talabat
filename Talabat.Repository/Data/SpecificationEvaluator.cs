﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> entityQuery, ISpecification<TEntity> spec)
        {
            var query = entityQuery; // _context.Set<TEntity>
            if(spec.Criteria != null)
                query = query.Where(spec.Criteria);
            
            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if(spec.OrderByDesc != null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (currentQuery, nextQuery) => currentQuery.Include(nextQuery));

            return query;
        }

    }
}
