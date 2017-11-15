namespace FSLibrary

module Engines =
    type Engine = {culture:string; searchEngine:string; routes:string list}

    let getDefault = {
                        culture="es"; 
                        searchEngine="https://www.google.es/search?q=";
                        routes=["https://www.aemps.gob.es/cima/dochtml/p";
                                "http://www.vademecum.es/medicamento";
                                "http://www.vademecum.es/principios-activos"]
                    }

    let database = [
            getDefault;
            {
                culture="cz"; 
                searchEngine="https://www.google.cz/search?q=";
                routes=["http://www.sukl.cz/modules/medication/detail.php?kod";
                        "https://www.drmax.cz";
                        "https://www.gigalekarna.cz/produkt/";]
            };
        ]

    
     