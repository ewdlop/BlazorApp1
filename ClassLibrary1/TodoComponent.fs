// TodoComponent.fs
namespace ClassLibrary1

//open Microsoft.AspNetCore.Components

//type TodoItem = { Text: string; Completed: bool }

//type TodoComponent() =
//    inherit ComponentBase()


//    override this.BuildRenderTree(builder) =
//        let view = hooks {
//            let! todos = Hooks.useState [] (fun () -> this.StateHasChanged())
//            let! input = Hooks.useState "" (fun () -> this.StateHasChanged())

//            let addTodo () =
//                if not (System.String.IsNullOrWhiteSpace input.Value) then
//                    todos.Update(fun items -> { Text = input.Value; Completed = false } :: items)
//                    input.Update(fun _ -> "")
            
//            let toggleTodo index =
//                todos.Update(fun items ->
//                    items |> List.mapi (fun i todo ->
//                        if i = index then { todo with Completed = not todo.Completed }
//                        else todo))
            
//            return fun () ->
//                div [] [
//                    div [] [
//                        input [
//                            value input.Value
//                            onInput (fun e -> 
//                                // 將事件值轉型為 string
//                                input.Update(fun _ -> e.Value :?> string))
//                        ]
//                        button [ onClick (fun _ -> addTodo()) ] [ str "Add" ]
//                    ]
//                    ul [] [
//                        for (todo, i) in List.indexed todos.Value do
//                            li [
//                                style [
//                                    if todo.Completed then "text-decoration", "line-through"
//                                ]
//                                onClick (fun _ -> toggleTodo i)
//                            ] [ str todo.Text ]
//                    ]
//                ]
//        }
//        view (fun () -> this.StateHasChanged()) |> renderElement builder 0
