﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnimalEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class AnimalsContainer : DbContext
    {
        public AnimalsContainer()
            : base("name=AnimalsContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Animal> AnimalSet { get; set; }
        public DbSet<Habitat> HabitatSet { get; set; }
        public DbSet<Person> PersonSet { get; set; }
    
        [EdmFunction("AnimalsContainer", "DogsSearch")]
        public virtual IQueryable<DogsSearch_Result> DogsSearch(string searchstring)
        {
            var searchstringParameter = searchstring != null ?
                new ObjectParameter("searchstring", searchstring) :
                new ObjectParameter("searchstring", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<DogsSearch_Result>("[AnimalsContainer].[DogsSearch](@searchstring)", searchstringParameter);
        }
    }
}
