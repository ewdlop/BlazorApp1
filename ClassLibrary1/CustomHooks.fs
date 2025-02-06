//// CustomHooks.fs
namespace ClassLibrary1

//open Microsoft.AspNetCore.Components

//module CustomHooks =
//    let useLocalStorage<'T> key initialValue (triggerStateHasChanged: unit -> unit) =
//        let state = Hooks.useState initialValue triggerStateHasChanged
//        let setValue value =
//            state.Update(fun _ ->
//                let json = System.Text.Json.JsonSerializer.Serialize(value)
//                Browser.WebStorage.LocalStorage.setItem(key, json)
//                value)
//        // 初始化 localStorage 資料
//        match Browser.WebStorage.LocalStorage.getItem(key) with
//        | null -> ()
//        | json ->
//            try
//                let value = System.Text.Json.JsonSerializer.Deserialize<'T>(json)
//                setValue value |> ignore
//            with _ -> ()
//        state.Value, setValue
        
//    let useDebounce<'T> (value: 'T) (delay: int) (triggerStateHasChanged: unit -> unit) =
//        let state = Hooks.useState value triggerStateHasChanged
//        let timeout = Hooks.useState (None: System.Threading.Timer option) triggerStateHasChanged

//        timeout.Value |> Option.iter (fun t -> t.Dispose())

//        let timer = new System.Threading.Timer(
//            (fun _ ->
//                state.Update(fun _ -> value)
//                timeout.Update(fun _ -> None)),
//            null, delay, System.Threading.Timeout.Infinite)
//        timeout.Update(fun _ -> Some timer)
//        state.Value
