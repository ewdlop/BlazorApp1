namespace ClassLibrary1

open Microsoft.AspNetCore.Components

module Say =
    let hello name =
        printfn "Hello %s" name