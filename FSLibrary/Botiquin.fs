namespace FSLibrary

open FSharp.Data

module Scrapping = 
    type MedicineToSearchRequest = { culture:string; term:string }
    type Link = { name:string; url:string }

    let getEngineByCulture culture =
        let engine = 
            Engines.database |> List.tryFind (fun x -> x.culture = culture)
        engine |> Option.defaultValue Engines.getDefault
       

    let private getLinksFromSearch url = 
        async {
            let! googleResult = HtmlDocument.AsyncLoad(url)
            let links = 
                googleResult.Descendants["a"]
                |> Seq.filter (fun x -> x.AttributeValue("href").StartsWith("/url?"))
                |> Seq.filter (fun x -> x.AttributeValue("href").IndexOf("&sa=") <> -1)
                |> Seq.map(fun x -> {name=x.InnerText();
                    url=x.AttributeValue("href").Substring(0, x.AttributeValue("href").IndexOf("&sa=")).Replace("/url?q=", "")})
            return links
        }


    let private matchLinksPattern (links: seq<Link>) (pattern: string) = 
         let result = links |> Seq.filter (fun (link) -> link.url.StartsWith(pattern))
         result

    
    let findProspecto medicine =
        let engine = getEngineByCulture medicine.culture
        let links = getLinksFromSearch (engine.searchEngine + medicine.term) |> Async.RunSynchronously
        let results = engine.routes |> Seq.collect(matchLinksPattern links)
        results
        
            
        
