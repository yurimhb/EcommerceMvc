﻿using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext contexto;
        protected readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSets;

        public BaseRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
            dbSets = contexto.Set<T>();
        }
    }
}
