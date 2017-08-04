namespace TpRepro

open FSharp.Data.Sql

module Foo =
    
    let [<Literal>] resolutionPath = __SOURCE_DIRECTORY__ + @"\..\packages\System.Data.SQLite.Core\lib\net46"
    let [<Literal>] connectionString = "Data Source=" + __SOURCE_DIRECTORY__ + @"\..\Northwind_small.sqlite;Version=3"
    // create a type alias with the connection string and database vendor settings

    type sql = SqlDataProvider< 
                  ConnectionString = connectionString,
                  DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
                  ResolutionPath = resolutionPath,
                  IndividualsAmount = 1000,
                  UseOptionTypes = true >
    let ctx = sql.GetDataContext()
    
    let x = ctx.Main.Employee.Individuals.``1`` 
    

    let zz = 
        query {
            for aaa in ctx.Main.Employee do
            join bbb in ctx.Main.EmployeeTerritory on (aaa.Id = Some bbb.EmployeeId)
            select aaa.Address
        } |> Seq.tryHead
    


