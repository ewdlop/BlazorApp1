namespace ClassLibrary1
//// CounterComponent.fs
//namespace ClassLibrary1

//open Microsoft.AspNetCore.Components

//type CounterComponent() =
//    inherit ComponentBase()

//    override this.BuildRenderTree(builder) =
//        let view = hooks {
//            let! countHook = Hooks.useState 0 (fun () -> this.StateHasChanged())
//            return fun () ->
//                div [] [
//                    p [] [str $"Count: {countHook.Value}"]
//                    button [ onClick (fun _ -> countHook.Update(fun x -> x + 1)) ] [ str "Increment" ]
//                    button [ onClick (fun _ -> countHook.Update(fun x -> x - 1)) ] [ str "Decrement" ]
//                ]
//        }
//        view (fun () -> this.StateHasChanged()) |> renderElement builder 0
