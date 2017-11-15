module Tests

open System
open Xunit
open FSLibrary

    
[<Fact>]
let ``Find prospecto`` () = 
    let response = Scrapping.findProspecto {culture="es";term="casenlax"}
    Assert.NotNull(response)