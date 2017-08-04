#load @".paket/load/main.group.fsx"

open FSharp.Data.Sql


let [<Literal>] resolutionPath = __SOURCE_DIRECTORY__ + @"\packages\System.Data.SQLite.Core\lib\net46"
let [<Literal>] connectionString = "Data Source=" + __SOURCE_DIRECTORY__ + @"\Northwind_small.sqlite;Version=3"
// create a type alias with the connection string and database vendor settings

type sql = SqlDataProvider< 
              ConnectionString = connectionString,
              DatabaseVendor = Common.DatabaseProviderTypes.SQLITE,
              ResolutionPath = resolutionPath,
              IndividualsAmount = 1000,
              UseOptionTypes = true >
let ctx = sql.GetDataContext()

let zz = 
    query {
        for e in ctx.Main.Employee do
        where (e.Title = None)
        join et in ctx.Main.EmployeeTerritory on (e.Id = Some et.EmployeeId)
        join t in ctx.Main.Territory on (et.TerritoryId = t.Id)
        select (t.RegionId)
    } |> Seq.tryHead





