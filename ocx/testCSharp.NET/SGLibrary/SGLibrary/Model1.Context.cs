﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Data.EntityClient;
    
    
    public partial class dbSG2000Entities : DbContext
    {
    
    
    	private static string ConnectionString()
    	{
    		SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
    		sqlBuilder.DataSource = "PC0157";
    		sqlBuilder.InitialCatalog = "dbSG2000";
    		sqlBuilder.PersistSecurityInfo = true;
    		sqlBuilder.IntegratedSecurity = false;
    		sqlBuilder.UserID ="USRSG2006";
            sqlBuilder.Password  ="sonrisa14";
    		sqlBuilder.MultipleActiveResultSets = true;
    
    		EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
    		entityBuilder.ProviderConnectionString = sqlBuilder.ToString();
    		entityBuilder.Metadata = "res://*/";
    		entityBuilder.Provider = "System.Data.SqlClient";
    
    		return entityBuilder.ToString();
    	}
    
        public dbSG2000Entities()
               : base(nameOrConnectionString: ConnectionString())
        {
    	// EULISES
    
    
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<TB_Productos> TB_Productos { get; set; }
    }
}
