//// SearchComponent.fs
namespace ClassLibrary1

//open Microsoft.AspNetCore.Components

//type SearchComponent() =
//    inherit ComponentBase()

//    override this.BuildRenderTree(builder) =
//        let view = hooks {
//            let! query = Hooks.useState "" (fun () -> this.StateHasChanged())
//            let debouncedQuery = CustomHooks.useDebounce query.Value 300 (fun () -> this.StateHasChanged())
//            let! results = Hooks.useState [] (fun () -> this.StateHasChanged())
            
//            // 此處可加入 Effect 以進行 API 呼叫
            
//            return fun () ->
//                div [] [
//                    input [
//                        value query.Value
//                        onInput (fun e -> query.Update(fun _ -> e.Value :?> string))
//                    ]
//                    if debouncedQuery <> query.Value then
//                        p [] [ str "Searching..." ]
//                    ul [] [
//                        for result in results.Value do
//                            li [] [ str result ]
//                    ]
//                ]
//        }
//        view (fun () -> this.StateHasChanged()) |> renderElement builder 0
